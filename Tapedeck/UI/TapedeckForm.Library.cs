using System.ComponentModel;
using System.Diagnostics;

namespace mediaPlayerPrj;

/// <summary>
/// Library part of the form (FR3, FR7): load Library.txt and Favorites.txt at start-up, save,
/// remove items, reload, open the library in Notepad and count missing files.
/// The file work itself is done by LibraryStore and FavoritesStore. Owner: Heaven.
/// </summary>
public partial class TapedeckForm
{
    private const string EmptyLibraryStatus = "The library is empty. Add files with Ctrl+O.";
    private const string SaveHint = "\n\nIf the program is in a protected folder such as Program Files, move it to Documents and try again.";

    // Remove button, Library > Remove Selected and the Delete key.
    private void btnRemove_Click(object sender, EventArgs e)
    {
        int index = SelectedIndex;
        if (index < 0)
        {
            SetStatus("Select an item to remove first.");
            return;
        }

        MediaItem removed = Playlist[index];
        KeepCurrentIndexAfterRemove(index);
        Playlist.RemoveAt(index);
        RefreshListView();
        SelectRow(Math.Min(index, Playlist.Count - 1)); // so Delete can be pressed again

        if (TrySaveLibrary())
            SetStatus("Removed: " + removed);
    }

    // CurrentIndex must keep pointing at the playing item after the list gets shorter.
    private void KeepCurrentIndexAfterRemove(int index)
    {
        if (index == CurrentIndex)
        {
            Stop(); // never keep playing an item that is no longer in the list
            CurrentIndex = -1;
            ShowNowPlaying(null);
        }
        else if (index < CurrentIndex)
        {
            CurrentIndex--; // the playing item moved up one row
        }
    }

    // Start-up and reload: a missing, unreadable or damaged file never stops the program.
    private void LoadLibraryFromFile()
    {
        List<MediaItem> items = ReadLibraryFile(out int skippedLibrary);
        int skippedFavorites = ApplyFavoritesFile(items);

        CurrentIndex = -1; // nothing is playing after a load
        SetPlaylist(items);
        RefreshListView();

        ReportSkippedLines(LibraryStore.FileName, skippedLibrary);
        ReportSkippedLines(FavoritesStore.FileName, skippedFavorites);
        SetStatus(LoadedStatus());
    }

    // Reads Library.txt. On a read error the library starts empty and the user is told why.
    private List<MediaItem> ReadLibraryFile(out int skippedLines)
    {
        skippedLines = 0;
        try
        {
            return LibraryStore.Load(LibraryStore.DefaultPath, out skippedLines);
        }
        catch (IOException ex)
        {
            ShowError(LibraryStore.FileName + " could not be read, so the library starts empty.\n\n" + ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            ShowError("No permission to read " + LibraryStore.FileName + ", so the library starts empty.\n\n" + ex.Message);
        }
        return new List<MediaItem>();
    }

    // Applies Favorites.txt. On a read error the items keep no favorites, but stay loaded.
    private int ApplyFavoritesFile(List<MediaItem> items)
    {
        try
        {
            FavoritesStore.Apply(FavoritesStore.DefaultPath, items, out int skippedLines);
            return skippedLines;
        }
        catch (IOException ex)
        {
            ShowError(FavoritesStore.FileName + " could not be read, so favorites and ratings were not loaded.\n\n" + ex.Message);
        }
        catch (UnauthorizedAccessException ex)
        {
            ShowError("No permission to read " + FavoritesStore.FileName + ", so favorites and ratings were not loaded.\n\n" + ex.Message);
        }
        return 0;
    }

    private void ReportSkippedLines(string fileName, int skippedLines)
    {
        if (skippedLines > 0)
            ShowError(skippedLines + " damaged line(s) in " + fileName + " were skipped.\nThe rest of the library was loaded.");
    }

    // "Loaded 12 item(s)..." plus how many media files are missing, or the empty-library hint.
    private string LoadedStatus()
    {
        if (Playlist.Count == 0) return EmptyLibraryStatus;

        string status = "Loaded " + Playlist.Count + " item(s) from " + LibraryStore.FileName + ".";
        int missing = CountMissingFiles();
        if (missing > 0) status += " " + missing + " file(s) not found (shown greyed out).";
        return status;
    }

    // File > Reload Library (Ctrl+Shift+L): reads the files again, for example after editing in
    // Notepad. It does not save first, so the changes made in Notepad are kept.
    private void ReloadLibrary()
    {
        Stop();
        ShowNowPlaying(null);
        LoadLibraryFromFile();
    }

    // Used when the form closes and after adding files; errors are shown, never thrown.
    private void SaveLibraryToFile()
    {
        TrySaveLibrary();
    }

    // File > Save Library (Ctrl+S).
    private void SaveWithStatus()
    {
        if (TrySaveLibrary())
            SetStatus("Saved " + Playlist.Count + " item(s) to " + LibraryStore.FileName + ".");
    }

    // Saves Library.txt and Favorites.txt. Returns false (after telling the user) when saving failed.
    private bool TrySaveLibrary()
    {
        try
        {
            LibraryStore.Save(LibraryStore.DefaultPath, Playlist);
            FavoritesStore.Save(FavoritesStore.DefaultPath, Playlist);
            return true;
        }
        catch (IOException ex)
        {
            ShowError("The library could not be saved.\n\n" + ex.Message + SaveHint);
        }
        catch (UnauthorizedAccessException ex)
        {
            ShowError("No permission to save the library.\n\n" + ex.Message + SaveHint);
        }
        return false;
    }

    // File > Open Library in Notepad (Ctrl+L). Saves first so Notepad shows the current list.
    private void btnOpenNotepad_Click(object sender, EventArgs e)
    {
        if (!TrySaveLibrary()) return;

        try
        {
            ProcessStartInfo startInfo = new ProcessStartInfo(NotepadExe, "\"" + LibraryStore.DefaultPath + "\"");
            startInfo.UseShellExecute = false;
            using (Process.Start(startInfo)) { } // we do not wait for Notepad to close
            SetStatus(LibraryStore.FileName + " opened in Notepad. After saving your changes there, use File > Reload Library (Ctrl+Shift+L).");
        }
        catch (Win32Exception ex)
        {
            ShowError("Notepad could not be started.\n\n" + ex.Message);
        }
        catch (InvalidOperationException ex)
        {
            ShowError("Notepad could not be started.\n\n" + ex.Message);
        }
    }

    // How many items point to a file that was moved or deleted (FR7). Those rows are greyed out.
    private int CountMissingFiles()
    {
        int missing = 0;
        foreach (MediaItem item in Playlist)
        {
            if (!File.Exists(item.FilePath)) missing++;
        }
        return missing;
    }
}
