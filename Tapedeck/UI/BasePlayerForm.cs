using System.ComponentModel;
using LibVLCSharp.Shared;
using LibVLCSharp.WinForms;

namespace mediaPlayerPrj;

/// <summary>
/// Base player that implements IMediaControls with the LibVLCSharp engine.
/// Kept concrete with a parameterless constructor so the Visual Studio designer can open
/// forms that inherit from it. Each group's form inherits from this class and overrides
/// the virtual members and hooks.
/// </summary>
public class BasePlayerForm : Form, IMediaControls
{
    private const int ParseTimeoutMs = 5000;
    private const string EmptyLibraryMessage = "The library is empty. Add some files first.";

    private LibVLC _libVLC;
    private MediaPlayer _player;
    private readonly Random _random = new Random();
    private string _loadedPath;
    private bool _isPaused;

    /// <summary>Video output, drawn by VLC and not by WinForms. Never place controls over it.</summary>
    protected VideoView VideoSurface;

    /// <summary>The playlist. Code works through the MediaItem base type (polymorphism).</summary>
    protected List<MediaItem> Playlist = new List<MediaItem>();

    /// <summary>Index of the current item in Playlist, or -1 when nothing is selected.</summary>
    protected int CurrentIndex = -1;

    /// <summary>True picks a random item next instead of the following one.</summary>
    public bool ShuffleEnabled { get; set; }

    /// <summary>Off stops after the last item, All loops the list, One replays the item.</summary>
    public RepeatMode Repeat { get; set; } = RepeatMode.Off;

