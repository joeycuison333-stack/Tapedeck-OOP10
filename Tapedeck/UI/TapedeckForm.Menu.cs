namespace mediaPlayerPrj;

/// <summary>Menu strip with keyboard shortcuts (FR10). Every item calls the same method as its button. Owner: Joey Cuison.</summary>
public partial class TapedeckForm
{
    private void BuildMenu()
    {
        menuMain = new MenuStrip();

        ToolStripMenuItem fileMenu = new ToolStripMenuItem("&File");
        fileMenu.DropDownItems.Add(CreateItem("&Add Files...", Keys.Control | Keys.O, btnAdd_Click));
        fileMenu.DropDownItems.Add(CreateItem("Add &Folder...", Keys.Control | Keys.Shift | Keys.O, btnAddFolder_Click));
        fileMenu.DropDownItems.Add(new ToolStripSeparator());
        fileMenu.DropDownItems.Add(CreateItem("&Save Library", Keys.Control | Keys.S, (s, e) => SaveWithStatus()));
        fileMenu.DropDownItems.Add(CreateItem("&Reload Library", Keys.Control | Keys.Shift | Keys.L, (s, e) => ReloadLibrary()));
        fileMenu.DropDownItems.Add(CreateItem("&Open Library in Notepad", Keys.Control | Keys.L, btnOpenNotepad_Click));
        fileMenu.DropDownItems.Add(new ToolStripSeparator());
        ToolStripMenuItem exitItem = CreateItem("E&xit", Keys.None, (s, e) => Close());
        exitItem.ShortcutKeyDisplayString = "Alt+F4"; // Windows already handles Alt+F4
        fileMenu.DropDownItems.Add(exitItem);

        ToolStripMenuItem playMenu = new ToolStripMenuItem("&Playback");
        playMenu.DropDownItems.Add(CreateItem("&Play", Keys.F5, btnPlay_Click));
        playMenu.DropDownItems.Add(CreateItem("P&ause", Keys.F6, btnPause_Click));
        playMenu.DropDownItems.Add(CreateItem("&Stop", Keys.F7, btnStop_Click));
        playMenu.DropDownItems.Add(CreateItem("&Next", Keys.Control | Keys.Right, btnNext_Click));
        playMenu.DropDownItems.Add(CreateItem("Pre&vious", Keys.Control | Keys.Left, btnPrevious_Click));
        playMenu.DropDownItems.Add(new ToolStripSeparator());
        playMenu.DropDownItems.Add(CreateItem("Volume &Up", Keys.Control | Keys.Up, (s, e) => ChangeVolume(VolumeStep)));
        playMenu.DropDownItems.Add(CreateItem("Volume &Down", Keys.Control | Keys.Down, (s, e) => ChangeVolume(-VolumeStep)));
        playMenu.DropDownItems.Add(new ToolStripSeparator());

        shuffleItem = new ToolStripMenuItem("S&huffle");
        shuffleItem.ShortcutKeys = Keys.Control | Keys.H;
        shuffleItem.CheckOnClick = true; // shows a check mark when on
        shuffleItem.CheckedChanged += (s, e) => SetShuffle(shuffleItem.Checked);
        playMenu.DropDownItems.Add(shuffleItem);

        repeatItem = new ToolStripMenuItem(RepeatText(Repeat));
        repeatItem.ShortcutKeys = Keys.Control | Keys.R; // each press cycles Off, All, One
        repeatItem.Click += (s, e) => CycleRepeat();
        playMenu.DropDownItems.Add(repeatItem);

        ToolStripMenuItem libraryMenu = new ToolStripMenuItem("&Library");
        libraryMenu.DropDownItems.Add(CreateItem("Sort by &Title", Keys.Control | Keys.D1, (s, e) => SortLibrary(SortKey.Title)));
        libraryMenu.DropDownItems.Add(CreateItem("Sort by &Artist", Keys.Control | Keys.D2, (s, e) => SortLibrary(SortKey.Artist)));
        libraryMenu.DropDownItems.Add(CreateItem("Sort by A&lbum / Resolution", Keys.Control | Keys.D3, (s, e) => SortLibrary(SortKey.AlbumOrResolution)));
        libraryMenu.DropDownItems.Add(CreateItem("Sort by &Duration", Keys.Control | Keys.D4, (s, e) => SortLibrary(SortKey.Duration)));
        libraryMenu.DropDownItems.Add(CreateItem("Sort by &Rating", Keys.Control | Keys.D5, (s, e) => SortLibrary(SortKey.Rating)));
        libraryMenu.DropDownItems.Add(new ToolStripSeparator());
        libraryMenu.DropDownItems.Add(CreateItem("&Search...", Keys.Control | Keys.F, (s, e) => txtSearch.Focus()));
        libraryMenu.DropDownItems.Add(CreateItem("Toggle &Favorite", Keys.Control | Keys.D, btnFavorite_Click));
        ToolStripMenuItem removeItem = CreateItem("Re&move Selected", Keys.None, btnRemove_Click);
        removeItem.ShortcutKeyDisplayString = "Delete"; // handled by the playlist's KeyDown
        libraryMenu.DropDownItems.Add(removeItem);

        ToolStripMenuItem helpMenu = new ToolStripMenuItem("&Help");
        helpMenu.DropDownItems.Add(CreateItem("&Keyboard Shortcuts", Keys.Control | Keys.K, (s, e) => ShowShortcuts()));
        helpMenu.DropDownItems.Add(CreateItem("&About", Keys.F1, (s, e) => ShowAbout()));

        menuMain.Items.AddRange(new ToolStripItem[] { fileMenu, playMenu, libraryMenu, helpMenu });
        menuMain.Dock = DockStyle.None;
        menuMain.AutoSize = false;
        MainMenuStrip = menuMain;
        Controls.Add(menuMain);
        PlaceMenu();
        pnlHeader.SizeChanged += (s, e) => PlaceMenu(); // Windows display scaling resizes the title bar
    }

