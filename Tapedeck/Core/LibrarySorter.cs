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
/// Provides comparison, sorting, and searching operations for media items.
/// Uses a hand-written, stable insertion sort and searching algorithms.
/// </summary>
public static class LibrarySorter
{
    /// <summary>
    /// Compares two media items using the selected sorting key.
    /// </summary>
    /// <param name="a">The first media item.</param>
    /// <param name="b">The second media item.</param>
    /// <param name="key">The sorting property to compare.</param>
    /// <returns>A negative, zero, or positive value indicating the comparison result.</returns>
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
    /// <summary>
    /// Finds a media item by its exact title using binary search.
    /// The list must already be sorted by title.
    /// </summary>
    /// <param name="items">The sorted list of media items.</param>
    /// <param name="title">The title to search for.</param>
    /// <returns>The matching item's index, or -1 if not found.</returns>
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
    /// <summary>
    /// Finds the first media item whose title or artist contains the search text.
    /// </summary>
    /// <param name="items">The media items to search.</param>
    /// <param name="text">The partial text to find.</param>
    /// <returns>The first matching item's index, or -1 if not found.</returns>
    public static int FindFirstContaining(List<MediaItem> items, string text)
    {
        if (items == null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            return -1;
        }

        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].Title.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0 ||
                items[i].Artist.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)
            {
                return i;
            }
        }

        return -1;
    }
}
