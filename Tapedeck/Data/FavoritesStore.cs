namespace mediaPlayerPrj;

/// <summary>
/// PLACEHOLDER pushed by Joey so the project builds. HEAVEN replaces this whole file.
/// Saves favorites and ratings in Favorites.txt.
/// </summary>
public static class FavoritesStore
{
    /// <summary>Name of the favorites file.</summary>
    public const string FileName = "Favorites.txt";

    /// <summary>Full path of Favorites.txt next to the program.</summary>
    public static string DefaultPath
    {
        get { return Path.Combine(AppContext.BaseDirectory, FileName); }
    }

    /// <summary>Placeholder: saves nothing yet.</summary>
    public static void Save(string path, List<MediaItem> items) { }

    /// <summary>Placeholder: applies nothing yet.</summary>
    public static void Apply(string path, List<MediaItem> items, out int skippedLines)
    {
        skippedLines = 0;
    }
}
