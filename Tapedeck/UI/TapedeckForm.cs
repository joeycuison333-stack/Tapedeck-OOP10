namespace mediaPlayerPrj;

/// <summary>
/// The group's player. Inherits playback from BasePlayerForm and adds the retro Tapedeck skin, the
/// library list, menu strip, elapsed time, sorting, searching, shuffle and repeat, and the
/// favorites and ratings enhancement (FR8).
/// Overrides: Play, Pause, Stop, OnNowPlayingChanged, ApplyTheme, SetStatus, ShowError.
/// Unique members: ToggleFavorite, SetRating, SearchLibrary, CycleRepeat.
/// Owner of this file, the designer and the Playback, View, AddMedia and Menu parts: Joey Cuison.
/// </summary>
public partial class TapedeckForm : BasePlayerForm
{
    private const int ElapsedIntervalMs = 500;
    private const int VolumeStep = 10;
    private const int SeekScale = 1000;
    private const string ZeroTime = TimeFormat.Zero + " / " + TimeFormat.Zero;
    private const string NotepadExe = "notepad.exe";
    private const string NothingPlaying = "Nothing playing";

    // Columns: 0 Title, 1 Artist, 2 Album / Resolution, 3 Duration, 4 Favorite and rating
    private const int TitleColumn = 0;
    private const int ArtistColumn = 1;
    private const int ExtraColumn = 2;
    private const int DurationColumn = 3;
    private const int RatingColumn = 4;

    // Share of the list width per column, in the order above (the designed widths 92/70/62/58/40).
    private static readonly int[] ColumnWeights = { 80, 62, 64, 66, 48 };
    private const int ColumnMargin = 4;

    private readonly System.Windows.Forms.Timer tmrElapsed = new System.Windows.Forms.Timer();
    private MenuStrip menuMain;
    private ToolStripMenuItem shuffleItem;
    private ToolStripMenuItem repeatItem;
    private Font _boldListFont;
    private bool _userIsSeeking;
    private bool _updatingRating;

    /// <summary>Builds the form, menu and skin, and connects the video panel.</summary>
    public TapedeckForm() : this(null) { }

    /// <summary>
    /// Builds the form using an already-starting LibVLC engine (see
    /// <see cref="BasePlayerForm.PrepareEngineAsync"/>), so Program.cs can overlap engine start-up
    /// with the splash screen instead of pausing on a blank window after it closes.
    /// </summary>
    public TapedeckForm(Task<LibVLCSharp.Shared.LibVLC> enginePrep) : base(enginePrep)
    {
        InitializeComponent();
        if (IsDesignTime) return;

        BuildMenu();
        ApplyTheme();
        AttachVideo(pnlVideo);
        SetVolume(trackVolume.Value);
        SetUpToolTips();
        listView1.ShowItemToolTips = true;
        listView1.Resize += (s, e) => FitColumns();
        FitColumns();
        // These labels keep the size they are given. At 200% scaling Windows scaled the media
        // line twice, so it grew over the elapsed time and the seek bar (bug B06).
        foreach (Label label in new[] { lblTitle, lblArtist, lblExtra, lblElapsed })
            label.AutoSize = false;
        ClientSizeChanged += (s, e) => PlaceSkinControls();
        PlaceSkinControls();

        // Controls added earlier in the designer sit on top of later ones. At high display
        // scaling (200%) an overlapping label painted over the Add button and the left edge
        // of Add Folder, so the library buttons and rating controls are raised to the top.
        foreach (Control control in new Control[] { btnAdd, btnAddFolder, btnRemove, btnFavorite, lblRating, cboRating })
            control.BringToFront();

        tmrElapsed.Interval = ElapsedIntervalMs;
        tmrElapsed.Tick += tmrElapsed_Tick;
        tmrElapsed.Start();
    }

