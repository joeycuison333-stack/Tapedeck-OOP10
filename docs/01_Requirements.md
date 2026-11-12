# 1. Requirements Specification

**Project:** Tapedeck – OOP10 Media Player  
**Group:** OOP Group 3 – BSCS 2-A  
**Author:** Ammaar · **Updated for M4 (all FRs/NFRs, FR8 corrected):** Joey Cuison, November 12, 2026

## 1.1 Functional Requirements

This section describes the functional requirements of Tapedeck and identifies the classes, methods, and components responsible for each requirement. All thirteen match `PRD.md` Section 2 and are implemented in the current code.

| ID | Functional Requirement | Status | Where it is implemented |
|---|---|---|---|
| FR1 | Add MP3 and MP4 files using file selection or folder selection. | Done | `TapedeckForm.AddMedia.cs` – `btnAdd_Click()`, `btnAddFolder_Click()`, `AddFilesAsync()`, `ReadMediaItemAsync()` (in `BasePlayerForm.cs`) |
| FR2 | Display media information, including title, artist, duration, file path, media type, and album or resolution. | Done | `Core/MediaItem.cs` – `Title`, `Artist`, `Duration`, `FilePath`, `MediaType`, `Extra`; `Core/AudioItem.cs`/`Core/VideoItem.cs`; `TapedeckForm.View.cs` – `RefreshListView()` |
| FR3 | Save the library and reload it when the application starts. | Done | `Data/LibraryStore.cs` – `Load()`, `Save()`, `ParseLine()`; `TapedeckForm.Library.cs` – `LoadLibraryFromFile()`, `ReadLibraryFile()`, `SaveLibraryToFile()`, `TrySaveLibrary()` |
| FR4 | Provide playback controls, including play, pause, stop, next, previous, volume, and seeking. | Done | `UI/BasePlayerForm.cs` – `Play()`, `Pause()`, `Stop()`, `Next()`, `Previous()`, `SetVolume()`, `Seek()`; `Core/IMediaControls.cs` – playback control interface; `TapedeckForm.Playback.cs` – button/shortcut event handlers |
| FR5 | Display the playlist and currently playing media. | Done | `TapedeckForm.View.cs` – `RefreshListView()`, `SelectRow()`, `ShowNowPlaying()`; `TapedeckForm.Playback.cs` – `OnNowPlayingChanged()` override |
| FR6 | Sort library items by title, artist, album or resolution, duration, and rating; search by exact title or partial title/artist. | Done | `Core/LibrarySorter.cs` – `Compare()`, `BinarySearchByTitle()`, `FindFirstContaining()`; `TapedeckForm.SortSearch.cs` – `SortLibrary()`, `SearchLibrary()`, `listView1_ColumnClick()` |
| FR7 | Handle missing files, corrupted library entries, unsupported formats, and an empty library. | Done | `Data/LibraryStore.cs` – `Load()`, `ParseLine()`; `Core/MediaFormats.cs` – `IsSupported()`, `Create()`; `TapedeckForm.Library.cs` – `ReadLibraryFile()`, `ReportSkippedLines()`, `LoadedStatus()`, `CountMissingFiles()`; `UI/BasePlayerForm.cs` – `Play()`'s `File.Exists` check, `Player_EncounteredError` |
| FR8 | Allow users to mark favorites and assign ratings from 1 to 5 stars, saved in `Favorites.txt`. | Done | `TapedeckForm.Favorites.cs` – `ToggleFavorite()`, `SetRating()`, `FavoriteText()`; `Data/FavoritesStore.cs` – `Save()`, `Apply()` |
| FR9 | Splash screen: logo, title, group, section, every member's photo, name and role; closes after ~3 s or on click/key. | Done | `UI/SplashForm.cs` – `BuildLayout()`, `BuildCards()`, `CloseSplash()` |
| FR10 | MenuStrip (File, Playback, Library, Help) with keyboard shortcuts calling the same methods as the buttons. | Done | `TapedeckForm.Menu.cs` – `BuildMenu()`, `CreateItem()` |
| FR11 | Elapsed time next to total length (00:01:12 / 00:03:25), updated at least once per second. | Done | `TapedeckForm.Playback.cs` – `tmrElapsed_Tick()` (500 ms timer); `Core/TimeFormat.cs` – `ToClock()` |
| FR12 | Hand-written insertion sort by title and artist (no `List.Sort`/`OrderBy`); keeps the currently playing item selected. | Done | `Core/LibrarySorter.cs` – `InsertionSort()`; `UI/BasePlayerForm.cs` – `SetPlaylist()` |
| FR13 | Shuffle on/off and Repeat Off/All/One; the next item starts automatically. | Done | `UI/BasePlayerForm.cs` – `Next()`, `PickRandomIndex()`, `PlayNextOrStop()`, `Repeat`/`ShuffleEnabled`; `TapedeckForm.Playback.cs` – `CycleRepeat()`, `SetShuffle()` |

