using System.Text;

namespace mediaPlayerPrj;

/// <summary>
/// Saves favorites and star ratings (FR8) in Favorites.txt, one line per item:
/// FilePath|1 or 0|rating. Only items that are a favorite or have a rating are written.
/// File I/O only, no UI. Callers must catch <see cref="IOException"/> and
/// <see cref="UnauthorizedAccessException"/>. Owner: Heaven.
/// </summary>
public static class FavoritesStore
{
    /// <summary>Name of the favorites file.</summary>
    public const string FileName = "Favorites.txt";

    private const char Separator = '|';
    private const string FavoriteFlag = "1";
    private const string NotFavoriteFlag = "0";

    /// <summary>Full path of Favorites.txt next to the program.</summary>
    public static string DefaultPath
    {
        get { return Path.Combine(AppContext.BaseDirectory, FileName); }
    }

    /// <summary>
    /// Writes "FilePath|1or0|rating" (UTF-8) for every item that is a favorite or rated.
    /// Callers must catch <see cref="IOException"/> and <see cref="UnauthorizedAccessException"/>.
    /// </summary>
    /// <param name="path">Full path of Favorites.txt.</param>
    /// <param name="items">The playlist.</param>
    public static void Save(string path, List<MediaItem> items)
    {
        List<string> lines = new List<string>();
        foreach (MediaItem item in items)
        {
            if (item.IsFavorite || item.Rating > 0)
                lines.Add(ToLine(item));
        }
        File.WriteAllLines(path, lines, Encoding.UTF8);
    }

    // One Favorites.txt line, for example "C:\Music\a.mp3|1|4".
    private static string ToLine(MediaItem item)
    {
        string flag = item.IsFavorite ? FavoriteFlag : NotFavoriteFlag;
        return item.FilePath + Separator + flag + Separator + item.Rating;
    }

    /// <summary>Placeholder: applies nothing yet.</summary>
    public static void Apply(string path, List<MediaItem> items, out int skippedLines)
    {
        skippedLines = 0;
    }
}
