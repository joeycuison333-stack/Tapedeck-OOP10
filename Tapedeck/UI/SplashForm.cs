namespace mediaPlayerPrj;

/// <summary>
/// Splash screen. M3: layout (logo, title, group, section).
/// M4: member cards with photo, name and role (grey placeholder if no photo).
/// M5: closes after about 3 seconds, or on click / key press.
/// </summary>
public class SplashForm : Form
{
    private const int SplashMilliseconds = 3000;

    private readonly PictureBox _logo = new PictureBox();
    private readonly Label _title = new Label();
    private readonly Label _group = new Label();
    private readonly Panel _cardsPanel = new Panel();
    private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();

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
            BackgroundImage = LoadImage(skin);
            BackgroundImageLayout = ImageLayout.Stretch;
        }

        BuildLayout();

        // M5: click or any key closes the splash
        Click += (sender, e) => CloseSplash();
        KeyDown += (sender, e) => CloseSplash();

        // M5: close automatically after 3 seconds
        _timer.Interval = SplashMilliseconds;
        _timer.Tick += (sender, e) => CloseSplash();
    }

    /// <summary>Starts the 3-second countdown once the splash is visible.</summary>
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        _timer.Start();
    }

    /// <summary>Stops the timer and closes the splash (safe to call more than once).</summary>
    private void CloseSplash()
    {
        _timer.Stop();
        if (DialogResult != DialogResult.OK)
        {
            DialogResult = DialogResult.OK;
        }
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
            _logo.Image = LoadImage(logoPath);
        }
        _logo.Click += (sender, e) => CloseSplash();
        Controls.Add(_logo);

        // Title
        _title.Text = TeamInfo.AppTitle;
        _title.Font = new Font("Segoe UI", 36f, FontStyle.Bold);
        _title.ForeColor = Color.White;
        _title.BackColor = Color.Transparent;
        _title.AutoSize = false;
        _title.TextAlign = ContentAlignment.MiddleCenter;
        _title.SetBounds(0, 160, ClientSize.Width, 65);
        _title.Click += (sender, e) => CloseSplash();
        Controls.Add(_title);

        // Group and section
        _group.Text = TeamInfo.GroupName + "  |  " + TeamInfo.Section;
        _group.Font = new Font("Segoe UI", 14f, FontStyle.Regular);
        _group.ForeColor = Color.Gainsboro;
        _group.BackColor = Color.Transparent;
        _group.AutoSize = false;
        _group.TextAlign = ContentAlignment.MiddleCenter;
        _group.SetBounds(0, 225, ClientSize.Width, 35);
        _group.Click += (sender, e) => CloseSplash();
        Controls.Add(_group);

        // Member cards (M4)
        _cardsPanel.BackColor = Color.Transparent;
        _cardsPanel.SetBounds(30, 290, ClientSize.Width - 60, 300);
        _cardsPanel.Click += (sender, e) => CloseSplash();
        Controls.Add(_cardsPanel);
        BuildCards();
    }

    /// <summary>Adds one card per team member, side by side.</summary>
    private void BuildCards()
    {
        const int gap = 15;
        int count = TeamInfo.Members.Length;
        int cardWidth = (_cardsPanel.Width - gap * (count - 1)) / count;
        int x = 0;

        foreach (TeamMember member in TeamInfo.Members)
        {
            Panel card = CreateCard(member, cardWidth, _cardsPanel.Height - 10);
            card.Location = new Point(x, 0);
            _cardsPanel.Controls.Add(card);
            x += cardWidth + gap;
        }
    }

    /// <summary>Builds one member card: photo (or placeholder), name and role.</summary>
    private Panel CreateCard(TeamMember member, int width, int height)
    {
        Panel card = new Panel();
        card.Size = new Size(width, height);
        card.BackColor = Color.FromArgb(40, 40, 48);

        int photoSize = Math.Min(140, width - 20);
        int photoX = (width - photoSize) / 2;

        string photoPath = AssetPath(member.PhotoFile);
        if (File.Exists(photoPath))
        {
            PictureBox photo = new PictureBox();
            photo.SetBounds(photoX, 15, photoSize, photoSize);
            photo.SizeMode = PictureBoxSizeMode.Zoom;
            photo.Image = LoadImage(photoPath);
            card.Controls.Add(photo);
        }
        else
        {
            // Missing photo: grey placeholder, the app does not crash
            Label placeholder = new Label();
            placeholder.SetBounds(photoX, 15, photoSize, photoSize);
            placeholder.BackColor = Color.Gray;
            placeholder.ForeColor = Color.White;
            placeholder.Font = new Font("Segoe UI", 10f, FontStyle.Italic);
            placeholder.TextAlign = ContentAlignment.MiddleCenter;
            placeholder.Text = "(no photo)";
            card.Controls.Add(placeholder);
        }

        Label name = new Label();
        name.SetBounds(5, photoSize + 25, width - 10, 45);
        name.Font = new Font("Segoe UI", 10.5f, FontStyle.Bold);
        name.ForeColor = Color.White;
        name.TextAlign = ContentAlignment.MiddleCenter;
        name.Text = member.Name;
        card.Controls.Add(name);

        Label role = new Label();
        role.SetBounds(5, photoSize + 72, width - 10, height - photoSize - 80);
        role.Font = new Font("Segoe UI", 9f, FontStyle.Regular);
        role.ForeColor = Color.Gainsboro;
        role.TextAlign = ContentAlignment.TopCenter;
        role.Text = member.Role;
        card.Controls.Add(role);

        // Clicking anywhere on the card closes the splash
        card.Click += (sender, e) => CloseSplash();
        foreach (Control child in card.Controls)
        {
            child.Click += (sender, e) => CloseSplash();
        }

        return card;
    }

    /// <summary>Loads an image without keeping the file locked.</summary>
    private static Image LoadImage(string path)
    {
        using Image original = Image.FromFile(path);
        return new Bitmap(original);
    }

    /// <summary>Assets are copied next to the .exe by the .csproj (Assets\**).</summary>
    private static string AssetPath(string file)
    {
        return Path.Combine(AppContext.BaseDirectory, "Assets", file);
    }

    /// <summary>Frees the timer and images.</summary>
    protected override void Dispose(bool disposing)
    {
        if (disposing)
        {
            _timer.Dispose();
            _logo.Image?.Dispose();
            BackgroundImage?.Dispose();
            foreach (Control card in _cardsPanel.Controls)
            {
                foreach (Control child in card.Controls)
                {
                    if (child is PictureBox photo)
                    {
                        photo.Image?.Dispose();
                    }
                }
            }
        }
        base.Dispose(disposing);
    }
}