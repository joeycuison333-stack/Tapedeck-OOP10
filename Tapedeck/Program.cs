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

        using (SplashForm splash = new SplashForm())
        {
            splash.ShowDialog(); // returns when the splash closes itself
        }

        Application.Run(new TapedeckForm());
    }
}