    /// <summary>Loads Library.txt once the window is visible, so messages appear on top of it.</summary>
    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        if (IsDesignTime) return;
        PlaceSkinControls(); // again, after Windows has finished scaling the window
        LoadLibraryFromFile();
    }

    /// <summary>Status text goes on the form, because the title bar is hidden.</summary>
    protected override void SetStatus(string text)
    {
        lblStatus.Text = text;
    }

    /// <summary>Puts the first line of the error in the status bar, then shows the message box.</summary>
    protected override void ShowError(string message)
    {
        if (lblStatus != null) SetStatus(message.Split('\n')[0]);
        base.ShowError(message);
    }

    /// <summary>All skin work happens here and in Theme.cs, never inside the playback code.</summary>
    protected override void ApplyTheme()
    {
        BackColor = Theme.BackColor;
        ForeColor = Theme.LabelColor;

        try
        {
            BackgroundImage = Theme.LoadImage(Theme.BackgroundFile);
            BackgroundImageLayout = ImageLayout.Stretch;
        }
        catch (IOException ex)
        {
            SetStatus("Skin not loaded, using plain colours: " + ex.Message);
        }
        catch (ArgumentException ex)
        {
            SetStatus("Skin image is damaged, using plain colours: " + ex.Message);
        }

        try
        {
            Icon = Theme.LoadIcon(Theme.IconFile);
            picLogo.Image = Theme.LoadImage(Theme.LogoFile);
        }
        catch (IOException ex)
        {
            SetStatus("Logo not loaded: " + ex.Message);
        }
        catch (ArgumentException ex)
        {
            SetStatus("Logo image is damaged: " + ex.Message);
        }

        if (Theme.Borderless)
        {
            FormBorderStyle = FormBorderStyle.None;
            foreach (Control handle in new Control[] { pnlHeader, picLogo, lblAppTitle, lblGroup })
                Theme.EnableDrag(this, handle); // a borderless form needs its own drag
        }

        StyleLabels();
        StyleInputs();
        StyleButtons();
        StyleMenu();
        lblGroup.Text = TeamInfo.GroupName + " | " + TeamInfo.Section;
        lblAppTitle.Text = TeamInfo.AppTitle;
        UpdateModeButtons();
    }

    private void StyleLabels()
    {
        // Title bar: cream text on the charcoal bar painted in the skin.
        lblAppTitle.BackColor = Color.Transparent;
        lblAppTitle.ForeColor = Theme.TitleTextColor;
        lblGroup.BackColor = Color.Transparent;
        lblGroup.ForeColor = Theme.TitleMutedColor;

        foreach (Label label in new[] { lblTitle, lblArtist, lblElapsed, lblVolume, lblRating, lblStatus, lblCount })
        {
            label.BackColor = Color.Transparent;
            label.ForeColor = Theme.LabelColor;
        }
        foreach (Label label in new[] { lblExtra, lblRepeat })
        {
            label.BackColor = Color.Transparent;
            label.ForeColor = Theme.MutedColor;
        }
        // Group-box captions sit on the etched frame line, so they get the window colour behind them.
        foreach (Label label in new[] { lblNowCaption, lblLibrary })
        {
            label.BackColor = Theme.BackColor;
            label.ForeColor = Theme.AccentColor;
        }
    }

    private void StyleInputs()
    {
        listView1.BackColor = Theme.ListBackColor;
        listView1.ForeColor = Theme.ListTextColor;
        txtSearch.BackColor = Theme.ListBackColor;
        txtSearch.ForeColor = Theme.LabelColor;
        cboRating.BackColor = Theme.ListBackColor;
        cboRating.ForeColor = Theme.LabelColor;
        trackSeek.BackColor = Theme.BackColor;
        trackVolume.BackColor = Theme.BackColor;
        _boldListFont = new Font(listView1.Font, FontStyle.Bold);
    }

    private void StyleButtons()
    {
        Theme.StyleImageButton(btnPlay, Theme.PlayButton, Theme.CardColor);
        Theme.StyleImageButton(btnPause, Theme.PauseButton, Theme.CardColor);
        Theme.StyleImageButton(btnStop, Theme.StopButton, Theme.CardColor);
        Theme.StyleImageButton(btnNext, Theme.NextButton, Theme.CardColor);
        Theme.StyleImageButton(btnPrevious, Theme.PreviousButton, Theme.CardColor);
        Theme.StyleImageButton(btnShuffle, Theme.ShuffleButton, Theme.CardColor);
        Theme.StyleImageButton(btnRepeat, Theme.RepeatButton, Theme.CardColor);
        Theme.StyleImageButton(btnClose, Theme.CloseButton, Theme.CardColor);
        Theme.StyleImageButton(btnMinimize, Theme.MinimizeButton, Theme.CardColor);
        foreach (Button button in new[] { btnAdd, btnAddFolder, btnRemove, btnSearch, btnFavorite })
            Theme.StyleTextButton(button);
    }

    private void StyleMenu()
    {
        menuMain.Renderer = new ToolStripProfessionalRenderer(new Theme.MenuColorTable());
        menuMain.BackColor = Theme.MenuBackColor;
        menuMain.ForeColor = Theme.LabelColor;
        foreach (ToolStripItem top in menuMain.Items)
            StyleMenuItem(top);
    }

    private static void StyleMenuItem(ToolStripItem item)
    {
        item.ForeColor = Theme.LabelColor;
        if (item is ToolStripMenuItem menuItem)
            foreach (ToolStripItem child in menuItem.DropDownItems)
                StyleMenuItem(child);
    }

    private void SetUpToolTips()
    {
        toolTip1.SetToolTip(btnPlay, "Play (F5)");
        toolTip1.SetToolTip(btnPause, "Pause (F6)");
        toolTip1.SetToolTip(btnStop, "Stop (F7)");
        toolTip1.SetToolTip(btnNext, "Next (Ctrl+Right)");
        toolTip1.SetToolTip(btnPrevious, "Previous (Ctrl+Left)");
        toolTip1.SetToolTip(btnShuffle, "Shuffle on/off (Ctrl+H)");
        toolTip1.SetToolTip(btnRepeat, "Repeat Off / All / One (Ctrl+R)");
        toolTip1.SetToolTip(btnAdd, "Add files (Ctrl+O)");
        toolTip1.SetToolTip(btnAddFolder, "Add folder (Ctrl+Shift+O)");
        toolTip1.SetToolTip(btnRemove, "Remove selected item (Delete)");
        toolTip1.SetToolTip(btnFavorite, "Mark or unmark favorite (Ctrl+D)");
        toolTip1.SetToolTip(btnSearch, "Search by title or artist (Ctrl+F)");
        toolTip1.SetToolTip(btnClose, "Close");
        toolTip1.SetToolTip(btnMinimize, "Minimize");
    }

    // Windows display scaling enlarges the list but not its columns, which left the text cut off
    // ("LAN… Un…"). Share the list's width between the columns in the designed proportions instead.
    // The status bar boxes are painted in the skin, which is stretched to the window. Display
    // scaling (125%, 150%, 200%...) moves the designer labels by a slightly different amount, so
    // at the bottom of the window they drifted below the boxes and the text was cut off. Placing
    // the labels from the skin's own coordinates keeps them inside the boxes at any scaling.
    private void PlaceSkinControls()
    {
        float sx = ClientSize.Width / (float)Theme.FormSize.Width;
        float sy = ClientSize.Height / (float)Theme.FormSize.Height;
        if (sx <= 0 || sy <= 0) return; // minimized

        lblStatus.Bounds = ScaleSkinRect(new Rectangle(12, 591, 784, 22), sx, sy);
        lblCount.Bounds = ScaleSkinRect(new Rectangle(810, 591, 158, 22), sx, sy);

        // The Now Playing lines, elapsed time and seek bar are placed the same way (bug B06).
        lblArtist.Bounds = ScaleSkinRect(new Rectangle(36, 460, 380, 22), sx, sy);
        lblExtra.Bounds = ScaleSkinRect(new Rectangle(36, 482, 360, 20), sx, sy);
        lblElapsed.Bounds = ScaleSkinRect(new Rectangle(400, 480, 166, 22), sx, sy);
        trackSeek.Bounds = ScaleSkinRect(new Rectangle(30, 504, 540, 28), sx, sy);

        // The Now Playing labels are transparent: a transparent label paints the window background
        // over any control behind it, so the elapsed time and the seek bar must be in front.
        lblElapsed.BringToFront();
        trackSeek.BringToFront();
    }

    private static Rectangle ScaleSkinRect(Rectangle r, float sx, float sy)
    {
        return Rectangle.FromLTRB(
            (int)Math.Round(r.Left * sx), (int)Math.Round(r.Top * sy),
            (int)Math.Round(r.Right * sx), (int)Math.Round(r.Bottom * sy));
    }

    private void FitColumns()
    {
        // Always leave room for the vertical scroll bar, so adding items never adds a horizontal one.
        int available = listView1.Width - SystemInformation.VerticalScrollBarWidth - ColumnMargin;
        if (available <= 0) return;

        int totalWeight = 0;
        foreach (int weight in ColumnWeights) totalWeight += weight;

        int used = 0;
        for (int i = 1; i < listView1.Columns.Count; i++)
        {
            int width = available * ColumnWeights[i] / totalWeight;
            listView1.Columns[i].Width = width;
            used += width;
        }
        listView1.Columns[TitleColumn].Width = available - used; // the title gets what is left
    }

    private void btnClose_Click(object sender, EventArgs e) { Close(); }

    private void btnMinimize_Click(object sender, EventArgs e) { WindowState = FormWindowState.Minimized; }

    // The player is disposed when the form closes, so stop reading from it first.
    /// <inheritdoc/>
    protected override void OnFormClosing(FormClosingEventArgs e)
    {
        tmrElapsed.Stop();
        if (!IsDesignTime) SaveLibraryToFile();
        base.OnFormClosing(e);
    }

    /// <inheritdoc/>
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        tmrElapsed.Dispose();
        _boldListFont?.Dispose();
        base.OnFormClosed(e);
    }
}
