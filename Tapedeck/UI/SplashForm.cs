using Tapedeck.UI;

namespace mediaPlayerPrj;

/// <summary>
/// Splash screen. M3: layout (logo, title, group, section).
/// M4 (member cards) and M5 (3-second timer) are added to this file next.
/// </summary>
public class SplashForm : Form
{
    private readonly PictureBox _logo = new PictureBox();
    private readonly Label _title = new Label();
    private readonly Label _group = new Label();
    private readonly Panel _cardsPanel = new Panel();   // M4 fills this

    /// <summary>Builds the splash window.</summary>
    public SplashForm()
    {
        Text = TeamInfo.AppTitle;
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(980, 620);
        BackColor = Color.FromArgb(24, 24, 28);
        DoubleBuffered = true;
        KeyPreview = true;

        // Canva skin background (added in M6). Skipped if the file is missing.
        string skin = AssetPath("tapedeck_skin.png");
        if (File.Exists(skin))
        {
            BackgroundImage = Image.FromFile(skin);
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        BuildLayout();

        // Temporary so the app never gets stuck: click or any key closes the splash.
        // M5 adds the 3-second timer.
        Click += (sender, e) => DialogResult = DialogResult.OK;
        KeyDown += (sender, e) => DialogResult = DialogResult.OK;
    }

    private void BuildLayout()
    {
        // Logo (top centre)
        _logo.Size = new Size(120, 120);
        _logo.Location = new Point((ClientSize.Width - _logo.Width) / 2, 30);
        _logo.SizeMode = PictureBoxSizeMode.Zoom;
        _logo.BackColor = Color.Transparent;
        string logoPath = AssetPath(TeamInfo.LogoFile);
        if (File.Exists(logoPath))
        {
            _logo.Image = Image.FromFile(logoPath);
        }
        Controls.Add(_logo);

        // Title
        _title.Text = TeamInfo.AppTitle;
        _title.Font = new Font("Segoe UI", 36f, FontStyle.Bold);
        _title.ForeColor = Color.White;
        _title.BackColor = Color.Transparent;
        _title.AutoSize = false;
        _title.TextAlign = ContentAlignment.MiddleCenter;
        _title.SetBounds(0, 160, ClientSize.Width, 65);
        Controls.Add(_title);

        // Group and section
        _group.Text = TeamInfo.GroupName + "  |  " + TeamInfo.Section;
        _group.Font = new Font("Segoe UI", 14f, FontStyle.Regular);
        _group.ForeColor = Color.Gainsboro;
        _group.BackColor = Color.Transparent;
        _group.AutoSize = false;
        _group.TextAlign = ContentAlignment.MiddleCenter;
        _group.SetBounds(0, 225, ClientSize.Width, 35);
        Controls.Add(_group);

        // Area reserved for the member cards (M4)
        _cardsPanel.BackColor = Color.Transparent;
        _cardsPanel.SetBounds(30, 290, ClientSize.Width - 60, 300);
        Controls.Add(_cardsPanel);
    }

    /// <summary>Assets are copied next to the .exe by the .csproj (Assets\**).</summary>
    private static string AssetPath(string file)
    {
        return Path.Combine(AppContext.BaseDirectory, "Assets", file);
    }

    /// <summary>Frees the images.</summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _logo.Image?.Dispose();
            BackgroundImage?.Dispose();
        }
        base.Dispose(disposing);
    }
}