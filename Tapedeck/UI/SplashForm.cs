using System.Drawing.Drawing2D;

namespace mediaPlayerPrj;

/// <summary>
/// Splash screen, drawn as a retro cassette tape.
/// M3: layout (logo, title, group, section).
/// M4: member cards with photo, name and role (placeholder if no photo).
/// M5: closes after about 3 seconds, or on click / key press.
/// The shell, label, stripes, reels and bottom tab are painted in Assets\splash_bg.png;
/// this form only places the text and the member cards on top of it.
/// </summary>
public class SplashForm : Form
{
    private const int SplashMilliseconds = 3000;

    // Layout of splash_bg.png (980 x 620). Change these together with the image.
    private const int DesignWidth = 980;
    private const int DesignHeight = 620;
    private const int CornerRadius = 24;
    private static readonly Rectangle CardsArea = new Rectangle(77, 190, 826, 274);
    private const int CardGap = 14;
    private const int PhotoSize = 130;

    // Polaroid-style member cards on the cream label.
    private static readonly Color CardColor = Color.FromArgb(255, 250, 243);
    private static readonly Color CardBorderColor = Color.FromArgb(214, 196, 176);

    private readonly PictureBox _logo = new PictureBox();
    private readonly Label _title = new Label();
    private readonly Label _group = new Label();
    private readonly Label _hint = new Label();
    private readonly Panel _cardsPanel = new Panel();
    private readonly System.Windows.Forms.Timer _timer = new System.Windows.Forms.Timer();
    private readonly bool _hasArt;

    /// <summary>Builds the splash window.</summary>
    public SplashForm()
    {
        SuspendLayout();

        // Laid out at 96 DPI; Windows scales everything on high-DPI screens.
        AutoScaleDimensions = new SizeF(96F, 96F);
        AutoScaleMode = AutoScaleMode.Dpi;

        Text = TeamInfo.AppTitle;
        FormBorderStyle = FormBorderStyle.None;
        StartPosition = FormStartPosition.CenterScreen;
        ClientSize = new Size(DesignWidth, DesignHeight);
        DoubleBuffered = true;
        KeyPreview = true;

        // Cassette artwork. If the file is missing the splash still works on a plain cream face.
        string background = AssetPath(Theme.SplashBackgroundFile);
        _hasArt = File.Exists(background);
        if (_hasArt)
        {
            BackgroundImage = LoadImage(background);
            BackgroundImageLayout = ImageLayout.Stretch;
            BackColor = Theme.ShellColor;
        }
        else
        {
            BackColor = Theme.BackColor;
        }

        BuildLayout();

        // M5: click or any key closes the splash
        Click += (sender, e) => CloseSplash();
        KeyDown += (sender, e) => CloseSplash();

        // M5: close automatically after 3 seconds
        _timer.Interval = SplashMilliseconds;
        _timer.Tick += (sender, e) => CloseSplash();

        ResumeLayout(false);
    }

    /// <summary>Rounds the window corners like a cassette shell (after DPI scaling).</summary>
    protected override void OnLoad(EventArgs e)
    {
        base.OnLoad(e);

        int diameter = 2 * CornerRadius * ClientSize.Width / DesignWidth;
        Rectangle r = ClientRectangle;
        using (GraphicsPath path = new GraphicsPath())
        {
            path.AddArc(r.X, r.Y, diameter, diameter, 180, 90);
            path.AddArc(r.Right - diameter, r.Y, diameter, diameter, 270, 90);
            path.AddArc(r.Right - diameter, r.Bottom - diameter, diameter, diameter, 0, 90);
            path.AddArc(r.X, r.Bottom - diameter, diameter, diameter, 90, 90);
            path.CloseFigure();
            Region = new Region(path);
        }
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
        // Logo (top left of the cassette label)
        _logo.SetBounds(76, 46, 64, 64);
        _logo.SizeMode = PictureBoxSizeMode.Zoom;
        _logo.BackColor = Color.Transparent;
        string logoPath = AssetPath(TeamInfo.LogoFile);
        if (File.Exists(logoPath))
        {
            _logo.Image = LoadImage(logoPath);
        }
        Controls.Add(_logo);

        // Title, written on the label like a tape name
        _title.Text = TeamInfo.AppTitle.ToUpperInvariant();
        _title.Font = new Font("Arial Black", 30f, FontStyle.Regular);
        _title.ForeColor = Theme.LabelColor;
        _title.BackColor = Color.Transparent;
        _title.AutoSize = false;
        _title.TextAlign = ContentAlignment.MiddleLeft;
        _title.SetBounds(146, 38, 292, 60);
        Controls.Add(_title);

        // Group and section, typewriter style under the title
        _group.Text = (TeamInfo.GroupName + "  |  " + TeamInfo.Section).ToUpperInvariant();
        _group.Font = new Font("Consolas", 10f, FontStyle.Bold);
        _group.ForeColor = Theme.MutedColor;
        _group.BackColor = Color.Transparent;
        _group.AutoSize = false;
        _group.TextAlign = ContentAlignment.MiddleLeft;
        _group.SetBounds(152, 94, 286, 22);
        Controls.Add(_group);

        // Hint on the bottom tab of the cassette
        _hint.Text = "LOADING...\r\nclick or press any key";
        _hint.Font = new Font("Consolas", 9f, FontStyle.Bold);
        _hint.ForeColor = _hasArt ? Theme.BackColor : Theme.MutedColor;
        _hint.BackColor = Color.Transparent;
        _hint.AutoSize = false;
        _hint.TextAlign = ContentAlignment.TopCenter;
        _hint.SetBounds(385, 514, 210, 40);
        Controls.Add(_hint);

        // Member cards (M4), under the four colour stripes
        _cardsPanel.BackColor = Color.Transparent;
        _cardsPanel.Bounds = CardsArea;
        Controls.Add(_cardsPanel);
        BuildCards();

        // Clicking anywhere closes the splash
        foreach (Control control in new Control[] { _logo, _title, _group, _hint, _cardsPanel })
        {
            control.Click += (sender, e) => CloseSplash();
        }
    }

