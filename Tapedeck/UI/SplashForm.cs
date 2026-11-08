namespace mediaPlayerPrj;

/// <summary>
/// PLACEHOLDER pushed by Joey so the project builds. MADDOX replaces this whole file
/// with the real splash screen (logo, title, group, member photos, 3-second timer).
/// This version closes as soon as it appears.
/// </summary>
public class SplashForm : Form
{
    /// <summary>Creates a splash that closes immediately.</summary>
    public SplashForm()
    {
        Text = TeamInfo.AppTitle;
        Shown += (sender, e) => DialogResult = DialogResult.OK;
    }
}
