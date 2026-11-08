namespace mediaPlayerPrj;

/// <summary>Playlist view and Now Playing text (FR5). Owner: Joey Cuison.</summary>
public partial class TapedeckForm
{
    private int SelectedIndex
    {
        get { return listView1.SelectedIndices.Count > 0 ? listView1.SelectedIndices[0] : -1; }
    }

    private MediaItem SelectedItem
    {
        get { return SelectedIndex >= 0 && SelectedIndex < Playlist.Count ? Playlist[SelectedIndex] : null; }
    }

    // Rebuilds the rows from the playlist, so the list always matches the playlist order.
    private void RefreshListView()
    {
        MediaItem selected = SelectedItem;
        listView1.BeginUpdate();
        listView1.Items.Clear();
        for (int i = 0; i < Playlist.Count; i++)
        {
            MediaItem item = Playlist[i];
            ListViewItem row = new ListViewItem(item.Title);
            row.SubItems.Add(item.Artist);
            row.SubItems.Add(item.Extra);
            row.SubItems.Add(item.DurationText);
            row.SubItems.Add(FavoriteText(item));
            row.ToolTipText = item.MediaType + ": " + item.FilePath;
            if (!File.Exists(item.FilePath)) row.ForeColor = Theme.MutedColor;
            if (i == CurrentIndex)
            {
                row.BackColor = Theme.NowPlayingRowColor;
                row.ForeColor = Theme.NowPlayingTextColor;
                if (_boldListFont != null) row.Font = _boldListFont;
            }
            listView1.Items.Add(row);
        }
        listView1.EndUpdate();

        if (selected != null) SelectRow(Playlist.IndexOf(selected));
        lblCount.Text = Playlist.Count + (Playlist.Count == 1 ? " item" : " items");
    }

    private void SelectRow(int index)
    {
        listView1.SelectedIndices.Clear();
        if (index < 0 || index >= listView1.Items.Count) return;
        listView1.Items[index].Selected = true;
        listView1.Items[index].Focused = true;
        listView1.EnsureVisible(index);
    }

    private void ShowNowPlaying(MediaItem item)
    {
        if (item == null)
        {
            lblTitle.Text = NothingPlaying;
            lblArtist.Text = string.Empty;
            lblExtra.Text = string.Empty;
            Text = TeamInfo.AppTitle;
            return;
        }
        lblTitle.Text = item.Title;
        lblArtist.Text = item.Artist;
        lblExtra.Text = item.MediaType + "  •  " + item.ExtraLabel + ": " + item.Extra
                        + (item.IsFavorite ? "  •  ♥ Favorite" : string.Empty)
                        + (item.Rating > 0 ? "  •  " + item.RatingStars : string.Empty);
    }

    private void listView1_DoubleClick(object sender, EventArgs e) { PlaySelected(); }

    private void listView1_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Enter)
        {
            e.SuppressKeyPress = true;
            PlaySelected();
        }
        else if (e.KeyCode == Keys.Delete)
        {
            btnRemove_Click(sender, EventArgs.Empty);
        }
    }

    private void PlaySelected()
    {
        if (SelectedIndex < 0) return;
        CurrentIndex = SelectedIndex;
        Play();
    }

    private void listView1_SelectedIndexChanged(object sender, EventArgs e)
    {
        MediaItem item = SelectedItem;
        _updatingRating = true;
        cboRating.SelectedIndex = item == null ? -1 : item.Rating;
        _updatingRating = false;
        btnFavorite.Text = item != null && item.IsFavorite ? "♥ Unfav" : "♥ Fav";
    }
}
