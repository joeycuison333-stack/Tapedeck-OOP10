# 6. Test Plan, Test Cases, and Bug Log

**Project:** Tapedeck (OOP10 media player) · **Group 3 – BSCS 2A** · **Author:** Valencia Genevieve (Genevieve)  
**Branch:** `test/test-plan` · **Date:** November 9, 2026  

---

## 6.1 Test Plan

### Objectives
Verify that Tapedeck meets all functional requirements (FR1–FR13) and non-functional requirements (NFR1–NFR6) as outlined in `PRD.md` Sections 2 and 3. Testing validates media playback integrity via LibVLCSharp, persistent storage of library and user ratings, custom retro UI rendering and event dispatch, edge cases, and robust negative error handling without application crashes.

### Test Environment
- **Operating System:** Windows 11 / Windows 10 (64-bit, x64 architecture).
- **Target Framework:** .NET 8.0 Windows Forms (`net8.0-windows`).
- **Development Tool:** Visual Studio 2026.
- **Display Scaling:** Tested at 100% and 125% Windows DPI scaling.
- **Playback Engine Dependencies:** LibVLCSharp.WinForms 3.9.0 and VideoLAN.LibVLC.Windows 3.0.21 native binaries (`libvlc\win-x64`).

### Test Media Suite
- Standard MP3 audio tracks (with varied ID3 metadata: Title, Artist, Album, Duration).
- Audio files with missing ID3 tags (testing fallback values).
- Standard MP4 video files with varied resolutions (e.g. 1080p `1920x1080`, 720p `1280x720`).
- Unsupported audio/video files (e.g. `.wav`, `.mkv`, `.flac`, `.avi`).
- Non-media files (e.g. `.txt`, `.jpg`, `.pdf`, `.exe`).
- Corrupt and malformed test data: `docs/test-data/Library_corrupt.txt`.
- Missing media test items (files renamed or deleted after library addition).

### Execution Methodology
1. **Automated Unit / Class Verification:** Validate data validation, parsing rules in `LibraryStore` and `FavoritesStore`, and sorting algorithms in `LibrarySorter`.
2. **Interactive Functional Testing:** Execute test cases TC01 through TC24 on the integrated build (`main` branch).
3. **Negative & Edge Testing:** Specifically stress-test empty library states, missing files, damaged persistence files, unsupported file imports, single-item collections, and playback boundary conditions.
4. **Bug Logging & Verification:** Log any discrepancy in Section 6.3, assign it to the respective component owner, and re-test following the fix commit.

---

## 6.2 Test Cases (TC01–TC24)

