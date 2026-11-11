# 7. User Manual

**Project:** Tapedeck – OOP10 Desktop Media Player  
**Course:** OOP10 – Object-Oriented Programming with C# and Windows Forms  
**Group:** Group 3 · BSCS 2A  
**Author:** Valencia Genevieve (Genevieve) · Favorites, Testing, and User Manual  
**Target Release:** v1.0 (.NET 8 Windows Forms, x64)

---

## 7.1 Product Overview and Features

**Tapedeck** is an original retro late-90s desktop media player designed for Windows 10 and Windows 11 (64-bit). Built in C# and Windows Forms (.NET 8), using Visual Studio 2026, Tapedeck combines modern audio/video decoding via LibVLCSharp with the tactile charm of a vintage cassette tape deck.

### Core Capabilities
- **Universal Media Support:** Seamlessly catalogues and plays standard MPEG-1 Audio Layer III (`.mp3`) and MPEG-4 Part 14 Video (`.mp4`) files.
- **Rich Tag Parsing:** Automatically inspects ID3 metadata to extract track Title, Artist, Duration, Album (audio), and Frame Resolution (video).
- **Persistent Library Storage:** Automatically maintains your media catalog in `Library.txt` using a standard 6-field delimited structure, reloading your collection on startup.
- **Custom Retro Cassette Skin:** Features a custom 980 × 620 px cream-and-terracotta cassette deck interface with bevelled controls, sunken wells, analog-style indicators, and custom button artwork designed in Canva.
- **Complete Playback Controls:** Play, Pause, Stop, Previous, Next, Seek slider (0–1000), and Volume slider (0–100) with real-time numeric/status feedback.
- **Continuous & Randomized Playback:** Supports Repeat Off, Repeat All, Repeat One, and non-repeating Shuffle playback.
- **Algorithmic Sorting & Searching:** Hand-written stable insertion sort (O(n) best-case, O(n²) worst-case) sorting by Title, Artist, Album/Resolution, Duration, and Star Rating without interrupting active playback. High-speed binary search by exact title backed by fallback partial-text search across titles and artists.
- **Favorites & Star Ratings Enhancement:** 1-to-5 star rating system and favorite toggle (`♥ Fav` / `♥ Unfav`) stored persistently in `Favorites.txt`.
- **Full Keyboard Accessibility:** Every major operation can be triggered through dedicated menu shortcuts, hotkeys, or mnemonic access keys.
- **Robust Error Resilience:** Zero-crash architecture with structured exception handling around all file system and VLC engine operations, paired with clear user status feedback.

---

## 7.2 System Requirements & Installation

### System Requirements
| Component | Minimum Requirement | Recommended |
|---|---|---|
| **Operating System** | Windows 10 (64-bit, version 1809+) | Windows 11 (64-bit, version 22H2+) |
| **Architecture** | x64 (AMD64 / Intel 64) | x64 |
| **Runtime Environment** | None (.NET 8 runtime bundled in self-contained release) | .NET 8 Desktop Runtime (x64) |
| **Media Codecs** | Bundled native LibVLC 3.0.21 x64 binaries | None required externally |
| **Display Resolution** | 1024 × 768 pixels | 1920 × 1080 pixels (100% or 125% DPI scaling) |
| **Storage Space** | 360 MB free disk space | 500 MB free disk space |
| **Permissions** | Standard user account read/write permissions in application directory | User `Documents` or `Desktop` folder |

