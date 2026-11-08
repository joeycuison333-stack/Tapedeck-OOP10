namespace mediaPlayerPrj;

/// <summary>Playback overrides, controls, shuffle and repeat (FR4, FR13), volume, seek and elapsed time (FR11). Owner: Joey Cuison.</summary>
public partial class TapedeckForm
{
    /// <summary>Plays through the base player, then swaps the Play button for Pause.</summary>
    public override void Play()
    {
        base.Play(); // reuse inherited behaviour
        UpdatePlayPauseButtons();
    }

    /// <summary>Pauses through the base player, then shows the Play button again.</summary>
    public override void Pause()
    {
        base.Pause();
        UpdatePlayPauseButtons();
    }

    /// <summary>Stops through the base player and resets the time display.</summary>
    public override void Stop()
    {
        base.Stop();
        lblElapsed.Text = ZeroTime;
        trackSeek.Value = 0;
        UpdatePlayPauseButtons();
        RefreshListView();
    }

    /// <summary>Fills the Now Playing area and highlights the row.</summary>
    protected override void OnNowPlayingChanged(MediaItem item)
    {
        ShowNowPlaying(item);
        RefreshListView();
        SelectRow(CurrentIndex);
        Text = item + " - " + TeamInfo.AppTitle; // taskbar text
        SetStatus("Now playing: " + item);
    }

    private void btnPlay_Click(object sender, EventArgs e)
    {
        int selected = SelectedIndex;
        if (IsPlaying && (selected < 0 || selected == CurrentIndex)) return; // already playing
        if (selected >= 0 && selected != CurrentIndex) CurrentIndex = selected;
        Play();
    }

    private void btnPause_Click(object sender, EventArgs e) { Pause(); }

    private void btnStop_Click(object sender, EventArgs e) { Stop(); }

    private void btnNext_Click(object sender, EventArgs e) { Next(); }

    private void btnPrevious_Click(object sender, EventArgs e) { Previous(); }

    private void btnShuffle_Click(object sender, EventArgs e)
    {
        shuffleItem.Checked = !shuffleItem.Checked; // the menu item keeps the state in sync
    }

    private void btnRepeat_Click(object sender, EventArgs e) { CycleRepeat(); }

    private void UpdatePlayPauseButtons()
    {
        btnPlay.Visible = !IsPlaying;
        btnPause.Visible = IsPlaying;
    }

    private void SetShuffle(bool enabled)
    {
        ShuffleEnabled = enabled;
        UpdateModeButtons();
        SetStatus(enabled ? "Shuffle on" : "Shuffle off");
    }

    /// <summary>Cycles the repeat mode Off → All → One → Off and updates the menu and button.</summary>
    public void CycleRepeat()
    {
        Repeat = NextRepeatMode(Repeat);
        repeatItem.Text = RepeatText(Repeat);
        UpdateModeButtons();
        SetStatus("Repeat: " + Repeat);
    }

    private static RepeatMode NextRepeatMode(RepeatMode mode)
    {
        switch (mode)
        {
            case RepeatMode.Off: return RepeatMode.All;
            case RepeatMode.All: return RepeatMode.One;
            default: return RepeatMode.Off;
        }
    }

    private static string RepeatText(RepeatMode mode)
    {
        return "&Repeat: " + mode;
    }

    // "On" buttons get a pressed-in face colour so the mode is visible without opening the menu.
    private void UpdateModeButtons()
    {
        btnShuffle.BackColor = ShuffleEnabled ? Theme.ToggleOnColor : Theme.CardColor;
        btnRepeat.BackColor = Repeat == RepeatMode.Off ? Theme.CardColor : Theme.ToggleOnColor;
        lblRepeat.Text = Repeat.ToString();
        lblRepeat.ForeColor = Repeat == RepeatMode.Off ? Theme.MutedColor : Theme.AccentAltColor;
    }

    private void trackVolume_ValueChanged(object sender, EventArgs e)
    {
        SetVolume(trackVolume.Value);
        lblVolume.Text = "Vol " + trackVolume.Value;
    }

    private void ChangeVolume(int delta)
    {
        int volume = trackVolume.Value + delta;
        trackVolume.Value = Math.Max(trackVolume.Minimum, Math.Min(trackVolume.Maximum, volume));
    }

    private void trackSeek_MouseDown(object sender, MouseEventArgs e) { _userIsSeeking = true; }

    private void trackSeek_MouseUp(object sender, MouseEventArgs e)
    {
        Seek(trackSeek.Value / (float)SeekScale);
        _userIsSeeking = false;
    }

    // Keyboard arrows on the seek bar.
    private void trackSeek_Scroll(object sender, EventArgs e)
    {
        if (!_userIsSeeking) Seek(trackSeek.Value / (float)SeekScale);
    }

    private void tmrElapsed_Tick(object sender, EventArgs e)
    {
        MediaItem item = CurrentItem;
        if (item == null || (!IsPlaying && !IsPaused))
        {
            lblElapsed.Text = ZeroTime;
            if (!_userIsSeeking) trackSeek.Value = 0;
            return;
        }
        lblElapsed.Text = TimeFormat.ToClock(ElapsedTime) + " / " + item.DurationText;
        if (!_userIsSeeking) trackSeek.Value = (int)(PlaybackPosition * SeekScale);
    }
}
