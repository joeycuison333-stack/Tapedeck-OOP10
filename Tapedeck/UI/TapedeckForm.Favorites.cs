namespace mediaPlayerPrj;

/// <summary>
/// PLACEHOLDER pushed by Joey so the project builds. GENEVIEVE replaces this whole file
/// (favorites, star ratings, the ♥ ★ column text).
/// </summary>
public partial class TapedeckForm
{
    /// <summary>Placeholder: favorites are not available yet.</summary>
    public void ToggleFavorite() { SetStatus("Favorites: coming soon (Genevieve)."); }

    /// <summary>Placeholder: ratings are not available yet.</summary>
    public void SetRating(int stars) { }

    private void cboRating_SelectedIndexChanged(object sender, EventArgs e) { }

    private void btnFavorite_Click(object sender, EventArgs e) { ToggleFavorite(); }

    private static string FavoriteText(MediaItem item) { return string.Empty; }
}
