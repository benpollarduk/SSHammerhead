using NetAF.Assets;
using NetAF.Events;
using NetAF.Logic;
using NetAF.Targets.Console;
using NetAF.Targets.Console.Rendering;
using SSHammerhead;
using SSHammerhead.Assets.Regions.Ship.Items;
using SSHammerhead.Configuration;
using SSHammerhead.Console;

internal class Program
{
    private static Size RecommendedSize => new Size(120, 50);

    private static void Main(string[] _)
    {
        try
        {
            var currentSize = new Size(Console.WindowWidth, Console.WindowHeight);
            CheckScreenSize(RecommendedSize, currentSize);

            EventBus.Subscribe<GameRestored>(x =>
            {
                if (Radio.IsPlaying(x.Game))
                    Radio.Start(x.Game, 1, Radio.DetermineProximity(x.Game));
            });
            EventBus.Subscribe<GameFinished>(x => Radio.Stop(x.Game));
            EventBus.Subscribe<GameUpdated>(x => Radio.Adjust(1, Radio.DetermineProximity(x.Game)));

            var presentation = new Presentation
            (
                FrameBuilderCollections.Naomi,
                FrameBuilderCollections.Bot,
                FrameBuilderCollections.Anne,
                FrameBuilderCollections.Alex,
                FrameBuilderCollections.Marina,
                FrameBuilderCollections.Scott,
                FrameBuilderCollections.Zhiying
            );

            var configuration = new GameConfiguration(new ConsoleAdapter(), FrameBuilderCollections.Naomi, Size.Dynamic);
            GameExecutor.Execute(TroubleAboardTheSSHammerhead.Create(configuration, presentation, true), new ConsoleExecutionController());
        }
        catch (Exception e)
        {
            Console.WriteLine($"Exception caught running game: {e.Message}");
            Console.ReadKey();
        }
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility", Justification = "Checks internally.")]
    private static void CheckScreenSize(Size recommended, Size actual)
    {
        if (Environment.OSVersion.Platform != PlatformID.Win32NT)
            return;

        if (actual.Width > recommended.Width && actual.Height > recommended.Height)
            return;

        var builder = new GridStringBuilder();
        builder.Resize(actual);

        builder.DrawWrapped("Warning!", 0, 1, actual.Width, AnsiColor.BrightYellow, out var _, out var lastY);
        builder.DrawWrapped($"The current console window size ({actual.Width}x{actual.Height}) is smaller than the recommended size ({recommended.Width}x{recommended.Height}). Some content may not display correctly.", 0, lastY += 2, actual.Width, AnsiColor.White, out _, out lastY);
        builder.DrawWrapped("Press 'Y' to resize now or any other key to continue without resizing.", 0, lastY += 2, actual.Width, AnsiColor.White, out _, out _);

        var frame = new GridTextFrame(builder, 0, 0, false, AnsiColor.Black, AnsiColor.White);
        var output = frame.ToPresentableString();
        Console.WriteLine(output);

        var key = Console.ReadKey();

        if (key.Key == ConsoleKey.Y)
            Console.SetWindowSize(recommended.Width, recommended.Height);
    }
}