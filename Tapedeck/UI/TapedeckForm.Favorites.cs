namespace mediaPlayerPrj;

/// <summary>
/// Favorites and ratings partial for TapedeckForm (FR8). Owner: Valencia Genevieve.
/// Handles toggling favorites, assigning 1-5 star ratings, and displaying ratings in the playlist.
/// </summary>
public partial class TapedeckForm
{
    private const string SelectToFavoriteMessage = "Select an item to mark as favorite.";
    private const string MarkedFavoritePrefix = "Marked as favorite: ";
    private const string RemovedFavoritePrefix = "Removed from favorites: ";
    private const string FavButtonText = "♥ Fav";
    private const string UnfavButtonText = "♥ Unfav";

    /// <summary>
    /// Toggles the favorite status of the selected item, or the currently playing item if nothing is selected.
    /// Updates the playlist list view, saves the library, updates Now Playing if currently playing,
    /// sets the favorite button text, and displays a status message.
    /// </summary>
    public void ToggleFavorite()
    {
        MediaItem item = SelectedItem ?? CurrentItem;
        if (item == null)
        {
            SetStatus(SelectToFavoriteMessage);
            return;
        }

        item.IsFavorite = !item.IsFavorite;
        RefreshListView();
        SaveLibraryToFile();

        if (item == CurrentItem)
        {
            ShowNowPlaying(item);
        }

        btnFavorite.Text = item.IsFavorite ? UnfavButtonText : FavButtonText;
        SetStatus((item.IsFavorite ? MarkedFavoritePrefix : RemovedFavoritePrefix) + item);
    }

    /// <summary>Placeholder: ratings are not available yet (Genevieve G5).</summary>
    /// <param name="stars">Star rating between 0 and 5.</param>
    public void SetRating(int stars) { }

    private void cboRating_SelectedIndexChanged(object sender, EventArgs e) { }

    private void btnFavorite_Click(object sender, EventArgs e)
    {
        ToggleFavorite();
    }

    private static string FavoriteText(MediaItem item)
    {
        return string.Empty;
    }
}
