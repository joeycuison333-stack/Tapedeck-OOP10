namespace mediaPlayerPrj;

/// <summary>
/// PLACEHOLDER pushed by Joey so the project builds. AMMAAR replaces this whole file
/// (sorting from menu and column headers, search box).
/// </summary>
public partial class TapedeckForm
{
    private void SortLibrary(SortKey key) { SetStatus("Sorting: coming soon (Ammaar)."); }

    private void listView1_ColumnClick(object sender, ColumnClickEventArgs e) { }

    /// <summary>Placeholder: search is not available yet.</summary>
    public void SearchLibrary(string text) { SetStatus("Search: coming soon (Ammaar)."); }

    private void btnSearch_Click(object sender, EventArgs e) { SearchLibrary(txtSearch.Text); }

    private void txtSearch_KeyDown(object sender, KeyEventArgs e) { }
}