    /// <summary>Creates the playback engine. Returns early in design mode so the designer opens.</summary>
    public BasePlayerForm()
    {
        if (IsDesignTime) return;

        try
        {
            Core.Initialize();
            _libVLC = new LibVLC();
            _player = new MediaPlayer(_libVLC);
            _player.EndReached += Player_EndReached;
            _player.EncounteredError += Player_EncounteredError;
            VideoSurface = new VideoView();
            VideoSurface.MediaPlayer = _player;
            VideoSurface.Dock = DockStyle.Fill;
            VideoSurface.BackColor = Color.Black;
        }
        catch (VLCException ex)
        {
            ShowError("The playback engine could not start. Playback is disabled.\n\n" + ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            ShowError("The VLC files were not found next to the program.\n\n" + ex.Message);
        }
    }

    /// <summary>True when the form is being shown in the Visual Studio designer.</summary>
    protected static bool IsDesignTime
    {
        get { return LicenseManager.UsageMode == LicenseUsageMode.Designtime; }
    }

    /// <summary>True when an item is loaded and not paused.</summary>
    protected bool IsPlaying
    {
        get { return _player != null && _loadedPath != null && !_isPaused; }
    }

    /// <summary>True when an item is loaded and paused.</summary>
    protected bool IsPaused
    {
        get { return _loadedPath != null && _isPaused; }
    }

    /// <summary>How long the current item has been playing.</summary>
    protected TimeSpan ElapsedTime
    {
        get
        {
            if (_player == null || _loadedPath == null) return TimeSpan.Zero;
            return TimeSpan.FromMilliseconds(Math.Max(0, _player.Time));
        }
    }

    /// <summary>Position of the current item from 0.0 to 1.0.</summary>
    protected float PlaybackPosition
    {
        get
        {
            if (_player == null || _loadedPath == null) return 0f;
            return Math.Max(0f, Math.Min(1f, _player.Position));
        }
    }

    /// <summary>The item at CurrentIndex, or null.</summary>
    protected MediaItem CurrentItem
    {
        get { return CurrentIndex >= 0 && CurrentIndex < Playlist.Count ? Playlist[CurrentIndex] : null; }
    }

    /// <summary>Puts the video surface inside a panel of the group's form.</summary>
    protected void AttachVideo(Control host)
    {
        if (VideoSurface != null) host.Controls.Add(VideoSurface);
    }

    /// <summary>Plays the current item, resumes it when paused, or reports a missing file.</summary>
    public virtual void Play()
    {
        if (Playlist.Count == 0)
        {
            ShowError(EmptyLibraryMessage);
            return;
        }
        if (CurrentIndex < 0 || CurrentIndex >= Playlist.Count) CurrentIndex = 0;
        if (_player == null)
        {
            SetStatus("The playback engine is not available, so nothing can be played.");
            return;
        }

        MediaItem item = Playlist[CurrentIndex];

        if (_isPaused && string.Equals(_loadedPath, item.FilePath, StringComparison.OrdinalIgnoreCase))
        {
            _player.SetPause(false);
            _isPaused = false;
            SetStatus("Resumed: " + item);
            return;
        }

        if (!File.Exists(item.FilePath))
        {
            if (_loadedPath != null) Stop(); // do not keep showing the old item's time
            SetStatus("File not found: " + item.FilePath);
            ShowError("This file was moved or deleted:\n\n" + item.FilePath);
            return;
        }

        try
        {
            using (Media media = new Media(_libVLC, item.FilePath, FromType.FromPath))
            {
                _player.Play(media);
            }
            _loadedPath = item.FilePath;
            _isPaused = false;
            OnNowPlayingChanged(item);
        }
        catch (VLCException ex)
        {
            ShowError("Could not play " + item.FileName + ".\n\n" + ex.Message);
        }
    }

    /// <summary>Pauses the current item.</summary>
    public virtual void Pause()
    {
        if (_player == null || _loadedPath == null || _isPaused) return;
        _player.SetPause(true);
        _isPaused = true;
        SetStatus("Paused");
    }

    /// <summary>Stops playback and unloads the item.</summary>
    public virtual void Stop()
    {
        if (_player == null) return;
        _player.Stop();
        _loadedPath = null;
        _isPaused = false;
        SetStatus("Stopped");
    }

    /// <summary>Plays the next item, or a random one when shuffle is on. Wraps from last to first.</summary>
    public virtual void Next()
    {
        if (Playlist.Count == 0) return;
        _isPaused = false; // a new item always starts from the beginning
        CurrentIndex = ShuffleEnabled ? PickRandomIndex() : (CurrentIndex + 1) % Playlist.Count;
        Play();
    }

    /// <summary>Plays the previous item in list order. Wraps from first to last.</summary>
    public virtual void Previous()
    {
        if (Playlist.Count == 0) return;
        _isPaused = false;
        CurrentIndex = (CurrentIndex - 1 + Playlist.Count) % Playlist.Count;
        Play();
    }

    /// <summary>Sets the volume from 0 to 100.</summary>
    public virtual void SetVolume(int percent)
    {
        if (_player == null) return;
        _player.Volume = Math.Max(0, Math.Min(100, percent));
    }

    /// <summary>Jumps to a position from 0.0 (start) to 1.0 (end).</summary>
    public virtual void Seek(float position)
    {
        if (_player == null || _loadedPath == null || !_player.IsSeekable) return;
        _player.Position = Math.Max(0f, Math.Min(1f, position));
    }

    /// <summary>
    /// Replaces the playlist (for example with a sorted copy) and keeps CurrentIndex on the
    /// same item, so the playing song stays selected.
    /// </summary>
    protected void SetPlaylist(List<MediaItem> items)
    {
        MediaItem current = CurrentItem;
        Playlist = items;
        CurrentIndex = current == null ? -1 : Playlist.IndexOf(current);
    }

    // Hooks: child forms override these.

    /// <summary>Called after a new item starts. Update the Now Playing area here.</summary>
    protected virtual void OnNowPlayingChanged(MediaItem item) { }

    /// <summary>Applies the skin (see Theme.cs).</summary>
    protected virtual void ApplyTheme() { }

    /// <summary>Shows a short message. Works without a title bar.</summary>
    protected virtual void SetStatus(string text) { }

    /// <summary>Shows an error to the user. Override to change how errors look.</summary>
    protected virtual void ShowError(string message)
    {
        MessageBox.Show(message, TeamInfo.AppTitle, MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    /// <summary>
    /// Reads title, artist, duration and album or resolution through LibVLC and returns an
    /// AudioItem or VideoItem. Missing tags fall back to the file name. If the engine is not
    /// available the item is still created from the file name and extension.
    /// </summary>
    protected async Task<MediaItem> ReadMediaItemAsync(string path)
    {
        string fileTitle = Path.GetFileNameWithoutExtension(path);
        bool isVideo = MediaFormats.IsVideoFile(path);

        if (_libVLC == null)
            return isVideo
                ? new VideoItem(fileTitle, null, TimeSpan.Zero, path, null)
                : new AudioItem(fileTitle, null, TimeSpan.Zero, path, null);

        using (Media media = new Media(_libVLC, path, FromType.FromPath))
        {
            await media.Parse(MediaParseOptions.ParseLocal, ParseTimeoutMs);

            string title = media.Meta(MetadataType.Title);
            if (string.IsNullOrWhiteSpace(title)) title = fileTitle;
            string artist = media.Meta(MetadataType.Artist);
            TimeSpan duration = TimeSpan.FromMilliseconds(Math.Max(0, media.Duration));

            if (isVideo)
                return new VideoItem(title, artist, duration, path, ReadResolution(media));
            return new AudioItem(title, artist, duration, path, media.Meta(MetadataType.Album));
        }
    }

    private static string ReadResolution(Media media)
    {
        foreach (MediaTrack track in media.Tracks)
        {
            if (track.TrackType == TrackType.Video && track.Data.Video.Width > 0)
                return track.Data.Video.Width + "x" + track.Data.Video.Height;
        }
        return null; // VideoItem turns this into "Unknown Resolution"
    }

    // A random index that is not the current one (unless there is only one item).
    private int PickRandomIndex()
    {
        if (Playlist.Count == 1) return 0;
        int index;
        do
        {
            index = _random.Next(Playlist.Count);
        }
        while (index == CurrentIndex);
        return index;
    }

    // VLC raises EndReached on its own thread; move to the UI thread before touching the player.
    private void Player_EndReached(object sender, EventArgs e)
    {
        if (IsHandleCreated && !IsDisposed)
            BeginInvoke(new Action(PlayNextOrStop));
    }

    private void Player_EncounteredError(object sender, EventArgs e)
    {
        if (IsHandleCreated && !IsDisposed)
            BeginInvoke(new Action(() =>
            {
                _loadedPath = null;
                ShowError("The file could not be played. It may be damaged or in an unsupported format.");
            }));
    }

    // Called when an item ends (on the UI thread through BeginInvoke).
    private void PlayNextOrStop()
    {
        _loadedPath = null; // the finished item must be loaded again
        if (Repeat == RepeatMode.One)
            Play(); // same CurrentIndex
        else if (ShuffleEnabled || Repeat == RepeatMode.All || CurrentIndex < Playlist.Count - 1)
            Next();
        else
            Stop(); // last item and repeat is off
    }

    /// <summary>Stops and releases the engine when the form closes.</summary>
    protected override void OnFormClosed(FormClosedEventArgs e)
    {
        if (_player != null)
        {
            _player.EndReached -= Player_EndReached;
            _player.EncounteredError -= Player_EncounteredError;
            _player.Stop();
            VideoSurface?.Dispose();
            _player.Dispose();
            _libVLC?.Dispose();
            _player = null;
        }
        base.OnFormClosed(e);
    }
}
