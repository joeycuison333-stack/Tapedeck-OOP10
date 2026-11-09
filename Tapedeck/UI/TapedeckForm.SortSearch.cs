namespace mediaPlayerPrj;

/// <summary>
/// (sorting from menu and column headers, search box).
/// </summary>
public partial class TapedeckForm
{
    /// <summary>
    /// Sorts the playlist using insertion sort while keeping the currently playing item.
    /// </summary>
    /// <param name="key">The property used to sort the playlist.</param>
    private void SortLibrary(SortKey key)
    {
        if (Playlist.Count < 2)
        {
            return;
        }

        List<MediaItem> sortedPlaylist = new List<MediaItem>(Playlist);

        LibrarySorter.InsertionSort(sortedPlaylist, key);

        SetPlaylist(sortedPlaylist);
        RefreshListView();
        SaveLibraryToFile();

        SetStatus("Sorted by " + key + " (insertion sort).");
    }

    /// <summary>
    /// Sorts the playlist when a column header is clicked.
    /// </summary>
    private void listView1_ColumnClick(object sender, ColumnClickEventArgs e)
    {
        switch (e.Column)
        {
            case TitleColumn:
                SortLibrary(SortKey.Title);
                break;

            case ArtistColumn:
                SortLibrary(SortKey.Artist);
                break;

            case ExtraColumn:
                SortLibrary(SortKey.AlbumOrResolution);
                break;

            case DurationColumn:
                SortLibrary(SortKey.Duration);
                break;

            case RatingColumn:
                SortLibrary(SortKey.Rating);
                break;
        }
    }
    /// <summary>
    /// Searches the library by exact title first, then by partial title or artist.
    /// </summary>
    /// <param name="text">The title or artist text to search for.</param>
    public void SearchLibrary(string text)
    {
        text = text?.Trim() ?? string.Empty;

        if (string.IsNullOrWhiteSpace(text))
        {
            SetStatus("Enter a title or artist to search.");
            return;
        }

        if (Playlist.Count == 0)
        {
            SetStatus("The library is empty.");
            return;
        }

        SortLibrary(SortKey.Title);

        int index = LibrarySorter.BinarySearchByTitle(Playlist, text);
        string method = "binary search";

        if (index == -1)
        {
            index = LibrarySorter.FindFirstContaining(Playlist, text);
            method = "partial-match search";
        }

        if (index == -1)
        {
            SetStatus("No match for \"" + text + "\".");
            return;
        }

        SelectRow(index);
        listView1.Focus();

        SetStatus("Found \"" + text + "\" using " + method + ".");
    }

    private void btnSearch_Click(object sender, EventArgs e) { SearchLibrary(txtSearch.Text); }

    /// <summary>
    /// Runs the library search when the Enter key is pressed.
    /// </summary>
    private void txtSearch_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            SearchLibrary(txtSearch.Text);
        }
    }
}
