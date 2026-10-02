using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace InheritDocRewriter;

internal sealed class InheritDocRewriterWalker : CSharpSyntaxRewriter
{
    private readonly SemanticModel _semanticModel;
    public int Replacements { get; private set; }

    public InheritDocRewriterWalker(SemanticModel semanticModel)
    {
        _semanticModel = semanticModel;
    }

    public override SyntaxNode? VisitMethodDeclaration(MethodDeclarationSyntax node) => TryRewrite(node) ?? base.VisitMethodDeclaration(node);
    public override SyntaxNode? VisitConstructorDeclaration(ConstructorDeclarationSyntax node) => TryRewrite(node) ?? base.VisitConstructorDeclaration(node);
    public override SyntaxNode? VisitPropertyDeclaration(PropertyDeclarationSyntax node) => TryRewrite(node) ?? base.VisitPropertyDeclaration(node);
    public override SyntaxNode? VisitIndexerDeclaration(IndexerDeclarationSyntax node) => TryRewrite(node) ?? base.VisitIndexerDeclaration(node);
    public override SyntaxNode? VisitEventDeclaration(EventDeclarationSyntax node) => TryRewrite(node) ?? base.VisitEventDeclaration(node);
    public override SyntaxNode? VisitEventFieldDeclaration(EventFieldDeclarationSyntax node) => TryRewrite(node) ?? base.VisitEventFieldDeclaration(node);
    public override SyntaxNode? VisitOperatorDeclaration(OperatorDeclarationSyntax node) => TryRewrite(node) ?? base.VisitOperatorDeclaration(node);
    public override SyntaxNode? VisitConversionOperatorDeclaration(ConversionOperatorDeclarationSyntax node) => TryRewrite(node) ?? base.VisitConversionOperatorDeclaration(node);
    public override SyntaxNode? VisitDestructorDeclaration(DestructorDeclarationSyntax node) => TryRewrite(node) ?? base.VisitDestructorDeclaration(node);

    public override SyntaxNode? VisitClassDeclaration(ClassDeclarationSyntax node) => TryRewritePrimaryCtorType(node) ?? base.VisitClassDeclaration(node);
    public override SyntaxNode? VisitStructDeclaration(StructDeclarationSyntax node) => TryRewritePrimaryCtorType(node) ?? base.VisitStructDeclaration(node);
    public override SyntaxNode? VisitRecordDeclaration(RecordDeclarationSyntax node) => TryRewritePrimaryCtorType(node) ?? base.VisitRecordDeclaration(node);

    private SyntaxNode? TryRewritePrimaryCtorType(TypeDeclarationSyntax node)
    {
        if (node.ParameterList is null)
            return null; // not a primary-constructor type
        return TryRewrite(node);
    }

    private SyntaxNode? TryRewrite(SyntaxNode node)
    {
        var leading = node.GetLeadingTrivia();
        var docIndex = FindDocCommentIndex(leading);
        if (docIndex < 0)
            return null;

        var docTrivia = leading[docIndex];
        var docSyntax = (DocumentationCommentTriviaSyntax?)docTrivia.GetStructure();
        if (docSyntax is null)
            return null;

        // Skip if already inheritdoc only.
        if (ContainsInheritDoc(docSyntax))
            return null;

        ISymbol? symbol = node switch
        {
            TypeDeclarationSyntax t when t.ParameterList is not null
                => _semanticModel.GetDeclaredSymbol(t) is INamedTypeSymbol nt
                    ? FindPrimaryConstructor(nt, t.ParameterList)
                    : null,
            EventFieldDeclarationSyntax ef => ef.Declaration.Variables.Count == 1
                ? _semanticModel.GetDeclaredSymbol(ef.Declaration.Variables[0])
                : null,
            _ => _semanticModel.GetDeclaredSymbol(node)
        };

        if (symbol is null || !InheritDocEligibility.IsEligible(symbol))
            return null;

        var newLeading = ReplaceDocWithInheritDoc(leading, docIndex);
        Replacements++;
        return node.WithLeadingTrivia(newLeading);
    }

    private static IMethodSymbol? FindPrimaryConstructor(INamedTypeSymbol type, ParameterListSyntax paramList)
    {
        foreach (var ctor in type.InstanceConstructors)
        {
            if (ctor.Parameters.Length != paramList.Parameters.Count)
                continue;
            // Primary constructor's declaring syntax references include the TypeDeclarationSyntax.
            foreach (var r in ctor.DeclaringSyntaxReferences)
            {
                if (r.GetSyntax() is TypeDeclarationSyntax)
                    return ctor;
            }
        }
        return null;
    }

    private static int FindDocCommentIndex(SyntaxTriviaList trivia)
    {
        for (int i = 0; i < trivia.Count; i++)
        {
            var kind = trivia[i].Kind();
            if (kind == SyntaxKind.SingleLineDocumentationCommentTrivia
                || kind == SyntaxKind.MultiLineDocumentationCommentTrivia)
            {
                return i;
            }
        }
        return -1;
    }

    private static bool ContainsInheritDoc(DocumentationCommentTriviaSyntax doc)
    {
        foreach (var child in doc.Content)
        {
            string? name = child switch
            {
                XmlEmptyElementSyntax e => e.Name.LocalName.ValueText,
                XmlElementSyntax e => e.StartTag.Name.LocalName.ValueText,
                _ => null
            };
            if (string.Equals(name, "inheritdoc", System.StringComparison.OrdinalIgnoreCase))
                return true;
        }
        return false;
    }

    private static SyntaxTriviaList ReplaceDocWithInheritDoc(SyntaxTriviaList leading, int docIndex)
    {
        // Determine the indentation to apply from the whitespace trivia immediately preceding the doc comment.
        string indent = string.Empty;
        if (docIndex > 0 && leading[docIndex - 1].IsKind(SyntaxKind.WhitespaceTrivia))
            indent = leading[docIndex - 1].ToString();

        var inheritDocText = "/// <inheritdoc/>\r\n";
        var parsed = SyntaxFactory.ParseLeadingTrivia(inheritDocText);

        // Build new trivia: keep everything before docIndex (including the preceding whitespace),
        // insert the inheritdoc trivia, re-add the indent so the member alignment is preserved,
        // then keep everything after docIndex (which typically starts with the end-of-line that
        // terminated the original doc comment, followed by the member's own indent — we must remove
        // that trailing end-of-line because our replacement already ends with one).
        var builder = new List<SyntaxTrivia>(leading.Count + parsed.Count);
        for (int i = 0; i < docIndex; i++)
            builder.Add(leading[i]);

        builder.AddRange(parsed);

        // Skip a single end-of-line trivia that followed the original doc comment, if present.
        int startAfter = docIndex + 1;
        if (startAfter < leading.Count && leading[startAfter].IsKind(SyntaxKind.EndOfLineTrivia))
            startAfter++;

        // Re-add indent for the member if we consumed an EOL (otherwise the next trivia already includes it).
        if (startAfter > docIndex + 1 && !string.IsNullOrEmpty(indent))
            builder.Add(SyntaxFactory.Whitespace(indent));

        for (int i = startAfter; i < leading.Count; i++)
            builder.Add(leading[i]);

        return SyntaxFactory.TriviaList(builder);
    }
}
