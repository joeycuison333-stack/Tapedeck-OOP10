using LibVLCSharp.Shared;

namespace mediaPlayerPrj;

/// <summary>Adding files and folders to the library (FR1, FR2). Owner: Joey Cuison.</summary>
public partial class TapedeckForm
{
    private async void btnAdd_Click(object sender, EventArgs e)
    {
        using (OpenFileDialog dialog = new OpenFileDialog())
        {
            dialog.Title = "Add media files";
            dialog.Filter = MediaFormats.DialogFilter;
            dialog.Multiselect = true;
            if (dialog.ShowDialog(this) != DialogResult.OK) return;
            await AddFilesAsync(dialog.FileNames);
        }
    }

    private async void btnAddFolder_Click(object sender, EventArgs e)
    {
        using (FolderBrowserDialog dialog = new FolderBrowserDialog())
        {
            dialog.Description = "Choose a folder with mp3 or mp4 files";
            if (dialog.ShowDialog(this) != DialogResult.OK) return;

            List<string> files = new List<string>();
            try
            {
                EnumerationOptions options = new EnumerationOptions { RecurseSubdirectories = true, IgnoreInaccessible = true };
                foreach (string path in Directory.EnumerateFiles(dialog.SelectedPath, "*.*", options))
                    if (MediaFormats.IsSupported(path)) files.Add(path);
            }
            catch (IOException ex)
            {
                ShowError("The folder could not be read.\n\n" + ex.Message);
                return;
            }
            catch (UnauthorizedAccessException ex)
            {
                ShowError("No permission to read this folder.\n\n" + ex.Message);
                return;
            }

            if (files.Count == 0)
            {
                SetStatus("No mp3 or mp4 files were found in that folder.");
                return;
            }
            await AddFilesAsync(files);
        }
    }

    // Reads each file's information and adds it. Unsupported, duplicate and unreadable files are counted and reported.
    private async Task AddFilesAsync(IEnumerable<string> paths)
    {
        int added = 0, unsupported = 0, duplicates = 0, failed = 0;
        SetStatus("Reading file information...");
        UseWaitCursor = true;

        foreach (string path in paths)
        {
            if (!MediaFormats.IsSupported(path)) { unsupported++; continue; }
            if (ContainsPath(path)) { duplicates++; continue; }
            if (!File.Exists(path)) { failed++; continue; }
            try
            {
                MediaItem item = await ReadMediaItemAsync(path);
                Playlist.Add(item); // read Playlist after the await, in case it was re-sorted meanwhile
                added++;
            }
            catch (VLCException)
            {
                failed++;
            }
            catch (IOException)
            {
                failed++;
            }
        }

        UseWaitCursor = false;
        RefreshListView();
        if (added > 0) SaveLibraryToFile();

        string message = "Added " + added + " item(s).";
        if (duplicates > 0) message += " " + duplicates + " already in the library.";
        if (unsupported > 0) message += " " + unsupported + " skipped: only .mp3 and .mp4 are supported.";
        if (failed > 0) message += " " + failed + " could not be read.";

        if (unsupported > 0 || failed > 0) ShowError(message);
        else SetStatus(message);
    }

    private bool ContainsPath(string path)
    {
        foreach (MediaItem item in Playlist)
            if (string.Equals(item.FilePath, path, StringComparison.OrdinalIgnoreCase)) return true;
        return false;
    }
}
