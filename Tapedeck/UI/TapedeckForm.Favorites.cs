namespace mediaPlayerPrj;

/// <summary>
/// Favorites and ratings partial for TapedeckForm (FR8). Owner: Valencia Genevieve.
/// Handles toggling favorites, assigning 1-5 star ratings, and displaying ratings in the playlist.
/// </summary>
public partial class TapedeckForm
{
    private const string SelectToFavoriteMessage = "Select an item to mark as favorite.";
    private const string SelectToRateMessage = "Select an item to rate.";
    private const string MarkedFavoritePrefix = "Marked as favorite: ";
    private const string RemovedFavoritePrefix = "Removed from favorites: ";
    private const string RatedPrefix = "Rated ";
    private const string RatingClearedPrefix = "Rating cleared: ";
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

    /// <summary>
    /// Sets the star rating (0-5) of the selected media item.
    /// Updates the list view, saves the library, updates Now Playing if currently playing,
    /// and displays a status message with the rating stars.
    /// </summary>
    /// <param name="stars">Star rating between 0 (not rated) and 5.</param>
    public void SetRating(int stars)
    {
        MediaItem item = SelectedItem;
        if (item == null)
        {
            SetStatus(SelectToRateMessage);
            return;
        }

        item.Rating = stars;
        RefreshListView();
        SaveLibraryToFile();

        if (item == CurrentItem)
        {
            ShowNowPlaying(item);
        }

        SetStatus(item.Rating > 0 ? RatedPrefix + item.RatingStars + ": " + item : RatingClearedPrefix + item);
    }

    /// <summary>
    /// Handles changes in the rating dropdown selection.
    /// Returns early if currently updating programmatically or if nothing is selected.
    /// </summary>
    private void cboRating_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_updatingRating || cboRating.SelectedIndex < 0 || SelectedItem == null)
        {
            return;
        }

        SetRating(cboRating.SelectedIndex);
    }

    /// <summary>Event handler for clicking the favorite button.</summary>
    private void btnFavorite_Click(object sender, EventArgs e)
    {
        ToggleFavorite();
    }

    /// <summary>Placeholder: column text for favorites and ratings (implemented in G6).</summary>
    private static string FavoriteText(MediaItem item)
    {
        return string.Empty;
    }
}
