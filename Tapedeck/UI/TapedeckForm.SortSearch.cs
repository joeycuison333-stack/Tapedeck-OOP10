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
    public void SearchLibrary(string text) { SetStatus("Search: coming soon (Ammaar)."); }

    private void btnSearch_Click(object sender, EventArgs e) { SearchLibrary(txtSearch.Text); }

    private void txtSearch_KeyDown(object sender, KeyEventArgs e) { }
}
