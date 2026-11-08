namespace mediaPlayerPrj;

/// <summary>Which file types the player accepts and how an item is built from its type name.</summary>
public static class MediaFormats
{
    /// <summary>Audio extension.</summary>
    public const string AudioExtension = ".mp3";

    /// <summary>Video extension.</summary>
    public const string VideoExtension = ".mp4";

    /// <summary>Filter for the Open File dialog.</summary>
    public const string DialogFilter = "Media files (*.mp3;*.mp4)|*.mp3;*.mp4|MP3 audio (*.mp3)|*.mp3|MP4 video (*.mp4)|*.mp4|All files (*.*)|*.*";

    /// <summary>True for .mp3 and .mp4 files (any letter case).</summary>
    public static bool IsSupported(string path)
    {
        string extension = Path.GetExtension(path ?? string.Empty);
        return extension.Equals(AudioExtension, StringComparison.OrdinalIgnoreCase)
            || extension.Equals(VideoExtension, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>True for .mp4 files.</summary>
    public static bool IsVideoFile(string path)
    {
        return Path.GetExtension(path ?? string.Empty).Equals(VideoExtension, StringComparison.OrdinalIgnoreCase);
    }

    /// <summary>
    /// Factory: builds an AudioItem or VideoItem from the type name stored in Library.txt.
    /// This is the only place that chooses a concrete type; the rest of the code uses MediaItem.
    /// Returns null for an unknown type.
    /// </summary>
    public static MediaItem Create(string mediaType, string title, string artist, TimeSpan duration, string filePath, string extra)
    {
        switch ((mediaType ?? string.Empty).Trim())
        {
            case "Audio":
                return new AudioItem(title, artist, duration, filePath, extra);
            case "Video":
                return new VideoItem(title, artist, duration, filePath, extra);
            default:
                return null;
        }
    }
}