| ID | Feature | Steps | Expected result | Actual result | Pass / Fail |
|---|---|---|---|---|---|
| **TC01** | FR1 – Add Media Files | 1. Click **Add** button (or press `Ctrl+O`).<br>2. In the OpenFileDialog, select one `.mp3` and one `.mp4` file.<br>3. Click Open. | Both files are read and appended to the playlist (`listView1`). Status bar displays `"Added 2 item(s)."`. Total item counter updates (`lblCount`). Changes are saved to `Library.txt`. | Pending execution on `main` | Untested |
| **TC02** | FR1 – Add Folder Recursively | 1. Click **Add Folder** button (or press `Ctrl+Shift+O`).<br>2. In the FolderBrowserDialog, select a folder containing `.mp3` and `.mp4` files across root and subfolders.<br>3. Click Select Folder. | Recursively enumerates files via `Directory.EnumerateFiles(..., RecurseSubdirectories)`. All valid supported audio and video files are added to the playlist. Status bar displays `"Added N item(s)."`. | Pending execution on `main` | Untested |
| **TC03** | FR1 – Duplicate Media Detection | 1. Add a media file (e.g., `song1.mp3`).<br>2. Click **Add** button again and select the exact same file (`song1.mp3`). | File is detected as already in library via `ContainsPath()`. File is not duplicated. Status bar shows `"Added 0 item(s). 1 already in the library."`. | Pending execution on `main` | Untested |
| **TC04** | FR1, FR7 – **[Negative]** Unsupported File Formats | 1. Click **Add** button (`Ctrl+O`).<br>2. Select files with unsupported extensions (e.g. `sample.wav`, `notes.txt`, `image.jpg`).<br>3. Click Open. | Unsupported files are filtered and skipped by `MediaFormats.IsSupported()`. A warning dialog (`ShowError`) is shown: `"Added 0 item(s). N skipped: only .mp3 and .mp4 are supported."`. Playlist remains unchanged and app does not crash. | Pending execution on `main` | Untested |
| **TC05** | FR2 – Media Metadata Display (Audio vs Video) | 1. Add an audio file with ID3 tags and an MP4 video file.<br>2. Inspect the columns in `listView1` (Title, Artist, Album/Res, Length).<br>3. Select the audio file, then select the video file. | Audio file shows Title, Artist, Album name in Album/Res column, and formatted duration (`hh:mm:ss`). Video file shows Title, Artist, video resolution (e.g. `1920x1080`) in Album/Res column, and duration. Missing tags display fallback constants (`Unknown Title`, `Unknown Artist`, etc.). | Pending execution on `main` | Untested |
| **TC06** | FR3 – Save Library & Startup Reload | 1. Add multiple media items to the library.<br>2. Close Tapedeck via the Close button (`btnClose` or `Alt+F4`).<br>3. Inspect the file `Library.txt` generated next to `Tapedeck.exe`.<br>4. Relaunch Tapedeck. | `Library.txt` contains entries in the 6-field pipe format (`Type\|Title\|Artist\|hh:mm:ss\|FilePath\|Extra`). On startup, `LoadLibraryFromFile` reloads all entries into `listView1` and displays status `"Loaded N item(s) from Library.txt."`. | Pending execution on `main` | Untested |
| **TC07** | FR3, FR10 – Reload Library & Open in Notepad | 1. With a populated library, select `File > Open Library in Notepad` (`Ctrl+L`).<br>2. Verify Notepad opens with `Library.txt`. Edit a title in Notepad and save the file.<br>3. Switch back to Tapedeck and select `File > Reload Library` (`Ctrl+Shift+L`). | Notepad opens `Library.txt`. After saving in Notepad and clicking Reload Library in Tapedeck, the library reloads and reflects the edited title without requiring application restart. | Pending execution on `main` | Untested |
| **TC08** | FR4 – Play, Pause, and Stop Controls | 1. Select a track and click **Play** (or press `F5`).<br>2. Verify playback starts and Play button toggles to Pause.<br>3. Click **Pause** (or press `F6`). Verify playback pauses and button toggles to Play.<br>4. Click **Play** to resume from paused position.<br>5. Click **Stop** (or press `F7`). | Media starts playing smoothly; Pause stops playback without unloading; resuming starts from paused timestamp; Stop unloads playback, resets elapsed timer to `00:00:00`, resets seek bar, and restores Play button visibility. | Pending execution on `main` | Untested |
| **TC09** | FR4 – Next & Previous Navigation | 1. Load at least 3 tracks into the library. Start playing track 1.<br>2. Click **Next** (`btnNext` or `Ctrl+Right`).<br>3. Click **Next** again until reaching the last track, then click **Next** once more.<br>4. Click **Previous** (`btnPrevious` or `Ctrl+Left`). | Next advances sequentially to track 2, then track 3. Advancing past the last track wraps around to track 1 (`(CurrentIndex + 1) % Count`). Previous moves backward; pressing Previous on the first track wraps around to the last track. | Pending execution on `main` | Untested |
| **TC10** | FR4, FR10 – Volume Adjustment & Shortcuts | 1. While playing audio, adjust the volume trackbar `trackVolume`.<br>2. Press `Ctrl+Up` repeatedly.<br>3. Press `Ctrl+Down` repeatedly. | Volume changes smoothly between 0 and 100 in LibVLC. `lblVolume` displays `"Vol {value}"`. Shortcuts `Ctrl+Up` / `Ctrl+Down` increase / decrease volume by step (5%) and clamp at 0 and 100 without exceptions. | Pending execution on `main` | Untested |
| **TC11** | FR4, FR11 – Seeking Position via Trackbar | 1. While playing media, drag the `trackSeek` slider to the middle and release the mouse.<br>2. Focus the seek slider and press `Left Arrow` / `Right Arrow`. | Track position jumps immediately to the selected position (`Seek(trackSeek.Value / 1000f)`). Audio/video playback resumes synchronously from that timestamp without stutter or crash. | Pending execution on `main` | Untested |
| **TC12** | FR5 – Playlist Selection & Now Playing Display | 1. Load multiple tracks. Double-click track 2 (or select track 2 and press `Enter`).<br>2. Inspect the Now Playing area and list row highlighting. | Track 2 begins playback immediately. Now Playing area displays track Title in large bold text (`lblTitle`), Artist (`lblArtist`), and MediaType + Extra info (`lblExtra`). Track 2 row in `listView1` is highlighted in navy (`NowPlayingRowColor`) with bold white text. Window title updates to `Title - Artist - Tapedeck`. | Pending execution on `main` | Untested |
| **TC13** | FR4, FR7 – **[Negative]** Empty Library Operations | 1. Ensure playlist is completely empty (no files added, or delete `Library.txt` prior to launch).<br>2. Click **Play** (`F5`).<br>3. Click **Next**, **Previous**, **Stop**, **Pause**.<br>4. Click **Remove** and attempt to **Search**. | Clicking Play displays a clear warning message `"The library is empty. Add some files first."` via `ShowError`. Next/Previous/Stop/Pause return safely without null reference exceptions. Remove and Search show status indicating empty library. No crash occurs (NFR1). | Pending execution on `main` | Untested |
| **TC14** | FR3, FR7 – **[Negative]** Corrupt Lines in `Library.txt` | 1. Close Tapedeck.<br>2. Replace `Library.txt` with `docs/test-data/Library_corrupt.txt` (containing lines with wrong field counts, invalid duration strings, blank paths, unknown types, and non-conforming plain text).<br>3. Launch Tapedeck. | `LibraryStore.Load` skips all malformed lines without throwing unhandled exceptions. Only valid lines (e.g. good audio/video) load into `listView1`. `ShowError` reports: `"{N} damaged line(s) in Library.txt were skipped. The rest of the library was loaded."`. Status bar displays count of loaded valid items. | Pending execution on `main` | Untested |
| **TC15** | FR4, FR7 – **[Negative]** Missing / Deleted Media File Handling | 1. Add a valid `.mp3` file to the library.<br>2. Outside the app (in File Explorer), delete or rename the underlying media file on disk.<br>3. In Tapedeck, select `File > Reload Library` (or observe row on next launch).<br>4. Attempt to play the missing item (press **Play** or double-click the item). | Missing item is displayed in `listView1` with greyed-out text (`Theme.MutedColor`). Clicking Play detects `!File.Exists(item.FilePath)`: playback stops, status bar displays `"File not found: {path}"`, and a modal warning is displayed: `"This file was moved or deleted:\n\n{path}"`. Application remains responsive. | Pending execution on `main` | Untested |
| **TC16** | FR6 – Search by Title (Binary Search) & Partial Match | 1. Add multiple items with diverse titles and artists (e.g., "Bohemian Rhapsody", "Hotel California", "Imagine").<br>2. Type "Imagine" into `txtSearch` and click **Search** (or press `Enter`).<br>3. Type "Hotel" into `txtSearch` and press `Enter`.<br>4. Type part of an artist name into `txtSearch` and press `Enter`. | For exact title matches, `BinarySearchByTitle` executes (after sorting by title) and selects the matching row. Status bar displays `"Found via binary search: {Title}"`. For partial title or artist matches, `FindFirstContaining` finds and highlights the matching row, reporting `"Found via partial search: {Title}"`. | Pending execution on `main` | Untested |
| **TC17** | FR6 – **[Negative]** Search with No Match & Blank Query | 1. With a populated library, leave `txtSearch` empty and click **Search**.<br>2. Type `"NonExistentSongTitleXYZ"` into `txtSearch` and click **Search**. | For blank input, status bar displays `"Type a title or artist to search for."`. For non-existent term, both binary search and partial search return `-1`. Status bar displays `"No match for 'NonExistentSongTitleXYZ'."`. Current selection remains stable without exceptions. | Pending execution on `main` | Untested |
| **TC18** | FR12 – Hand-written Insertion Sort & Playing Item Retention | 1. Load 5+ tracks in random order. Start playing track 3.<br>2. Click the **Title** column header (or press `Ctrl+1`).<br>3. Click **Artist** column header (`Ctrl+2`), **Album / Resolution** (`Ctrl+3`), **Length** (`Ctrl+4`), and **Rating** (`Ctrl+5`). | Library is sorted in-place using hand-written insertion sort (stable sort). ListView refreshes in alphabetical / duration / rating order. `CurrentIndex` updates via `SetPlaylist(copy)` so that the currently playing track remains correctly highlighted and continues playing uninterrupted. | Pending execution on `main` | Untested |
| **TC19** | FR8 – Toggle Favorite & UI Updates | 1. Select a track in `listView1`.<br>2. Click the **♥ Fav** button (or press `Ctrl+D` or menu `Library > Toggle Favorite`).<br>3. Observe the `♥ ★` column in `listView1` and the Now Playing banner.<br>4. Click **♥ Unfav** again. | First click marks track as favorite (`IsFavorite = true`); button text changes to `♥ Unfav`; `"♥"` appears in the `♥ ★` column and `"• ♥ Favorite"` appears in Now Playing. Second click toggles `IsFavorite = false`; button reverts to `♥ Fav`; heart symbol disappears. Changes are saved immediately. | Pending execution on `main` | Untested |
| **TC20** | FR8 – Set Star Rating & Persistence in `Favorites.txt` | 1. Select a track. In `cboRating`, select `"★★★★ (4)"`.<br>2. Verify the `♥ ★` column displays `"4★"`.<br>3. Close Tapedeck. Check `Favorites.txt` for line `FilePath\|0\|4`.<br>4. Relaunch Tapedeck. | Selecting 4 stars sets `item.Rating = 4`. `listView1` displays `4★` in `♥ ★` column. Rating is saved to `Favorites.txt`. On startup, `FavoritesStore.Apply` restores the 4-star rating and dropdown reflects 4 stars when the row is selected. | Pending execution on `main` | Untested |
| **TC21** | FR9 – Splash Screen Display & Auto/Manual Dismiss | 1. Launch `Tapedeck.exe`. Observe splash screen layout.<br>2. Note presence of logo, AppTitle, Group/Section, and team cards (Joey, Heaven, Ammaar, Maddox, Genevieve).<br>3. Let it run without user input (timing test).<br>4. On next launch, press any key or click mouse immediately after splash appears. | Splash window renders centered with raised 3D border, showing all 5 member cards with photos/names/roles. Closes automatically after ~3000 ms. On second test, pressing a key or clicking anywhere closes splash immediately and opens `TapedeckForm`. | Pending execution on `main` | Untested |
| **TC22** | FR4, FR12, FR13 – **[Negative]** Single-Item Playlist Behavior | 1. Clear library and add exactly ONE media file.<br>2. Start playback of this single item.<br>3. Click **Next** (`btnNext`) and **Previous** (`btnPrevious`).<br>4. Turn Shuffle ON (`btnShuffle`) and click **Next**.<br>5. Click column header to Sort the library (`Ctrl+1`). | `Next` and `Previous` wrap safely to index 0 (`(0+1)%1 = 0`) and replay the track without crash. In Shuffle mode, `PickRandomIndex()` detects `Playlist.Count == 1` and returns 0 without infinite loop. `SortLibrary()` detects `Playlist.Count < 2` and returns early without altering list or throwing errors. | Pending execution on `main` | Untested |
| **TC23** | FR13 – **[Negative]** Playback Completion with Repeat Off | 1. Load 2 or more tracks. Select the LAST track in the playlist.<br>2. Ensure Shuffle is OFF and Repeat is set to `RepeatMode.Off` (`lblRepeat` shows "Off").<br>3. Play the track and seek near the very end (e.g. within 2 seconds of completion). Let it finish. | When track reaches EOF, `Player_EndReached` triggers `PlayNextOrStop()`. Because `Repeat == RepeatMode.Off` and `CurrentIndex == Playlist.Count - 1`, the player calls `Stop()`. Playback unloads, `_loadedPath` is set to null, status shows `"Stopped"`, and playback does NOT auto-restart or wrap to track 1. | Pending execution on `main` | Untested |
| **TC24** | FR13 – Continuous Playback: Repeat All, Repeat One, and Shuffle | 1. Set Repeat to `RepeatMode.One` (`Ctrl+R` until `lblRepeat` shows "One"). Play track and seek to end.<br>2. Verify same track restarts automatically from beginning.<br>3. Cycle Repeat to `RepeatMode.All`. Play last track in list and seek to end.<br>4. Verify playback wraps and starts playing track 1 automatically.<br>5. Turn Shuffle ON (`btnShuffle` / `Ctrl+H`). Play track and seek to end. | In Repeat One, `PlayNextOrStop()` invokes `Play()` on current item. In Repeat All, `PlayNextOrStop()` invokes `Next()`, which wraps from last to first item. In Shuffle ON, a random distinct track (`PickRandomIndex()`) is chosen and played automatically upon track completion. | Pending execution on `main` | Untested |