    /// <summary>Adds one card per team member, side by side.</summary>
    private void BuildCards()
    {
        int count = TeamInfo.Members.Length;
        int cardWidth = (_cardsPanel.Width - CardGap * (count - 1)) / count;
        int x = 0;

        for (int i = 0; i < count; i++)
        {
            Color accent = Theme.StripeColors[i % Theme.StripeColors.Length];
            Panel card = CreateCard(TeamInfo.Members[i], cardWidth, _cardsPanel.Height, accent);
            card.Location = new Point(x, 0);
            _cardsPanel.Controls.Add(card);
            x += cardWidth + CardGap;
        }
    }

    /// <summary>Builds one polaroid-style card: colour strip, photo (or placeholder), name and role.</summary>
    private Panel CreateCard(TeamMember member, int width, int height, Color accent)
    {
        Panel card = new Panel();
        card.Size = new Size(width, height);
        card.BackColor = CardColor;
        card.Paint += (sender, e) =>
        {
            using Pen border = new Pen(CardBorderColor);
            e.Graphics.DrawRectangle(border, 0, 0, card.Width - 1, card.Height - 1);
        };

        // Colour strip across the top, matching the label stripes
        Panel strip = new Panel();
        strip.SetBounds(0, 0, width, 8);
        strip.BackColor = accent;
        card.Controls.Add(strip);

        int photoX = (width - PhotoSize) / 2;
        Image portrait = LoadPortrait(AssetPath(member.PhotoFile), PhotoSize);
        if (portrait != null)
        {
            PictureBox photo = new PictureBox();
            photo.SetBounds(photoX, 20, PhotoSize, PhotoSize);
            photo.SizeMode = PictureBoxSizeMode.Zoom;
            photo.Image = portrait;
            card.Controls.Add(photo);
        }
        else
        {
            // Missing or unreadable photo: placeholder, the app does not crash
            Label placeholder = new Label();
            placeholder.SetBounds(photoX, 20, PhotoSize, PhotoSize);
            placeholder.BackColor = Theme.ShellColor;
            placeholder.ForeColor = Theme.BackColor;
            placeholder.Font = new Font("Segoe UI", 10f, FontStyle.Italic);
            placeholder.TextAlign = ContentAlignment.MiddleCenter;
            placeholder.Text = "(no photo)";
            card.Controls.Add(placeholder);
        }

        Label name = new Label();
        name.SetBounds(6, 158, width - 12, 42);
        name.Font = new Font("Segoe UI", 10f, FontStyle.Bold);
        name.ForeColor = Theme.LabelColor;
        name.BackColor = CardColor;
        name.TextAlign = ContentAlignment.MiddleCenter;
        name.Text = member.Name;
        card.Controls.Add(name);

        // Short colour line between the name and the role
        Panel divider = new Panel();
        divider.SetBounds(40, 203, width - 80, 2);
        divider.BackColor = accent;
        card.Controls.Add(divider);

        Label role = new Label();
        role.SetBounds(8, 210, width - 16, height - 214);
        role.Font = new Font("Segoe UI", 8.25f, FontStyle.Regular);
        role.ForeColor = Theme.MutedColor;
        role.BackColor = CardColor;
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

    /// <summary>
    /// Loads a member photo as a square with a thin charcoal frame. Photos of any shape are
    /// cropped to a square (tall photos keep their upper part, where the face usually is).
    /// Returns null when the file is missing or is not a valid image.
    /// </summary>
    private static Image LoadPortrait(string path, int size)
    {
        if (!File.Exists(path))
        {
            return null;
        }

        try
        {
            using Image original = Image.FromFile(path);
            int side = Math.Min(original.Width, original.Height);
            Rectangle source = new Rectangle(
                (original.Width - side) / 2,
                (original.Height - side) / 4,
                side,
                side);

            Bitmap portrait = new Bitmap(size, size);
            using (Graphics g = Graphics.FromImage(portrait))
            {
                g.Clear(Theme.ShellColor);
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                g.DrawImage(original, new Rectangle(2, 2, size - 4, size - 4), source, GraphicsUnit.Pixel);
            }
            return portrait;
        }
        catch (OutOfMemoryException)
        {
            return null; // GDI+ reports a file that is not an image this way
        }
        catch (IOException)
        {
            return null;
        }
        catch (ArgumentException)
        {
            return null;
        }
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
        return Path.Combine(AppContext.BaseDirectory, Theme.AssetsFolder, file);
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
