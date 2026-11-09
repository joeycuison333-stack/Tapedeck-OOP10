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
    public static int Compare(MediaItem a, MediaItem b, SortKey key)
    {
        return key switch
        {
            SortKey.Title =>
                StringComparer.OrdinalIgnoreCase.Compare(a.Title, b.Title),

            SortKey.Artist =>
                StringComparer.OrdinalIgnoreCase.Compare(a.Artist, b.Artist),

            SortKey.AlbumOrResolution =>
                StringComparer.OrdinalIgnoreCase.Compare(a.Extra, b.Extra),

            SortKey.Duration =>
                a.Duration.CompareTo(b.Duration),

            SortKey.Rating =>
                b.Rating.CompareTo(a.Rating),

            _ => throw new ArgumentOutOfRangeException(nameof(key))
        };
    }

    /// <summary>
    /// Sorts the media items using a stable, hand-written insertion sort.
    /// Efficient for small or nearly sorted lists and sorts in place.
    /// Best case: O(n). Worst case: O(n²).
    /// </summary>
    public static void InsertionSort(List<MediaItem> items, SortKey key)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        for (int i = 1; i < items.Count; i++)
        {
            MediaItem current = items[i];
            int j = i - 1;

            while (j >= 0 && Compare(items[j], current, key) > 0)
            {
                items[j + 1] = items[j];
                j--;
            }

            items[j + 1] = current;
        }
    }
    public static int BinarySearchByTitle(List<MediaItem> items, string title)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            return -1;
        }

        int low = 0;
        int high = items.Count - 1;

        while (low <= high)
        {
            int middle = low + (high - low) / 2;

            int comparison = StringComparer.OrdinalIgnoreCase.Compare(
                items[middle].Title, title);

            if (comparison == 0)
            {
                return middle;
            }
            else if (comparison < 0)
            {
                low = middle + 1;
            }
            else
            {
                high = middle - 1;
            }
        }

        return -1;
    }
    public static int FindFirstContaining(List<MediaItem> items, string text) { return -1; }
}
