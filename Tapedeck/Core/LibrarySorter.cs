namespace mediaPlayerPrj;

/// <summary>What to sort the playlist by. (Final – keep these names.)</summary>
public enum SortKey
{
    /// <summary>Title A to Z.</summary>
    Title,

    /// <summary>Artist A to Z.</summary>
    Artist,

    /// <summary>Album (audio) or resolution (video) A to Z.</summary>
    AlbumOrResolution,

    /// <summary>Shortest to longest.</summary>
    Duration,

    /// <summary>Highest star rating first.</summary>
    Rating
}

/// <summary>
/// PLACEHOLDER pushed by Joey so the project builds. AMMAAR replaces this whole file
/// with the hand-written insertion sort and binary search.
/// </summary>
public static class LibrarySorter
{
    /// <summary>Placeholder: treats every item as equal.</summary>
    public static int Compare(MediaItem a, MediaItem b, SortKey key) { return 0; }

    /// <summary>Placeholder: leaves the list unchanged.</summary>
    public static void InsertionSort(List<MediaItem> items, SortKey key) { }

    /// <summary>Placeholder: never finds anything.</summary>
    public static int BinarySearchByTitle(List<MediaItem> items, string title) { return -1; }

    /// <summary>Placeholder: never finds anything.</summary>
    public static int FindFirstContaining(List<MediaItem> items, string text) { return -1; }
}
