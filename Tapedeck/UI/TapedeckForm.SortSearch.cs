namespace mediaPlayerPrj;

/// <summary>
/// Sorting and searching in the form (FR6, FR12): sort from the Library menu or by clicking a
/// column header, and search by title or artist. The algorithms live in LibrarySorter.
/// Owner: Ammaar Azmy.
/// </summary>
public partial class TapedeckForm
{
    private const string BinarySearchName = "binary search";
    private const string PartialMatchName = "partial match";

    // Sorts a copy of the playlist with the hand-written insertion sort, then swaps it in
    // through SetPlaylist so CurrentIndex keeps pointing at the playing item.
    private void SortLibrary(SortKey key)
    {
        if (Playlist.Count < 2)
        {
            SetStatus("Add at least two items to sort the library.");
            return;
        }

        List<MediaItem> sorted = new List<MediaItem>(Playlist); // sort a copy
        LibrarySorter.InsertionSort(sorted, key);

        SetPlaylist(sorted); // keeps CurrentIndex on the playing item
        RefreshListView();
        SaveLibraryToFile();
        SetStatus("Sorted by " + key + " (insertion sort).");
    }

    // Columns: 0 Title, 1 Artist, 2 Album / Resolution, 3 Duration, 4 Favorite and rating
    private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        switch (e.Column)
        {
            case TitleColumn: SortLibrary(SortKey.Title); break;
            case ArtistColumn: SortLibrary(SortKey.Artist); break;
            case ExtraColumn: SortLibrary(SortKey.AlbumOrResolution); break;
            case DurationColumn: SortLibrary(SortKey.Duration); break;
            case RatingColumn: SortLibrary(SortKey.Rating); break;
        }
    }

    /// <summary>
    /// Finds an item by title or artist. The library is sorted by title first so an exact title
    /// can be found with binary search; otherwise the first partial match on title or artist is used.
    /// The match is selected in the playlist.
    /// </summary>
    /// <param name="text">The title, or part of a title or artist, to find.</param>
    public void SearchLibrary(string text)
    {
        string query = (text ?? string.Empty).Trim();
        if (query.Length == 0)
        {
            SetStatus("Type a title or artist to search for.");
            return;
        }
        if (Playlist.Count == 0)
        {
            SetStatus(EmptyLibraryStatus);
            return;
        }

        if (Playlist.Count > 1) SortLibrary(SortKey.Title); // binary search needs a list sorted by title

        string method = BinarySearchName;
        int index = LibrarySorter.BinarySearchByTitle(Playlist, query);
        if (index < 0)
        {
            method = PartialMatchName;
            index = LibrarySorter.FindFirstContaining(Playlist, query);
        }

        if (index < 0)
        {
            SetStatus("No match for \"" + query + "\".");
            return;
        }

        SelectRow(index);
        listView1.Focus();
        SetStatus("Found \"" + Playlist[index] + "\" (" + method + "). Press Enter to play it.");
    }

    private void btnSearch_Click(object sender, EventArgs e) { SearchLibrary(txtSearch.Text); }

    // Enter in the search box searches, without the Windows "ding".
    private void txtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter) return;
        e.SuppressKeyPress = true;
        SearchLibrary(txtSearch.Text);
    }
}
