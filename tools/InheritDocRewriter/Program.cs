using Microsoft.Build.Locator;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;

namespace InheritDocRewriter;

internal static class Program
{
    private static async Task<int> Main(string[] args)
    {
        if (args.Length == 0)
        {
            Console.Error.WriteLine("Usage: InheritDocRewriter <path-to-solution.sln>");
            return 1;
        }

        var solutionPath = Path.GetFullPath(args[0]);
        if (!File.Exists(solutionPath))
        {
            Console.Error.WriteLine($"Solution not found: {solutionPath}");
            return 1;
        }

        if (!MSBuildLocator.IsRegistered)
            MSBuildLocator.RegisterDefaults();

        using var workspace = MSBuildWorkspace.Create();
        workspace.WorkspaceFailed += (_, e) =>
        {
            if (e.Diagnostic.Kind == WorkspaceDiagnosticKind.Failure)
                Console.Error.WriteLine($"[workspace] {e.Diagnostic.Message}");
        };

        Console.WriteLine($"Loading solution: {solutionPath}");
        var solution = await workspace.OpenSolutionAsync(solutionPath);

        int totalFiles = 0, totalReplacements = 0;

        foreach (var project in solution.Projects)
        {
            Console.WriteLine($"Project: {project.Name}");
            var compilation = await project.GetCompilationAsync();
            if (compilation is null)
                continue;

            foreach (var document in project.Documents)
            {
                if (document.FilePath is null)
                    continue;
                if (IsGenerated(document.FilePath))
                    continue;

                var tree = await document.GetSyntaxTreeAsync();
                if (tree is null)
                    continue;
                var model = compilation.GetSemanticModel(tree);
                var root = await tree.GetRootAsync();

                var rewriter = new InheritDocRewriterWalker(model);
                var newRoot = rewriter.Visit(root);
                if (rewriter.Replacements == 0 || newRoot is null || newRoot == root)
                    continue;

                var originalText = await document.GetTextAsync();
                var encoding = originalText.Encoding ?? System.Text.Encoding.UTF8;
                var newText = newRoot.ToFullString();

                await File.WriteAllTextAsync(document.FilePath, newText, encoding);
                totalFiles++;
                totalReplacements += rewriter.Replacements;
                Console.WriteLine($"  {rewriter.Replacements,4} -> {Path.GetRelativePath(Path.GetDirectoryName(solutionPath)!, document.FilePath)}");
            }
        }

        Console.WriteLine($"Done. Modified {totalFiles} file(s), {totalReplacements} member(s).");
        return 0;
    }

    private static bool IsGenerated(string path)
    {
        var fn = Path.GetFileName(path);
        if (fn.EndsWith(".g.cs", StringComparison.OrdinalIgnoreCase)) return true;
        if (fn.EndsWith(".g.i.cs", StringComparison.OrdinalIgnoreCase)) return true;
        if (fn.EndsWith(".Designer.cs", StringComparison.OrdinalIgnoreCase)) return true;
        if (path.Contains($"{Path.DirectorySeparatorChar}obj{Path.DirectorySeparatorChar}", StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }
}
