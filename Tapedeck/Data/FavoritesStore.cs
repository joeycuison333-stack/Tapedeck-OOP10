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

    // Favorites.txt line: FilePath|1or0|rating
    private const int FieldCount = 3;
    private const int PathField = 0;
    private const int FavoriteField = 1;
    private const int RatingField = 2;

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

    /// <summary>
    /// Reads Favorites.txt (UTF-8) and sets IsFavorite and Rating on the matching loaded items.
    /// A missing file means nothing was marked yet. Blank lines are ignored. A line without three
    /// fields or with a rating that is not a number is skipped and counted. A line for a file that
    /// is no longer in the library is ignored. Paths are compared without letter case, like Windows.
    /// Callers must catch <see cref="IOException"/> and <see cref="UnauthorizedAccessException"/>.
    /// </summary>
    /// <param name="path">Full path of Favorites.txt.</param>
    /// <param name="items">The items just loaded from Library.txt.</param>
    /// <param name="skippedLines">How many damaged lines were skipped.</param>
    public static void Apply(string path, List<MediaItem> items, out int skippedLines)
    {
        skippedLines = 0;
        if (!File.Exists(path)) return;

        Dictionary<string, MediaItem> itemsByPath = BuildPathIndex(items);
        foreach (string line in File.ReadAllLines(path, Encoding.UTF8))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            string[] fields = line.Split(Separator);
            if (fields.Length != FieldCount || !int.TryParse(fields[RatingField].Trim(), out int rating))
            {
                skippedLines++;
                continue;
            }

            if (itemsByPath.TryGetValue(fields[PathField].Trim(), out MediaItem item))
            {
                item.IsFavorite = fields[FavoriteField].Trim() == FavoriteFlag;
                item.Rating = rating; // the Rating setter keeps it between 0 and 5
            }
        }
    }

    // Looks up an item by its file path in one step instead of searching the whole list per line.
    private static Dictionary<string, MediaItem> BuildPathIndex(List<MediaItem> items)
    {
        Dictionary<string, MediaItem> itemsByPath = new Dictionary<string, MediaItem>(StringComparer.OrdinalIgnoreCase);
        foreach (MediaItem item in items)
            itemsByPath[item.FilePath] = item;
        return itemsByPath;
    }
}
