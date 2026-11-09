namespace mediaPlayerPrj;

/// <summary>
/// The group enhancement (FR8): favorites and 1 to 5 star ratings, shown in the ♥ ★ column and
/// the Now Playing area. Saving is done by FavoritesStore (Favorites.txt). Owner: Valencia Genevieve.
/// </summary>
public partial class TapedeckForm
{
    private const string FavoriteMark = "♥";
    private const string StarMark = "★";
    private const string FavoriteButtonText = "♥ Fav";
    private const string UnfavoriteButtonText = "♥ Unfav";

    /// <summary>
    /// Marks or unmarks the selected item (or the playing item when nothing is selected) as a
    /// favorite, then saves.
    /// </summary>
    public void ToggleFavorite()
    {
        MediaItem item = SelectedItem ?? CurrentItem;
        if (item == null)
        {
            SetStatus("Select an item to mark as favorite.");
            return;
        }

        item.IsFavorite = !item.IsFavorite;
        RefreshListView();
        SaveLibraryToFile();
        if (item == CurrentItem) ShowNowPlaying(item);

        btnFavorite.Text = item.IsFavorite ? UnfavoriteButtonText : FavoriteButtonText;
        SetStatus((item.IsFavorite ? "Added to favorites: " : "Removed from favorites: ") + item);
    }

    /// <summary>Sets the star rating (0 = not rated, up to 5) of the selected item, then saves.</summary>
    /// <param name="stars">Number of stars; values outside 0 to 5 are clamped by MediaItem.Rating.</param>
    public void SetRating(int stars)
    {
        MediaItem item = SelectedItem;
        if (item == null)
        {
            SetStatus("Select an item to rate.");
            return;
        }

        item.Rating = stars;
        RefreshListView();
        SaveLibraryToFile();
        if (item == CurrentItem) ShowNowPlaying(item);

        SetStatus(item.Rating == 0 ? "Rating removed: " + item : "Rated " + item.RatingStars + ": " + item);
    }

    // _updatingRating is true while the list selection fills the box, so that does not count as a new rating.
    private void cboRating_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (_updatingRating || SelectedItem == null || cboRating.SelectedIndex < 0) return;
        SetRating(cboRating.SelectedIndex);
    }

    private void btnFavorite_Click(object sender, EventArgs e) { ToggleFavorite(); }

    // Text for the ♥ ★ column, for example "♥ 4★". Empty when neither.
    private static string FavoriteText(MediaItem item)
    {
        string text = item.IsFavorite ? FavoriteMark : string.Empty;
        if (item.Rating > 0) text += (text.Length > 0 ? " " : string.Empty) + item.Rating + StarMark;
        return text;
    }
}
