namespace mediaPlayerPrj;

/// <summary>An mp3 file. Adds the album name.</summary>
public class AudioItem : MediaItem
{
    /// <summary>Album used when the file has no album tag.</summary>
    public const string UnknownAlbum = "Unknown Album";

    private string _album;

    /// <summary>Album name. A blank album becomes "Unknown Album".</summary>
    public string Album
    {
        get { return _album; }
        set { _album = string.IsNullOrWhiteSpace(value) ? UnknownAlbum : value.Trim(); }
    }

    /// <summary>Creates an audio item.</summary>
    public AudioItem(string title, string artist, TimeSpan duration, string filePath, string album)
        : base(title, artist, duration, filePath)
    {
        Album = album;
    }

    /// <inheritdoc/>
    public override string MediaType { get { return "Audio"; } }

    /// <inheritdoc/>
    public override string Extra { get { return Album; } }

    /// <inheritdoc/>
    public override string ExtraLabel { get { return "Album"; } }
}
