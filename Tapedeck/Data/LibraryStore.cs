using System.Globalization;
using System.Text;

namespace mediaPlayerPrj;

/// <summary>
/// Saves and loads Library.txt, one item per line: Type|Title|Artist|hh:mm:ss|FilePath|Extra
/// (Extra is the album for audio and the resolution for video). File I/O only, no UI.
/// Callers must catch <see cref="IOException"/> and <see cref="UnauthorizedAccessException"/>.
/// Owner: Heaven.
/// </summary>
public static class LibraryStore
{
    /// <summary>Name of the library file.</summary>
    public const string FileName = "Library.txt";

    private const string TempExtension = ".tmp";

    // Library.txt line: Type|Title|Artist|hh:mm:ss|FilePath|Extra
    private const int FieldCount = 6;
    private const int TypeField = 0;
    private const int TitleField = 1;
    private const int ArtistField = 2;
    private const int DurationField = 3;
    private const int PathField = 4;
    private const int ExtraField = 5;

    /// <summary>Full path of Library.txt next to the program.</summary>
    public static string DefaultPath
    {
        get { return Path.Combine(AppContext.BaseDirectory, FileName); }
    }

    /// <summary>
    /// Writes every item to <paramref name="path"/> (UTF-8). The lines go to a .tmp file first,
    /// which then replaces the real file, so a crash halfway through cannot corrupt the library.
    /// Callers must catch <see cref="IOException"/> and <see cref="UnauthorizedAccessException"/>.
    /// </summary>
    /// <param name="path">Full path of Library.txt.</param>
    /// <param name="items">The playlist to save, in its current order.</param>
    public static void Save(string path, List<MediaItem> items)
    {
        List<string> lines = new List<string>();
        foreach (MediaItem item in items)
            lines.Add(item.ToFileLine());

        string tempPath = path + TempExtension;
        File.WriteAllLines(tempPath, lines, Encoding.UTF8);
        File.Move(tempPath, path, true); // overwrite the old library in one step
    }

    /// <summary>
    /// Reads Library.txt (UTF-8). A missing file is not an error: it means the first run, so the
    /// library starts empty. Blank lines are ignored. A damaged line is skipped and counted, so one
    /// bad line never loses the rest of the library.
    /// Callers must catch <see cref="IOException"/> and <see cref="UnauthorizedAccessException"/>.
    /// </summary>
    /// <param name="path">Full path of Library.txt.</param>
    /// <param name="skippedLines">How many damaged lines were skipped.</param>
    /// <returns>The items in file order; empty when the file does not exist.</returns>
    public static List<MediaItem> Load(string path, out int skippedLines)
    {
        List<MediaItem> items = new List<MediaItem>();
        skippedLines = 0;
        if (!File.Exists(path)) return items; // first run

        foreach (string line in File.ReadAllLines(path, Encoding.UTF8))
        {
            if (string.IsNullOrWhiteSpace(line)) continue;

            MediaItem item = ParseLine(line);
            if (item == null)
                skippedLines++;
            else
                items.Add(item);
        }
        return items;
    }

    /// <summary>
    /// Turns one Library.txt line into an AudioItem or VideoItem through
    /// <see cref="MediaFormats.Create"/>, so this class never checks the media type itself.
    /// Returns null for a damaged line: wrong number of fields, a bad duration, a blank path,
    /// a file type other than mp3/mp4, or an unknown media type.
    /// </summary>
    /// <param name="line">One line in the format Type|Title|Artist|hh:mm:ss|FilePath|Extra.</param>
    /// <returns>The item, or null when the line is damaged.</returns>
    public static MediaItem ParseLine(string line)
    {
        if (string.IsNullOrWhiteSpace(line)) return null;

        string[] fields = line.Split(MediaItem.FieldSeparator);
        if (fields.Length != FieldCount) return null;

        if (!TimeSpan.TryParse(fields[DurationField], CultureInfo.InvariantCulture, out TimeSpan duration))
            return null;

        string filePath = fields[PathField].Trim();
        if (filePath.Length == 0 || !MediaFormats.IsSupported(filePath)) return null;

        return MediaFormats.Create(fields[TypeField], fields[TitleField], fields[ArtistField],
            duration, filePath, fields[ExtraField]);
    }
}