## 1.2 Non-Functional Requirements

| ID | Requirement | Status | Where it is addressed |
|---|---|---|---|
| NFR1 | No crashes in the demo: every file/media operation in try-catch, no empty catch blocks, global error handler. | Done | Every file/media call site in `Data/`, `UI/BasePlayerForm.cs` and the `TapedeckForm.*` partials catches `IOException`/`UnauthorizedAccessException`/`VLCException` and reports through `ShowError()`; `Program.cs` sets `Application.SetUnhandledExceptionMode` and a `ThreadException` handler as a last resort |
| NFR2 | Every main action has a button **and** a keyboard shortcut; text readable on the skin. | Done | `TapedeckForm.Menu.cs` – every `CreateItem()` call pairs a menu action with a `Keys` shortcut that calls the same handler as its button; `Theme.cs` sets label/menu colours for contrast against the skin |
| NFR3 | Runs on Windows 10/11 64-bit without Visual Studio (self-contained Release). | Done | `Tapedeck.csproj` Release `PropertyGroup` (`RuntimeIdentifier=win-x64`, `SelfContained=true`); `docs/07_Deployment_Guide.md` documents the publish steps and release checklist. H13's test on a second, clean computer is still outstanding (see `docs/05_Testing.md`) |
| NFR4 | UI, data classes and file I/O in separate classes; all skin settings in `Theme.cs`. | Done | `Core/` (domain classes), `Data/` (file I/O only, no UI), `UI/` (forms); `Theme.cs` is the only class with skin colours/fonts/asset paths |
| NFR5 | Coding standards: PascalCase/camelCase, control prefixes, XML `///` comments, constants instead of magic values. | Done | Followed throughout; spot-checked during the M4 pass (see `docs/03_Development_Log.md`) |
| NFR6 | Works at 100% and 125% Windows display scaling. | Code in place; visual re-check still needed | `TapedeckForm.cs` – `FitColumns()`, `PlaceStatusBar()`; `TapedeckForm.Menu.cs` – `PlaceMenu()`. These were fixed for scaling during M3 (see R9 in the refactoring record) but have not been re-confirmed on an actual 125%-scaled display since |
| NFR7 | Long-running work (reading file metadata, scanning a folder, starting the playback engine) does not freeze the UI thread. | Done | `TapedeckForm.AddMedia.cs` – `AddFilesAsync()` awaits `ReadMediaItemAsync()` per file instead of blocking; `UI/BasePlayerForm.cs` – `PrepareEngineAsync()` starts the LibVLC engine on a background thread (added during M4, bug TD-01 in `docs/05_Testing.md`) |

## 1.3 Notes

The implementation references identify the main classes and methods responsible for each requirement. Some requirements involve multiple components working together, such as the user interface, media classes, and data storage classes.

This document originally covered only FR1–FR8 and no NFRs, written before FR9–FR13 (splash screen, menu, elapsed time, insertion sort, shuffle/repeat) and the FR8 favorites/ratings UI were finished, and before NFR7 was added to the PRD. It has been brought up to date for M4 against the actual code and `docs/03_Development_Log.md`. Every FR and NFR is implemented; NFR6 (display scaling) is implemented in code but still needs a human to confirm it visually on a real 125%-scaled screen, and NFR3 still needs H13's second-computer test — both are tracked in `docs/05_Testing.md`.
