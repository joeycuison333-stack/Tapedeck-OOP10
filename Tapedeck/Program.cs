namespace mediaPlayerPrj;

/// <summary>Entry point: shows the splash screen, then the player.</summary>
internal static class Program
{
    /// <summary>Starts the application.</summary>
    [STAThread]
    private static void Main()
    {
        // Any error that slips past a try-catch is shown to the user instead of crashing the demo.
        Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
        Application.ThreadException += (sender, e) =>
            MessageBox.Show("Something went wrong:\n\n" + e.Exception.Message, TeamInfo.AppTitle,
                MessageBoxButtons.OK, MessageBoxIcon.Error);

        ApplicationConfiguration.Initialize();

        // Starts loading the native VLC engine (about 1-2 seconds on a cold start) on a background
        // thread right away, so that time overlaps with the splash screen's ~3 second display
        // instead of pausing on a blank window right after the splash closes.
        Task<LibVLCSharp.Shared.LibVLC> enginePrep = BasePlayerForm.PrepareEngineAsync();

        using (SplashForm splash = new SplashForm())
        {
            splash.ShowDialog(); // returns when the splash closes itself
        }

        Application.Run(new TapedeckForm(enginePrep));
    }
}