    // The menu sits right under the custom title bar, like a classic desktop window. It is added
    // in code after the designer layout was scaled for the display (125%, 150%...), so its position
    // and height come from the scaled title bar instead of fixed pixels. Otherwise the taller title
    // bar would cover the menu and swallow its clicks.
    private void PlaceMenu()
    {
        float scale = pnlHeader.Height / (float)Theme.TitleBarHeight;
        menuMain.Location = new Point(0, pnlHeader.Bottom);
        menuMain.Size = new Size(ClientSize.Width, (int)Math.Round(Theme.MenuHeight * scale));
        menuMain.BringToFront(); // nothing may cover the menu
    }

    // One place that creates every menu item, so the shortcut code is not repeated.
    private static ToolStripMenuItem CreateItem(string text, Keys shortcut, EventHandler handler)
    {
        ToolStripMenuItem item = new ToolStripMenuItem(text);
        item.ShortcutKeys = shortcut;
        item.Click += handler;
        return item;
    }

    private void ShowAbout()
    {
        using (SplashForm about = new SplashForm())
        {
            about.ShowDialog(this);
        }
    }

    private void ShowShortcuts()
    {
        MessageBox.Show(
            "Add files: Ctrl+O    Add folder: Ctrl+Shift+O\n" +
            "Save library: Ctrl+S    Reload: Ctrl+Shift+L    Notepad: Ctrl+L\n\n" +
            "Play: F5    Pause: F6    Stop: F7\n" +
            "Next: Ctrl+Right    Previous: Ctrl+Left\n" +
            "Volume up/down: Ctrl+Up / Ctrl+Down\n" +
            "Shuffle: Ctrl+H    Repeat (Off/All/One): Ctrl+R\n\n" +
            "Sort by title, artist, album/resolution, duration, rating: Ctrl+1 to Ctrl+5\n" +
            "Search: Ctrl+F    Favorite: Ctrl+D    Remove: Delete\n" +
            "About: F1",
            "Keyboard shortcuts", MessageBoxButtons.OK, MessageBoxIcon.Information);
    }
}
