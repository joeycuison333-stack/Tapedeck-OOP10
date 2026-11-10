using System.Runtime.InteropServices;

namespace mediaPlayerPrj;

/// <summary>
/// Every skin setting in one place: image file names, colours, fonts and borderless mode.
/// The design can change here without touching the playback logic (Section 3.3).
/// </summary>
public static class Theme
{
    /// <summary>Folder next to the .exe that holds the skin, logo and team photos.</summary>
    public const string AssetsFolder = "Assets";

    /// <summary>Background PNG (window frame, title bar, group boxes), exported from Canva at the form size (980 x 620).</summary>
    public static string BackgroundFile = "tapedeck_skin.png";

    /// <summary>Splash background PNG (cassette shell, label, stripes, reels), 980 x 620.</summary>
    public static string SplashBackgroundFile = "splash_bg.png";

    /// <summary>Square logo (cassette icon) shown on the splash screen and in the title bar.</summary>
    public static string LogoFile = "logo.png";

    /// <summary>Icon for the title bar, taskbar and Alt+Tab.</summary>
    public static string IconFile = "logo.ico";

    /// <summary>True = no Windows title bar; the form codes its own drag, close and minimize.</summary>
    public static bool Borderless = true;

    /// <summary>Pixels of this colour become see-through when TransparencyKey is used.</summary>
    public static Color TransparentColor = Color.Magenta;

    /// <summary>Form size the background was designed for.</summary>
    public static readonly Size FormSize = new Size(980, 620);

    /// <summary>Height of the custom title bar at the top of the window.</summary>
    public const int TitleBarHeight = 46;

    /// <summary>Height of the menu row under the title bar.</summary>
    public const int MenuHeight = 24;

    // Retro cassette palette: charcoal shell, cream label, red / orange / green / teal stripes,
    // brown tape. The skin PNG and splash_bg.png are painted with these same colours.

    /// <summary>Charcoal cassette shell (window frame, splash background).</summary>
    public static Color ShellColor = Color.FromArgb(52, 52, 55);

    /// <summary>Brown of the tape seen through the cassette window.</summary>
    public static Color TapeColor = Color.FromArgb(74, 46, 30);

    /// <summary>The four label stripes, in order: red, orange, green, teal.</summary>
    public static readonly Color[] StripeColors =
    {
        Color.FromArgb(222, 72, 82),
        Color.FromArgb(236, 168, 62),
        Color.FromArgb(167, 199, 88),
        Color.FromArgb(88, 189, 184),
    };

    /// <summary>Window face colour (cream cassette label). Must match the panel fill in the skin PNG.</summary>
    public static Color BackColor = Color.FromArgb(248, 234, 219);

    /// <summary>Face colour of buttons and other raised controls (darker cream).</summary>
    public static Color CardColor = Color.FromArgb(236, 219, 200);

    /// <summary>Main accent: label red, used for section captions and menu borders.</summary>
    public static Color AccentColor = Color.FromArgb(196, 62, 72);

    /// <summary>Second accent (stripe teal) for "on" states such as shuffle and repeat.</summary>
    public static Color AccentAltColor = Color.FromArgb(47, 143, 138);

    /// <summary>Normal text (charcoal ink).</summary>
    public static Color LabelColor = Color.FromArgb(43, 43, 45);

    /// <summary>Less important text (warm brown).</summary>
    public static Color MutedColor = Color.FromArgb(122, 90, 68);

    /// <summary>Text on the charcoal title bar.</summary>
    public static Color TitleTextColor = Color.FromArgb(251, 238, 223);

    /// <summary>Group line on the title bar (stripe orange).</summary>
    public static Color TitleMutedColor = Color.FromArgb(237, 176, 74);

    /// <summary>Playlist background (warm off-white "sunken" list).</summary>
    public static Color ListBackColor = Color.FromArgb(255, 249, 241);

    /// <summary>Playlist text.</summary>
    public static Color ListTextColor = Color.FromArgb(43, 43, 45);

    /// <summary>Row colour of the item that is playing (stripe teal).</summary>
    public static Color NowPlayingRowColor = Color.FromArgb(47, 143, 138);

    /// <summary>Text colour of the row that is playing.</summary>
    public static Color NowPlayingTextColor = Color.FromArgb(255, 255, 255);

    /// <summary>Face colour of a toggle button that is on (light teal, looks pressed in).</summary>
    public static Color ToggleOnColor = Color.FromArgb(190, 227, 223);

    /// <summary>Menu strip background (same cream as the menu row in the skin).</summary>
    public static Color MenuBackColor = Color.FromArgb(248, 234, 219);

    /// <summary>Highlight behind a menu item under the mouse (soft orange).</summary>
    public static Color MenuHighlightColor = Color.FromArgb(245, 204, 133);

    /// <summary>Font family used on the form (classic desktop look).</summary>
    public const string FontName = "Tahoma";

    // Image buttons (PNG, 36 x 36 or 44 x 44, transparent background).
    /// <summary>Play button image.</summary>
    public const string PlayButton = "btn_play.png";
    /// <summary>Pause button image.</summary>
    public const string PauseButton = "btn_pause.png";
    /// <summary>Stop button image.</summary>
    public const string StopButton = "btn_stop.png";
    /// <summary>Next button image.</summary>
    public const string NextButton = "btn_next.png";
    /// <summary>Previous button image.</summary>
    public const string PreviousButton = "btn_prev.png";
    /// <summary>Shuffle button image.</summary>
    public const string ShuffleButton = "btn_shuffle.png";
    /// <summary>Repeat button image.</summary>
    public const string RepeatButton = "btn_repeat.png";
    /// <summary>Close button image.</summary>
    public const string CloseButton = "btn_close.png";
    /// <summary>Minimize button image.</summary>
    public const string MinimizeButton = "btn_min.png";
    /// <summary>Favorite (heart) button image.</summary>
    public const string FavoriteButton = "btn_heart.png";