### Installation Instructions
1. Download `G3_BSCS2A_Release.zip` from the project repository or distribution package.
2. Extract the archive into a standard user-writable folder (such as `C:\Users\<Username>\Music\Tapedeck` or `Documents\Tapedeck`).
   > [!IMPORTANT]
   > Do not extract Tapedeck into write-protected system directories such as `C:\Program Files\` unless run with elevated permissions. Tapedeck writes `Library.txt` and `Favorites.txt` directly adjacent to `Tapedeck.exe`.
3. Verify that the application directory contains:
   - `Tapedeck.exe` (main executable)
   - `Assets\` folder (containing `tapedeck_skin.png`, `logo.png`, `logo.ico`, button glyphs, and team photos)
   - `libvlc\win-x64\` folder (containing native `libvlc.dll`, `libvlccore.dll`, and codec plugins)
4. Double-click `Tapedeck.exe` to launch the application.

---

## 7.3 User Interface Layout & Retro Desktop Skin

Tapedeck operates within a borderless 980 × 620 pixel canvas styled after a classic portable cassette recorder.

```
+-----------------------------------------------------------------------------------------+
| [Logo] Tapedeck                          OOP Group 3 | BSCS 2-A              [_] [X]    |
+-----------------------------------------------------------------------------------------+
| File    Playback    Library    Help                                                     |
+---------------------------------------------+-------------------------------------------+
|                                             |  Library (Count: N items)                 |
|                                             |  [ Search Text Box ] [ Search Button ]    |
|             VIDEO DISPLAY WELL              |  +-------------------------------------+  |
|                  (pnlVideo)                 |  | Title | Artist | Album/Res | Dur |♥ ★|  |
|                   560 x 315                 |  |-------------------------------------|  |
|                                             |  | Track 1                             |  |
|                                             |  | Track 2 (Navy/Teal Highlight)       |  |
|                                             |  | Track 3                             |  |
+---------------------------------------------+  | ...                                 |  |
| Now Playing                                 |  +-------------------------------------+  |
| "Track Title"                               |  [Add] [Add Folder] [Remove]              |
| Artist Name  •  Type • Extra • ♥ • ★★★★★    |  [♥ Fav / ♥ Unfav]  Rating: [★★★★☆ (4) v] |
| Elapsed: 00:01:24 / 00:03:45                |                                           |
| [==================== Seek Slider ========] |                                           |
| [|<]  [ > / || ]  [ # ]  [>|]   (Vol: 80%)  |                                           |
| [Shuffle] [Repeat: All]      [----Slider--] |                                           |
+---------------------------------------------+-------------------------------------------+
| Status: Ready / Loaded 12 item(s)...                    | 12 items                      |
+---------------------------------------------------------+-------------------------------+
```

### Visual Regions
1. **Custom Title Bar (`pnlHeader`):** Positioned at `(0, 0)` with a height of 46 px. Displays the retro cassette tape logo (`picLogo`), application title (`lblAppTitle`), group credentials (`lblGroup`), Minimize button (`btnMinimize`), and Close button (`btnClose`). The entire title bar acts as a draggable surface allowing seamless repositioning on the desktop.
2. **Menu Strip (`menuMain`):** Positioned directly beneath the custom title bar at `y = 46` with a height of 24 px. Styled with cream backgrounds and warm highlights to maintain full keyboard shortcut dispatch without interrupting the skin aesthetic.
3. **Sunken Video Well (`pnlVideo`):** Positioned at `(20, 80)` with dimensions 560 × 315 px. Hosts the hardware-accelerated LibVLC `VideoView`. Stays deep black during audio playback and displays crisp video during `.mp4` playback.
4. **Now Playing Group Box (`lblNowCaption`):** Positioned beneath the video well at `(32, 411)`. Shows:
   - `lblTitle`: Large 16 pt bold display of the active track title.
   - `lblArtist`: Artist metadata.
   - `lblExtra`: Media format type, album name or video resolution, favorite status, and star rating.
   - `lblElapsed`: Monospaced counter (`Consolas`) rendering `hh:mm:ss / hh:mm:ss`.
   - `trackSeek`: Seek trackbar with 1,000 discrete steps for fluid timeline scrub.
   - Transport buttons: `btnPrevious`, `btnPlay` / `btnPause` (dynamic toggle), `btnStop`, `btnNext`.
   - Mode toggles: `btnShuffle` (pressed-in teal when active) and `btnRepeat` paired with `lblRepeat` indicator (`Off`, `All`, `One`).
   - Volume control: `trackVolume` (0–100%) paired with `lblVolume` display.
5. **Library Group Box (`lblLibrary`):** Positioned at `(612, 91)` occupying the right side of the deck.
   - Search bar: `txtSearch` box and `btnSearch`.
   - Playlist view (`listView1`): Detailed 5-column listing displaying Title, Artist, Album/Resolution, Length, and `♥ ★`. Highlights the active song in navy/teal with bold white text; renders missing/unreachable files in muted grey.
   - Library controls: `btnAdd`, `btnAddFolder`, and `btnRemove`.
   - Rating controls: `btnFavorite` (`♥ Fav` / `♥ Unfav`) and `cboRating` (0 to 5 stars).
6. **Sunken Status Bar Panels:** Located along the bottom border at `y = 594`:
   - Left panel (`lblStatus`, 782 px width): Real-time user feedback, operation logs, error summaries, and playback states.
   - Right panel (`lblCount`, 154 px width): Exact track count in library.

---

## 7.4 Media Library Management

### Adding Individual Media Files (FR1)
1. Click the **Add** button in the Library panel or press **Ctrl+O** (or choose `File > Add Files...`).
2. In the Windows File Dialog, browse to your media folder.
3. Select one or more `.mp3` or `.mp4` files (hold `Ctrl` or `Shift` to multi-select).
4. Click **Open**. Tapedeck parses the metadata asynchronously and appends the tracks to the library view.
5. Status confirms: `"Added N item(s)."`

### Adding an Entire Directory or Folder (FR1)
1. Click **Add Folder** or press **Ctrl+Shift+O** (or choose `File > Add Folder...`).
2. Select any root folder containing your media collection.
3. Click **Select Folder**. Tapedeck recursively traverses all nested subdirectories, ignoring inaccessible folders, and imports all supported `.mp3` and `.mp4` files.
4. If unsupported formats or duplicate paths exist, Tapedeck reports the exact summary in the status bar (or via modal dialog if errors occurred).

### Duplicate and Unsupported File Detection (FR1, FR7)
- **Duplicates:** Tapedeck performs case-insensitive path comparisons (`ContainsPath`). If a selected file is already in your library, it is skipped without duplicating. The status bar will show `"Added N item(s). D already in the library."`
- **Unsupported Formats:** Files with extensions other than `.mp3` and `.mp4` (e.g. `.wav`, `.flac`, `.mkv`, `.avi`, `.txt`) are filtered out. Tapedeck displays: `"Added N item(s). S skipped: only .mp3 and .mp4 are supported."`

### Removing Media from Library (FR7)
1. Highlight the track you wish to remove in `listView1`.
2. Click **Remove** or press the **Delete** key (or select `Library > Remove Selected`).
3. If the removed track is currently playing, playback halts immediately (`Stop()`), Now Playing is cleared, and the track is safely excised.
4. If an item preceding the current playing track is removed, the internal `CurrentIndex` decrements automatically to keep pointing to the active song.
5. Changes are committed immediately to `Library.txt`.

### Editing and Reloading the Library in Notepad (FR3, FR10)
Advanced users can directly edit the persistent catalog text:
1. Select `File > Open Library in Notepad` (or press **Ctrl+L**).
2. Tapedeck saves the current memory playlist to `Library.txt` and opens it in Windows Notepad.
3. Each line represents one track using the 6-field pipe format:
   ```text
   Audio|Bohemian Rhapsody|Queen|00:05:55|C:\Music\Queen\bohemian.mp3|A Night at the Opera
   Video|Big Buck Bunny|Blender Foundation|00:09:56|C:\Videos\bbb.mp4|1920x1080
   ```
4. Edit titles, artists, or extra metadata, then save the file in Notepad (`Ctrl+S`) and close Notepad.
5. Return to Tapedeck and select `File > Reload Library` (or press **Ctrl+Shift+L**). Tapedeck instantly parses the modified file and updates the view.

---

## 7.5 Playback Operations

### Initiating Playback (FR4, FR5)
- **Double-Click:** Double-click any row in the playlist to start immediate playback.
- **Enter Key:** Select a row and press **Enter**.
- **Play Button:** Click the **Play** button or press **F5** (or choose `Playback > Play`).
  - If a track in the list is highlighted, playback begins from that track.
  - If paused on the same track, playback resumes from the exact paused timestamp without reloading.
  - While playing, the **Play** button automatically transforms into the **Pause** button.

### Pausing and Resuming
- Click the **Pause** button or press **F6** (or choose `Playback > Pause`).
- Audio/video freezes, elapsed timer halts, and status shows `"Paused"`.
- Click **Play** or press **F5** to resume playback smoothly.

### Stopping Playback
- Click the **Stop** button or press **F7** (or choose `Playback > Stop`).
- Playback terminates, media unloads, seek bar resets to 0, elapsed timer displays `00:00:00 / 00:00:00`, and status indicates `"Stopped"`.

### Next and Previous Navigation
- **Next Track:** Click **Next** or press **Ctrl+Right** (or choose `Playback > Next`). Advances to the next sequential track. If currently on the last item, wraps around to track 1.
- **Previous Track:** Click **Previous** or press **Ctrl+Left** (or choose `Playback > Previous`). Jumps backward one track. If currently on the first item, wraps around to the final track.

### Seeking and Timeline Scrubbing (FR11)
- **Mouse Drag:** Click and drag the `trackSeek` slider under the video well. When released, LibVLC jumps directly to the target percentage (`Seek(position)`).
- **Arrow Keys:** Focus the seek slider and use **Left Arrow** / **Right Arrow** to adjust position in fine increments.

### Volume Adjustment
- Drag the `trackVolume` slider between 0% and 100%.
- Press **Ctrl+Up** to boost volume by +10% increments.
- Press **Ctrl+Down** to lower volume by -10% increments.
- Volume clamps safely at 0% (mute) and 100% without distortion.

### Continuous & Random Playback Modes (FR13)
- **Shuffle Mode:** Click the **Shuffle** button or press **Ctrl+H** (or choose `Playback > Shuffle`).
  - When active, the Shuffle button turns teal and appears pressed-in.
  - When the current track ends, or when Next is clicked, Tapedeck selects a random index from the playlist (`PickRandomIndex()`), guaranteeing the same song is not repeated consecutively.
- **Repeat Modes:** Click the **Repeat** button or press **Ctrl+R** (or choose `Playback > Repeat`). Each activation cycles through:
  1. **Repeat Off:** Normal playback. When the final track in the playlist finishes, playback stops.
  2. **Repeat All:** When the final track finishes, playback wraps around to track 1 and continues indefinitely.
  3. **Repeat One:** Replays the active track continuously from the beginning each time it concludes.

---

## 7.6 Playlist Management & Library Persistence

### Data Storage Architecture (FR3)
Tapedeck stores its library in the application directory inside `Library.txt`.
- **Atomic Save Operation:** During saving, Tapedeck writes all records to a temporary file (`Library.txt.tmp`) using UTF-8 encoding. Once complete, `File.Move(..., overwrite: true)` atomically replaces `Library.txt`. This prevents file corruption in the event of an unexpected power failure or termination.
- **Line Format (6 Fields):**
  $$\text{MediaType} \mid \text{Title} \mid \text{Artist} \mid \text{Duration (hh:mm:ss)} \mid \text{FilePath} \mid \text{Album / Resolution}$$
  *(Any pipe characters inside metadata fields are safely converted to slashes `/`).*
- **Damaged Line Tolerance (FR7):** If a user manually edits `Library.txt` and introduces corrupted or malformed lines, Tapedeck skips the corrupt lines, increments a damaged line counter, loads all remaining valid lines, and alerts the user with the exact count of skipped entries.
- **Missing File Tracking:** If a media file was moved, renamed, or deleted from disk while Tapedeck was closed, the track remains in the playlist but is visually rendered in grey (`Theme.MutedColor`). Attempting to play the missing item displays a non-crashing warning dialog.

---

## 7.7 Sorting and Searching the Library

### Hand-Written Insertion Sort (FR6, FR12)
Tapedeck features a custom, stable in-memory insertion sort implementation in `LibrarySorter.cs`. Per course specifications, it does **not** rely on `List.Sort()`, `OrderBy()`, or LINQ.
- **Playing Item Retention:** When sorting is executed, Tapedeck preserves the currently playing song's position by updating `CurrentIndex = Playlist.IndexOf(current)` so playback continues uninterrupted.
- **Sorting Methods:**
  - **By Column Header:** Click any column header in `listView1` to sort immediately:
    - **Title:** Alphabetical A to Z (`StringComparer.OrdinalIgnoreCase`).
    - **Artist:** Alphabetical A to Z.
    - **Album / Resolution:** Alphabetical A to Z.
    - **Length:** Numeric duration comparison (shortest to longest).
    - **♥ ★ (Rating):** Highest star rating first (5★ down to unrated).
  - **By Menu / Shortcut:**
    - `Ctrl+1` $\rightarrow$ Sort by Title
    - `Ctrl+2` $\rightarrow$ Sort by Artist
    - `Ctrl+3` $\rightarrow$ Sort by Album / Resolution
    - `Ctrl+4` $\rightarrow$ Sort by Duration
    - `Ctrl+5` $\rightarrow$ Sort by Rating

### Searching the Library (FR6)
Tapedeck employs a dual-stage search mechanism:
1. Type a query string into the search box (`txtSearch`).
2. Click **Search** or press **Enter**.
3. **Stage 1 (Binary Search):** The library is sorted by Title and queried via `BinarySearchByTitle`. If an exact title match is found, the item is selected immediately and the status bar displays `"Found \"<query>\" using binary search."`
4. **Stage 2 (Partial-Match Search):** If binary search finds no exact match, `FindFirstContaining` searches linearly for the first track whose Title or Artist contains the query string (case-insensitive). If found, the row is highlighted and the status bar displays `"Found \"<query>\" using partial-match search."`
5. If no match is found, the status bar displays `"No match for \"<query>\"."`

---

## 7.8 Favorites and Star Ratings

### Marking Tracks as Favorites (FR8)
1. Select any track in the playlist view (or let a track play).
2. Click the **♥ Fav** button or press **Ctrl+D** (or select `Library > Toggle Favorite`).
3. If the track was unfavorited:
   - Button text switches to **♥ Unfav**.
   - A heart symbol `♥` appears in the playlist's `♥ ★` column.
   - The Now Playing banner updates to display `• ♥ Favorite`.
   - Status reports: `"Marked as favorite: Artist - Title"`.
4. Clicking **♥ Unfav** or pressing **Ctrl+D** again removes favorite status and updates all UI elements.

### Assigning Star Ratings (FR8)
1. Select a track in the playlist view.
2. Click the rating dropdown (`cboRating`) below the playlist.
3. Select any rating:
   - `Not rated (0)`
   - `★☆☆☆☆ (1)`
   - `★★☆☆☆ (2)`
   - `★★★☆☆ (3)`
   - `★★★★☆ (4)`
   - `★★★★★ (5)`
4. The rating updates immediately in the `♥ ★` column (e.g. `4★` or `♥ 5★`) and in the Now Playing area.
5. Status bar reports: `"Rated ★★★★☆: Artist - Title"`. Selecting `Not rated` clears the rating and displays `"Rating cleared: Artist - Title"`.

### Favorites Persistence (`Favorites.txt`)
All favorites and ratings are saved independently to `Favorites.txt` in the format:
$$\text{FilePath} \mid 1 \text{ or } 0 \mid \text{Rating (0--5)}$$
Only items that are either marked as favorite or assigned a star rating (> 0) are saved to disk, keeping the file lightweight and organized.

---

## 7.9 Keyboard Shortcuts Reference

Tapedeck features extensive keyboard shortcut bindings across the menu system, playlist, and search controls.

### Complete Keyboard Shortcuts Table
| Shortcut | Action | Scope / Context | Calling Handler / Implementation |
|---|---|---|---|
| **Ctrl+O** | Add Media Files... | Global Form / Menu | `btnAdd_Click` via `File > Add Files...` |
| **Ctrl+Shift+O** | Add Folder Recursively... | Global Form / Menu | `btnAddFolder_Click` via `File > Add Folder...` |
| **Ctrl+S** | Save Library | Global Form / Menu | `SaveWithStatus()` via `File > Save Library` |
| **Ctrl+Shift+L** | Reload Library from File | Global Form / Menu | `ReloadLibrary()` via `File > Reload Library` |
| **Ctrl+L** | Open `Library.txt` in Notepad | Global Form / Menu | `btnOpenNotepad_Click` via `File > Open Library in Notepad` |
| **Alt+F4** | Exit Tapedeck | Global Form / Menu | `Close()` via `File > Exit` |
| **F5** | Play / Resume Playback | Global Form / Menu | `btnPlay_Click` via `Playback > Play` |
| **F6** | Pause Playback | Global Form / Menu | `btnPause_Click` via `Playback > Pause` |
| **F7** | Stop Playback | Global Form / Menu | `btnStop_Click` via `Playback > Stop` |
| **Ctrl+Right** | Next Track | Global Form / Menu | `btnNext_Click` via `Playback > Next` |
| **Ctrl+Left** | Previous Track | Global Form / Menu | `btnPrevious_Click` via `Playback > Previous` |
| **Ctrl+Up** | Volume Up (+10%) | Global Form / Menu | `ChangeVolume(VolumeStep)` via `Playback > Volume Up` |
| **Ctrl+Down** | Volume Down (-10%) | Global Form / Menu | `ChangeVolume(-VolumeStep)` via `Playback > Volume Down` |
| **Ctrl+H** | Toggle Shuffle Mode | Global Form / Menu | `SetShuffle()` via `Playback > Shuffle` |
| **Ctrl+R** | Cycle Repeat (Off $\rightarrow$ All $\rightarrow$ One) | Global Form / Menu | `CycleRepeat()` via `Playback > Repeat` |
| **Ctrl+1** | Sort by Title (A to Z) | Global Form / Menu | `SortLibrary(SortKey.Title)` |
| **Ctrl+2** | Sort by Artist (A to Z) | Global Form / Menu | `SortLibrary(SortKey.Artist)` |
| **Ctrl+3** | Sort by Album / Resolution | Global Form / Menu | `SortLibrary(SortKey.AlbumOrResolution)` |
| **Ctrl+4** | Sort by Duration (Shortest first) | Global Form / Menu | `SortLibrary(SortKey.Duration)` |
| **Ctrl+5** | Sort by Star Rating (5★ first) | Global Form / Menu | `SortLibrary(SortKey.Rating)` |
| **Ctrl+F** | Focus Search Box | Global Form / Menu | `txtSearch.Focus()` via `Library > Search...` |
| **Ctrl+D** | Toggle Favorite (♥ Fav / ♥ Unfav) | Global Form / Menu | `btnFavorite_Click` via `Library > Toggle Favorite` |
| **Delete** | Remove Selected Track | Global Form / Playlist | `btnRemove_Click` / `listView1_KeyDown` |
| **Enter** *(in Playlist)* | Play Selected Track | Playlist (`listView1`) | `listView1_KeyDown` $\rightarrow$ `PlaySelected()` |
| **Enter** *(in Search)* | Execute Title/Artist Search | Search Box (`txtSearch`) | `txtSearch_KeyDown` $\rightarrow$ `SearchLibrary()` |
| **Left / Right Arrow** | Scrub Timeline Backward / Forward | Seek Bar (`trackSeek`) | `TrackBar` internal keyboard scroll |
| **Up / Down Arrow** | Adjust Audio Volume | Volume Bar (`trackVolume`) | `TrackBar` internal keyboard scroll |
| **Ctrl+K** | Show Keyboard Shortcuts Dialog | Global Form / Menu | `ShowShortcuts()` via `Help > Keyboard Shortcuts` |
| **F1** | Show About / Team Splash Dialog | Global Form / Menu | `ShowAbout()` via `Help > About` |
| **Any Key / Click** | Dismiss Splash Screen | Splash Window | `SplashForm.KeyDown` / `Click` $\rightarrow$ `CloseSplash()` |

### Menu Mnemonics (Alt Access Keys)
- **Alt+F:** Open File Menu (`&File`) $\rightarrow$ **A** (Add Files), **F** (Add Folder), **S** (Save Library), **R** (Reload Library), **O** (Open Library in Notepad), **X** (Exit).
- **Alt+P:** Open Playback Menu (`&Playback`) $\rightarrow$ **P** (Play), **A** (Pause), **S** (Stop), **N** (Next), **V** (Previous), **U** (Volume Up), **D** (Volume Down), **H** (Shuffle), **R** (Repeat).
- **Alt+L:** Open Library Menu (`&Library`) $\rightarrow$ **T** (Sort Title), **A** (Sort Artist), **L** (Sort Album/Res), **D** (Sort Duration), **R** (Sort Rating), **S** (Search), **F** (Toggle Favorite), **M** (Remove Selected).
- **Alt+H:** Open Help Menu (`&Help`) $\rightarrow$ **K** (Keyboard Shortcuts), **A** (About).

### Code & Dialog Mismatches Analysis
When cross-referencing `UI/TapedeckForm.Menu.cs` with the running application and popup dialogs:
1. **Shortcut Dialog Omission (`Ctrl+K`):** In `BuildMenu()`, `Help > Keyboard Shortcuts` is wired to `Keys.Control | Keys.K`. However, the string inside `ShowShortcuts()` omits `Ctrl+K` from its own printed cheat sheet.
2. **Exit Shortcut Omission (`Alt+F4`):** File > Exit specifies `exitItem.ShortcutKeyDisplayString = "Alt+F4"`, but `ShowShortcuts()` does not include exit instructions.
3. **Implicit Control Keys (`Enter` & Slider Arrows):** `Enter` for playing highlighted tracks in `listView1` and triggering search in `txtSearch` are handled in separate control event handlers (`KeyDown`) rather than the `MenuStrip`. They work natively in the app but are not itemized in the menu strip help dialog.
4. **Sort Key Consistency:** The code assigns `Ctrl+D1` through `Ctrl+D5` to `SortKey.Title` through `SortKey.Rating`. In the user manual and shortcuts popup, these are rendered cleanly as `Ctrl+1` through `Ctrl+5`.

---

## 7.10 Comprehensive Troubleshooting and Error Messages Reference

Tapedeck handles all file, playback, and parsing exceptions cleanly. The table below lists **every message** generated by `ShowError`, `SetStatus`, and `Application.ThreadException` across the codebase, along with its trigger and recommended solution.

> [!NOTE]
> In `TapedeckForm.cs`, when `ShowError(message)` is invoked, the application automatically extracts the first line (`message.Split('\n')[0]`) and displays it in the bottom-left status bar (`lblStatus`) in addition to showing the modal dialog.

### Complete Messages Catalog
| # | Exact Message Text | Display Mechanism | Source / Event | Root Cause | Recommended User Action |
|---|---|---|---|---|---|
| **1** | `"The playback engine could not start. Playback is disabled.\n\n<Exception>"` | Modal Warning Dialog (`ShowError`) + Status Bar | `BasePlayerForm` Constructor | LibVLC failed to initialize (`VLCException`). | Ensure 64-bit Windows is used. Check that system VC++ redistributable packages are intact. |
| **2** | `"The VLC files were not found next to the program.\n\n<Exception>"` | Modal Warning Dialog (`ShowError`) + Status Bar | `BasePlayerForm` Constructor | Missing `libvlc\win-x64\` folder adjacent to `Tapedeck.exe`. | Re-extract the application archive, verifying that `libvlc.dll`, `libvlccore.dll`, and `plugins/` are present in `libvlc\win-x64\`. |
| **3** | `"The library is empty. Add some files first."` | Modal Warning Dialog (`ShowError`) + Status Bar | `BasePlayerForm.Play()` | User pressed Play (`F5`) while `Playlist.Count == 0`. | Add audio or video files first by clicking **Add** (`Ctrl+O`) or **Add Folder** (`Ctrl+Shift+O`). |
| **4** | `"The playback engine is not available, so nothing can be played."` | Status Bar (`SetStatus`) | `BasePlayerForm.Play()` | Play invoked after LibVLC engine failed to load. | Re-extract native LibVLC binaries and restart Tapedeck. |
| **5** | `"Resumed: <Artist - Title>"` | Status Bar (`SetStatus`) | `BasePlayerForm.Play()` | User resumed playback of a paused track. | Normal informational feedback; track is continuing playback. |
| **6** | `"File not found: <FilePath>"` | Status Bar (`SetStatus`) | `BasePlayerForm.Play()` | User attempted to play a file that was deleted or moved. | Re-locate the file or remove the missing entry from the library using **Delete**. |
| **7** | `"This file was moved or deleted:\n\n<FilePath>"` | Modal Warning Dialog (`ShowError`) + Status Bar | `BasePlayerForm.Play()` | Underneath file no longer exists at recorded path. | Check if the target storage drive or USB is plugged in; remove the stale playlist row if file was permanently removed. |
| **8** | `"Could not play <FileName>.\n\n<Exception>"` | Modal Warning Dialog (`ShowError`) + Status Bar | `BasePlayerForm.Play()` | LibVLC threw `VLCException` while initiating playback. | Verify file is not corrupt and that another process has not locked the media file exclusively. |
| **9** | `"Paused"` | Status Bar (`SetStatus`) | `BasePlayerForm.Pause()` | Playback paused by user or hotkey (`F6`). | Press **Play** (`F5`) to resume playback. |
| **10** | `"Stopped"` | Status Bar (`SetStatus`) | `BasePlayerForm.Stop()` | Playback stopped by user (`F7`) or reached EOF with Repeat Off. | Press **Play** (`F5`) to restart from beginning. |
| **11** | `"The file could not be played. It may be damaged or in an unsupported format."` | Modal Warning Dialog (`ShowError`) + Status Bar | `BasePlayerForm.Player_EncounteredError` | LibVLC background decoder reported an unplayable bitstream. | The file header or stream payload is damaged. Convert file to standard MP3/MP4 format. |
| **12** | `"Now playing: <Artist - Title>"` | Status Bar (`SetStatus`) | `TapedeckForm.OnNowPlayingChanged` | New track began playing. | Normal operational feedback. |
| **13** | `"Shuffle on"` / `"Shuffle off"` | Status Bar (`SetStatus`) | `TapedeckForm.SetShuffle()` | Shuffle button toggled (`Ctrl+H`). | Indicates active randomized playback state. |
| **14** | `"Repeat: <Off / All / One>"` | Status Bar (`SetStatus`) | `TapedeckForm.CycleRepeat()` | Repeat button clicked (`Ctrl+R`). | Indicates active loop mode. |
| **15** | `"The folder could not be read.\n\n<Exception>"` | Modal Warning Dialog (`ShowError`) + Status Bar | `TapedeckForm.btnAddFolder_Click` | `IOException` while enumerating directory paths. | Verify folder is accessible and not corrupted on disk. |
| **16** | `"No permission to read this folder.\n\n<Exception>"` | Modal Warning Dialog (`ShowError`) + Status Bar | `TapedeckForm.btnAddFolder_Click` | `UnauthorizedAccessException` traversing directory. | Run with appropriate user permissions or choose a folder within your personal User profile. |
| **17** | `"No mp3 or mp4 files were found in that folder."` | Status Bar (`SetStatus`) | `TapedeckForm.btnAddFolder_Click` | Selected folder contains 0 supported media items. | Select a directory containing `.mp3` audio or `.mp4` video files. |
| **18** | `"Reading file information..."` | Status Bar (`SetStatus`) | `TapedeckForm.AddFilesAsync()` | Metadata extraction in progress. | Transient message while files are parsed asynchronously. Wait cursor is shown. |
| **19** | `"Added N item(s)."` | Status Bar (`SetStatus`) | `TapedeckForm.AddFilesAsync()` | Clean addition of media items with 0 warnings. | Normal operational feedback; tracks added to library. |
| **20** | `"Added N item(s). D already in the library."` | Status Bar (`SetStatus`) | `TapedeckForm.AddFilesAsync()` | Duplicates detected during import. | Stale or redundant files were omitted automatically. |
| **21** | `"Added N item(s). S skipped: only .mp3 and .mp4 are supported."` | Modal Warning Dialog (`ShowError`) + Status Bar | `TapedeckForm.AddFilesAsync()` | Non-supported extensions chosen. | Ensure files have `.mp3` or `.mp4` extensions. |
| **22** | `"Added N item(s). F could not be read."` | Modal Warning Dialog (`ShowError`) + Status Bar | `TapedeckForm.AddFilesAsync()` | File was missing or threw read exception during tag parse. | Verify file is not open exclusively in another editor or DAW. |
| **23** | `"Skin not loaded, using plain colours: <Exception>"` | Status Bar (`SetStatus`) | `TapedeckForm.ApplyTheme()` | Missing `Assets\tapedeck_skin.png` (`IOException`). | Ensure `Assets\tapedeck_skin.png` exists. Application falls back to standard silver/cream background. |
| **24** | `"Skin image is damaged, using plain colours: <Exception>"` | Status Bar (`SetStatus`) | `TapedeckForm.ApplyTheme()` | Corrupted `Assets\tapedeck_skin.png` (`ArgumentException`). | Replace the image with a valid 980 × 620 px PNG. |
| **25** | `"Logo not loaded: <Exception>"` | Status Bar (`SetStatus`) | `TapedeckForm.ApplyTheme()` | Missing `Assets\logo.png` or `logo.ico` (`IOException`). | Verify `Assets\logo.png` is in place. Logo box remains blank without crashing. |
| **26** | `"Logo image is damaged: <Exception>"` | Status Bar (`SetStatus`) | `TapedeckForm.ApplyTheme()` | Invalid image encoding in `Assets\logo.png`. | Replace logo with a valid PNG file. |
| **27** | `"The library is empty. Add files with Ctrl+O."` | Status Bar (`SetStatus`) | `TapedeckForm.LoadedStatus()` | Startup load found empty or non-existent `Library.txt`. | Normal first-run status; press **Ctrl+O** to add music. |
| **28** | `"Loaded N item(s) from Library.txt."` | Status Bar (`SetStatus`) | `TapedeckForm.LoadedStatus()` | Startup load loaded N items successfully. | Normal operational status. |
| **29** | `"Loaded N item(s) from Library.txt. M file(s) not found (shown greyed out)."` | Status Bar (`SetStatus`) | `TapedeckForm.LoadedStatus()` | Some tracks point to missing file paths. | Reconnect drive or remove missing entries with **Delete**. |
| **30** | `"Library.txt could not be read, so the library starts empty.\n\n<Exception>"` | Modal Warning Dialog (`ShowError`) + Status Bar | `TapedeckForm.ReadLibraryFile` | `IOException` reading `Library.txt`. | Verify `Library.txt` is not locked by another process. |
| **31** | `"No permission to read Library.txt, so the library starts empty.\n\n<Exception>"` | Modal Warning Dialog (`ShowError`) + Status Bar | `TapedeckForm.ReadLibraryFile` | `UnauthorizedAccessException` on `Library.txt`. | Move application folder out of system-protected folders into `Documents`. |
| **32** | `"Favorites.txt could not be read, so favorites and ratings were not loaded.\n\n<Exception>"` | Modal Warning Dialog (`ShowError`) + Status Bar | `TapedeckForm.ApplyFavoritesFile` | `IOException` reading `Favorites.txt`. | Close any open editors holding locks on `Favorites.txt`. |
| **33** | `"No permission to read Favorites.txt, so favorites and ratings were not loaded.\n\n<Exception>"` | Modal Warning Dialog (`ShowError`) + Status Bar | `TapedeckForm.ApplyFavoritesFile` | `UnauthorizedAccessException` reading `Favorites.txt`. | Check file permissions on `Favorites.txt`. |
| **34** | `"<N> damaged line(s) in <fileName> were skipped.\nThe rest of the library was loaded."` | Modal Warning Dialog (`ShowError`) + Status Bar | `TapedeckForm.ReportSkippedLines` | Malformed lines detected in `Library.txt` or `Favorites.txt`. | Open file via `File > Open Library in Notepad` to fix syntax errors, then use `File > Reload Library`. |
| **35** | `"Saved N item(s) to Library.txt."` | Status Bar (`SetStatus`) | `TapedeckForm.SaveWithStatus` | User manually saved library via `Ctrl+S`. | Normal confirmation of successful persistence. |
| **36** | `"The library could not be saved.\n\n<Exception>\n\nIf the program is in a protected folder such as Program Files, move it to Documents and try again."` | Modal Warning Dialog (`ShowError`) + Status Bar | `TapedeckForm.TrySaveLibrary` | `IOException` while writing `Library.txt` or `Favorites.txt`. | Ensure disk has free space and file is not locked by external text editors. |
| **37** | `"No permission to save the library.\n\n<Exception>\n\nIf the program is in a protected folder such as Program Files, move it to Documents and try again."` | Modal Warning Dialog (`ShowError`) + Status Bar | `TapedeckForm.TrySaveLibrary` | `UnauthorizedAccessException` saving catalog. | Relocate Tapedeck directory into `C:\Users\<User>\Documents`. |
| **38** | `"Library.txt opened in Notepad. After saving your changes there, use File > Reload Library (Ctrl+Shift+L)."` | Status Bar (`SetStatus`) | `TapedeckForm.btnOpenNotepad_Click` | User opened library in Notepad (`Ctrl+L`). | Save changes in Notepad and press `Ctrl+Shift+L` in Tapedeck to reload. |
| **39** | `"Notepad could not be started.\n\n<Exception>"` | Modal Warning Dialog (`ShowError`) + Status Bar | `TapedeckForm.btnOpenNotepad_Click` | `Win32Exception` / `InvalidOperationException` spawning `notepad.exe`. | Verify `notepad.exe` exists in `Windows\System32` and system environment PATH. |
| **40** | `"Select an item to remove first."` | Status Bar (`SetStatus`) | `TapedeckForm.btnRemove_Click` | Remove clicked with no row selected. | Highlight a track in the playlist before pressing Remove or Delete. |
| **41** | `"Removed: <Artist - Title>"` | Status Bar (`SetStatus`) | `TapedeckForm.btnRemove_Click` | Item successfully deleted from playlist. | Track removed from active playlist and persistent text file. |
| **42** | `"Sorted by <SortKey> (insertion sort)."` | Status Bar (`SetStatus`) | `TapedeckForm.SortLibrary` | User sorted playlist via header or `Ctrl+1..5`. | Playlist reordered; playing track maintained. |
| **43** | `"Enter a title or artist to search."` | Status Bar (`SetStatus`) | `TapedeckForm.SearchLibrary` | Search invoked with empty or whitespace string. | Enter search text into `txtSearch` before searching. |
| **44** | `"The library is empty."` | Status Bar (`SetStatus`) | `TapedeckForm.SearchLibrary` | Search invoked on 0-item playlist. | Add files to library before searching. |
| **45** | `"No match for \"<Text>\"."` | Status Bar (`SetStatus`) | `TapedeckForm.SearchLibrary` | Search query not found in any title or artist. | Check spelling or search for broader partial keyword. |
| **46** | `"Found \"<Text>\" using binary search."` | Status Bar (`SetStatus`) | `TapedeckForm.SearchLibrary` | Exact title match located via binary search. | Matching track selected and focused in playlist view. |
| **47** | `"Found \"<Text>\" using partial-match search."` | Status Bar (`SetStatus`) | `TapedeckForm.SearchLibrary` | Partial substring match located in title or artist. | Matching track selected and focused in playlist view. |
| **48** | `"Select an item to mark as favorite."` | Status Bar (`SetStatus`) | `TapedeckForm.ToggleFavorite` | Favorite button clicked with nothing selected or playing. | Select a track in `listView1` before toggling favorite. |
| **49** | `"Marked as favorite: <Artist - Title>"` | Status Bar (`SetStatus`) | `TapedeckForm.ToggleFavorite` | Track marked favorite (`Ctrl+D`). | Favorite heart indicator added; saved to `Favorites.txt`. |
| **50** | `"Removed from favorites: <Artist - Title>"` | Status Bar (`SetStatus`) | `TapedeckForm.ToggleFavorite` | Track unmarked from favorites. | Favorite removed; updated in `Favorites.txt`. |
| **51** | `"Select an item to rate."` | Status Bar (`SetStatus`) | `TapedeckForm.SetRating` | Rating changed with nothing selected in playlist. | Highlight a playlist item before changing rating dropdown. |
| **52** | `"Rated <Stars>: <Artist - Title>"` | Status Bar (`SetStatus`) | `TapedeckForm.SetRating` | Star rating (1 to 5) applied to track. | Rating saved to `Favorites.txt` and rendered in playlist. |
| **53** | `"Rating cleared: <Artist - Title>"` | Status Bar (`SetStatus`) | `TapedeckForm.SetRating` | Track set to `Not rated` (0 stars). | Rating cleared; updated in `Favorites.txt`. |
| **54** | `"Something went wrong:\n\n<Exception>"` | Global Error Dialog (`Application.ThreadException`) | `Program.Main` | Unhandled runtime exception trapped by global top-level handler. | Copy the exception message text. Restart Tapedeck and report the bug in `docs/05_Testing.md` Section 6.3. |

---

## 7.11 About Dialog & Team Information (FR9)

Selecting `Help > About` (or pressing **F1**) opens the modal About window, reusing the custom retro cassette splash screen (`SplashForm`).

### Team Credentials & Roles
| Member | Role | Responsibilities |
|---|---|---|
| **Joey Cuison** | Team Lead / Architecture | Foundation classes, LibVLCSharp integration, UI layout, playback engine, menu strip, integration |
| **Heaven** | Library & Deployment | `LibraryStore.cs`, `FavoritesStore.cs`, form library wiring, UML diagrams, deployment guide |
| **Ammaar Azmy** | Sorting & Searching | `LibrarySorter.cs`, insertion sort, binary search, partial match search, requirements |
| **Christian Maddox Rivera (Maddox)** | Design & Visual Assets | Retro cassette skin, Canva artwork, icons, `SplashForm.cs`, credits and presentation |
| **Valencia Genevieve (Genevieve)** | Quality Assurance & Documentation | Favorites & star ratings (`TapedeckForm.Favorites.cs`), test plan & bug log (`05_Testing.md`), user manual (`06_User_Manual.md`), portfolio assembly |

The About window can be dismissed at any time by clicking anywhere on the screen or pressing any key.