---

## 6.3 Bug Log

| Bug ID | Test Case | Severity | Description | Assigned Owner | Status | Fix Commit |
|---|---|---|---|---|---|---|
| *(Sample)* | TC14 | High | When Library.txt contains an empty line, app throws IndexOutOfRangeException. | Heaven | Resolved | `fix: skip blank lines in LibraryStore.Load` |
| | | | *(To be filled during test execution on `main`)* | | | |

---

## 6.4 Verification Summary Matrix

| Requirement | Description | Type | Covered By |
|---|---|---|---|
| **FR1** | Add mp3 and mp4 files through a file dialog (and a whole folder) | Must | TC01, TC02, TC03, TC04 |
| **FR2** | Show title, artist, duration, file path, media type, album (audio) or resolution (video) | Must | TC05 |
| **FR3** | Save the library to `Library.txt` and reload it when the program starts | Must | TC06, TC07, TC14 |
| **FR4** | Play, Pause, Stop, Next, Previous, volume and seek | Must | TC08, TC09, TC10, TC11, TC13, TC15, TC22 |
| **FR5** | Playlist list and Now Playing area | Must | TC12 |
| **FR6** | Search or sort by title or artist (sorting algorithm + binary search) | Should | TC16, TC17 |
| **FR7** | Handle missing files, corrupt lines, unsupported formats, empty library | Must | TC04, TC13, TC14, TC15, TC22, TC23 |
| **FR8** | Enhancement: Favorites and 1–5 star ratings, saved in `Favorites.txt` | Should | TC19, TC20 |
| **FR9** | Splash screen: logo, title, group, section, every member's photo, name and role; closes after ~3 s or on click/key | Must | TC21 |
| **FR10** | MenuStrip (File, Playback, Library, Help) with keyboard shortcuts calling the same methods as the buttons | Must | TC07, TC10, TC21 (plus menu invocations in TC01, TC06, TC08, TC18, TC19) |
| **FR11** | Elapsed time next to total length (00:01:12 / 00:03:25), updated ≥ once per second | Must | TC08, TC11, TC22 |
| **FR12** | Hand-written insertion sort by title and artist (no List.Sort/OrderBy); keeps the current item | Must | TC18, TC22 |
| **FR13** | Shuffle on/off and Repeat Off/All/One; the next item starts automatically | Must | TC22, TC23, TC24 |

### Negative Test Cases Verification
- **Missing file:** TC15 (moved/deleted file displays greyed out, reports error dialog on play).
- **Corrupt line in `Library.txt`:** TC14 (skips invalid lines from `Library_corrupt.txt` and reports skipped count).
- **Empty library:** TC13 (safe no-op/error messaging for Play, Next, Previous, Remove, Search with 0 items).
- **Unsupported format:** TC04 (rejects `.wav`, `.txt`, `.jpg` with warning).
- **One-item list:** TC22 (validates Next/Previous wrap, Shuffle infinite loop avoidance, and Sort early exit).
- **Last item with Repeat Off:** TC23 (stops playback at EOF on the last item when Repeat is Off).
