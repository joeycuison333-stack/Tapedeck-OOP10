namespace mediaPlayerPrj;

/// <summary>
/// PLACEHOLDER pushed by Joey so the project builds. HEAVEN replaces this whole file.
/// Saves and loads Library.txt (Type|Title|Artist|hh:mm:ss|FilePath|Extra).
/// </summary>
public static class LibraryStore
{
    /// <summary>Name of the library file.</summary>
    public const string FileName = "Library.txt";

    /// <summary>Full path of Library.txt next to the program.</summary>
    public static string DefaultPath
    {
        get { return Path.Combine(AppContext.BaseDirectory, FileName); }
    }

    /// <summary>Placeholder: saves nothing yet.</summary>
    public static void Save(string path, List<MediaItem> items) { }

    /// <summary>Placeholder: always returns an empty library.</summary>
    public static List<MediaItem> Load(string path, out int skippedLines)
    {
        skippedLines = 0;
        return new List<MediaItem>();
    }

    /// <summary>Placeholder: parses nothing yet.</summary>
    public static MediaItem ParseLine(string line)
    {
        return null;
    }
}