    /// <summary>
    /// Loads Assets\fileName without locking the file.
    /// Throws FileNotFoundException (an IOException) when missing and ArgumentException when
    /// the file is not a valid image. Callers must catch both and tell the user.
    /// </summary>
    public static Image LoadImage(string fileName)
    {
        string path = GetAssetPath(fileName);
        if (!File.Exists(path))
            throw new FileNotFoundException("Skin file not found: " + path, path);

        using (MemoryStream stream = new MemoryStream(File.ReadAllBytes(path)))
        using (Image image = Image.FromStream(stream))
        {
            return new Bitmap(image); // copy so the stream can close
        }
    }

    /// <summary>Loads Assets\fileName as an icon. Same exceptions as LoadImage.</summary>
    public static Icon LoadIcon(string fileName)
    {
        string path = GetAssetPath(fileName);
        if (!File.Exists(path))
            throw new FileNotFoundException("Icon file not found: " + path, path);
        return new Icon(path);
    }

    /// <summary>Full path of a file inside the Assets folder.</summary>
    public static string GetAssetPath(string fileName)
    {
        return Path.Combine(AppContext.BaseDirectory, AssetsFolder, fileName);
    }

    /// <summary>
    /// Turns a Button into a classic raised (3D) button with an image. When the image is
    /// missing the button keeps its text, so the control still works (the design never
    /// breaks the logic). Returns false when the image could not be loaded.
    /// </summary>
    public static bool StyleImageButton(Button button, string imageFile, Color background)
    {
        StyleClassicButton(button, background);
        try
        {
            button.Image = LoadImage(imageFile);
            button.ImageAlign = ContentAlignment.MiddleCenter;
            button.Text = string.Empty;
            return true;
        }
        catch (IOException)
        {
            return false;
        }
        catch (ArgumentException)
        {
            return false;
        }
    }

    /// <summary>Styles a text button (Add, Search, ...) as a classic raised button.</summary>
    public static void StyleTextButton(Button button)
    {
        StyleClassicButton(button, CardColor);
    }

    // Standard FlatStyle with a solid face colour gives the raised, bevelled look.
    private static void StyleClassicButton(Button button, Color background)
    {
        button.FlatStyle = FlatStyle.Standard;
        button.UseVisualStyleBackColor = false;
        button.BackColor = background;
        button.ForeColor = LabelColor;
        button.Font = new Font(FontName, 8.25F);
        button.Cursor = Cursors.Hand;
    }

    // Win32 calls that let Windows move a borderless window as if its title bar was dragged.
    private const int WmNcLButtonDown = 0xA1;
    private const int HtCaption = 0x2;

    [DllImport("user32.dll")]
    private static extern bool ReleaseCapture();

    [DllImport("user32.dll")]
    private static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);

    /// <summary>Lets the user drag a borderless form by pressing the mouse on 'handle'.</summary>
    public static void EnableDrag(Form form, Control handle)
    {
        handle.MouseDown += (sender, e) =>
        {
            if (e.Button != MouseButtons.Left) return;
            ReleaseCapture();
            SendMessage(form.Handle, WmNcLButtonDown, (IntPtr)HtCaption, IntPtr.Zero);
        };
    }

    /// <summary>Cream menu colours with a soft orange highlight (the menu stays visible so shortcuts work).</summary>
    public sealed class MenuColorTable : ProfessionalColorTable
    {
        /// <inheritdoc/>
        public override Color MenuItemSelected { get { return MenuHighlightColor; } }
        /// <inheritdoc/>
        public override Color MenuItemBorder { get { return AccentColor; } }
        /// <inheritdoc/>
        public override Color MenuBorder { get { return Color.FromArgb(128, 128, 128); } }
        /// <inheritdoc/>
        public override Color ToolStripDropDownBackground { get { return BackColor; } }
        /// <inheritdoc/>
        public override Color ImageMarginGradientBegin { get { return BackColor; } }
        /// <inheritdoc/>
        public override Color ImageMarginGradientMiddle { get { return BackColor; } }
        /// <inheritdoc/>
        public override Color ImageMarginGradientEnd { get { return BackColor; } }
        /// <inheritdoc/>
        public override Color MenuItemSelectedGradientBegin { get { return MenuHighlightColor; } }
        /// <inheritdoc/>
        public override Color MenuItemSelectedGradientEnd { get { return MenuHighlightColor; } }
        /// <inheritdoc/>
        public override Color MenuItemPressedGradientBegin { get { return MenuHighlightColor; } }
        /// <inheritdoc/>
        public override Color MenuItemPressedGradientEnd { get { return MenuHighlightColor; } }
        /// <inheritdoc/>
        public override Color MenuStripGradientBegin { get { return BackColor; } }
        /// <inheritdoc/>
        public override Color MenuStripGradientEnd { get { return BackColor; } }
        /// <inheritdoc/>
        public override Color SeparatorDark { get { return Color.FromArgb(128, 128, 128); } }
        /// <inheritdoc/>
        public override Color SeparatorLight { get { return Color.White; } }
    }
}
