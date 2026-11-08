namespace mediaPlayerPrj;

/// <summary>
/// Abstract base for every item in the library. Holds the properties shared by audio and video.
/// The rest of the program works only through this type (polymorphism).
/// </summary>
public abstract class MediaItem
{
    /// <summary>Title used when the file has no title.</summary>
    public const string UnknownTitle = "Unknown Title";

    /// <summary>Artist used when the file has no artist.</summary>
    public const string UnknownArtist = "Unknown Artist";

    /// <summary>Highest star rating (FR8 enhancement).</summary>
    public const int MaxRating = 5;

    /// <summary>Field separator in Library.txt; it is not allowed inside a field.</summary>
    public const char FieldSeparator = '|';

    private string _title;
    private string _artist;
    private string _filePath;
    private TimeSpan _duration;
    private int _rating;

    /// <summary>Song or video title. A blank title becomes "Unknown Title".</summary>
    public string Title
    {
        get { return _title; }
        set { _title = string.IsNullOrWhiteSpace(value) ? UnknownTitle : value.Trim(); }
    }

    /// <summary>Artist or creator. A blank artist becomes "Unknown Artist".</summary>
    public string Artist
    {
        get { return _artist; }
        set { _artist = string.IsNullOrWhiteSpace(value) ? UnknownArtist : value.Trim(); }
    }

    /// <summary>Total length. A negative length is stored as zero.</summary>
    public TimeSpan Duration
    {
        get { return _duration; }
        set { _duration = value < TimeSpan.Zero ? TimeSpan.Zero : value; }
    }

    /// <summary>Full path of the media file on disk.</summary>
    public string FilePath
    {
        get { return _filePath; }
        set { _filePath = value == null ? string.Empty : value.Trim(); }
    }

    /// <summary>True when the user marked this item as a favorite (FR8).</summary>
    public bool IsFavorite { get; set; }

    /// <summary>Star rating from 0 (not rated) to 5. Values outside the range are clamped.</summary>
    public int Rating
    {
        get { return _rating; }
        set { _rating = Math.Max(0, Math.Min(MaxRating, value)); }
    }

    /// <summary>Duration as hh:mm:ss.</summary>
    public string DurationText
    {
        get { return TimeFormat.ToClock(Duration); }
    }

    /// <summary>File name only, for example "01 - song.mp3".</summary>
    public string FileName
    {
        get { return Path.GetFileName(FilePath); }
    }

    /// <summary>Rating as stars, for example "★★★☆☆". Empty when not rated.</summary>
    public string RatingStars
    {
        get { return Rating == 0 ? string.Empty : new string('★', Rating) + new string('☆', MaxRating - Rating); }
    }

    /// <summary>Sets the shared properties. Validation runs through the property setters.</summary>
    protected MediaItem(string title, string artist, TimeSpan duration, string filePath)
    {
        Title = title;
        Artist = artist;
        Duration = duration;
        FilePath = filePath;
    }

    /// <summary>"Audio" or "Video".</summary>
    public abstract string MediaType { get; }

    /// <summary>Album for audio, resolution for video.</summary>
    public abstract string Extra { get; }

    /// <summary>Caption for the Extra value, for example "Album" or "Resolution".</summary>
    public abstract string ExtraLabel { get; }

    /// <summary>Builds one Library.txt line: Type|Title|Artist|Duration|FilePath|Extra.</summary>
    public virtual string ToFileLine()
    {
        return string.Join(FieldSeparator.ToString(),
            MediaType, Clean(Title), Clean(Artist), Duration.ToString(@"hh\:mm\:ss"), Clean(FilePath), Clean(Extra));
    }

    /// <summary>Shown in lists: "Artist - Title".</summary>
    public override string ToString()
    {
        return Artist + " - " + Title;
    }

    /// <summary>Replaces the separator so a title like "A|B" cannot break the file format.</summary>
    protected static string Clean(string value)
    {
        return (value ?? string.Empty).Replace(FieldSeparator, '/');
    }
}
