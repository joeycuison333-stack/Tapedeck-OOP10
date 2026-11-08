namespace mediaPlayerPrj;

/// <summary>An mp4 file. Adds the resolution, for example 1920x1080.</summary>
public class VideoItem : MediaItem
{
    /// <summary>Resolution used when it cannot be read.</summary>
    public const string UnknownResolution = "Unknown Resolution";

    private string _resolution;

    /// <summary>Width x height. A blank value becomes "Unknown Resolution".</summary>
    public string Resolution
    {
        get { return _resolution; }
        set { _resolution = string.IsNullOrWhiteSpace(value) ? UnknownResolution : value.Trim(); }
    }

    /// <summary>Creates a video item.</summary>
    public VideoItem(string title, string artist, TimeSpan duration, string filePath, string resolution)
        : base(title, artist, duration, filePath)
    {
        Resolution = resolution;
    }

    /// <inheritdoc/>
    public override string MediaType { get { return "Video"; } }

    /// <inheritdoc/>
    public override string Extra { get { return Resolution; } }

    /// <inheritdoc/>
    public override string ExtraLabel { get { return "Resolution"; } }
}
