# 2. Requirements Specification

**Project:** Tapedeck (OOP10 media player) · **Group 3 – BSCS 2A** · **Milestone:** M1 – Analysis and design

---

## 2.1 Problem statement
Students and casual listeners keep their music and videos in scattered folders. Opening each file from File Explorer is slow, there is no single list of what they own, and the default players do not remember a personal library in a simple, readable file.

**Tapedeck** is a desktop media player for Windows that gathers mp3 and mp4 files into one library, reads each file's information (title, artist, duration, album or resolution), saves the library to a plain text file (`Library.txt`) so it is there the next time the program starts, and plays the media through an original retro desktop skin.

## 2.2 Target users
| User | Needs |
|---|---|
| Student | Keep study music and lecture videos in one list; play, pause and skip quickly with the keyboard. |
| Casual listener | Add a whole music folder at once; shuffle or repeat; mark favorite songs and rate them. |
| Instructor (evaluator) | See inheritance, polymorphism, file I/O and error handling clearly in a working program. |

## 2.3 Scope
**In scope (the player will):**
- add mp3 and mp4 files one by one or a whole folder at a time;
- read and show title, artist, duration, file path, media type and album (audio) or resolution (video);
- save the library to `Library.txt` and reload it at start-up; save favorites and ratings to `Favorites.txt`;
- play, pause, stop, go to next / previous, change volume and seek; show the elapsed time;
- sort the library (hand-written insertion sort) and search it by title or artist;
- shuffle and repeat (Off / All / One);
- show a splash screen with the group and every member; offer every action through a menu with keyboard shortcuts;
- handle errors without crashing: missing files, damaged lines in `Library.txt`, unsupported formats, an empty library.

**Out of scope (the player will not):** stream from the internet, edit tags inside the media files, play formats other than mp3 and mp4, run on macOS or Linux, support several user accounts, or manage multiple playlists.

## 2.4 Functional requirements
Priorities follow Section 3.1 of the project guide: **Must** = required for a passing demo, **Should** = expected for full marks.

| ID | Requirement | Priority | Where it is implemented |
|---|---|---|---|
| FR1 | Add mp3 and mp4 files through a file dialog (also a whole folder). | Must | `TapedeckForm.AddMedia.cs`: `btnAdd_Click`, `btnAddFolder_Click`, `AddFilesAsync` |
| FR2 | Gather and display title, artist, duration, file path, media type, and album (audio) or resolution (video). | Must | `BasePlayerForm.ReadMediaItemAsync`; `AudioItem`, `VideoItem`; `TapedeckForm.View.cs`: `RefreshListView`, `ShowNowPlaying` |
| FR3 | Save the library to `Library.txt` and reload it when the program starts. | Must | `LibraryStore.Save` / `Load`; `TapedeckForm.Library.cs`: `LoadLibraryFromFile`, `TrySaveLibrary` |
| FR4 | Play, Pause, Stop, Next and Previous controls, plus volume and seek. | Must | `IMediaControls`; `BasePlayerForm` (virtual members); `TapedeckForm.Playback.cs` overrides |
| FR5 | A playlist list and a Now Playing area with the current item's information. | Must | `listView1`; `TapedeckForm.View.cs`: `ShowNowPlaying`, `OnNowPlayingChanged` |
| FR6 | Search or sort the library by title or artist (binary search on a list sorted by title). | Should | `LibrarySorter.BinarySearchByTitle`, `FindFirstContaining`; `TapedeckForm.SortSearch.cs`: `SearchLibrary` |
| FR7 | Handle errors gracefully: missing files, corrupt lines in `Library.txt`, unsupported formats, empty library. | Must | `LibraryStore.ParseLine` (skips damaged lines), `BasePlayerForm.Play` (missing file, empty library), `MediaFormats.IsSupported`, global handler in `Program.cs` |
| FR8 | Enhancement: **favorites and 1–5 star ratings**, saved in `Favorites.txt`. | Should | `MediaItem.IsFavorite` / `Rating`; `FavoritesStore`; `TapedeckForm.Favorites.cs`: `ToggleFavorite`, `SetRating` *(form side in progress)* |
| FR9 | Splash screen at start-up with logo, title, group and section, and each member's photo, name and role; closes after about 3 s or on click / key press. | Must | `SplashForm`, `TeamInfo`, `Program.Main` |
| FR10 | MenuStrip (File, Playback, Library, Help) with keyboard shortcuts; menu items call the same methods as the buttons. | Must | `TapedeckForm.Menu.cs`: `BuildMenu`, `CreateItem` |
| FR11 | Elapsed time next to the total length (e.g. `00:01:12 / 00:03:25`), updated at least once per second. | Must | `BasePlayerForm.ElapsedTime`; `tmrElapsed` (500 ms) and `tmrElapsed_Tick` |
| FR12 | Sort the playlist by at least title and artist with a hand-written insertion sort (no `List.Sort` / `OrderBy`); the list refreshes and keeps the current item. | Must | `LibrarySorter.InsertionSort`; `TapedeckForm.SortSearch.cs`: `SortLibrary` (sorts a copy, then `SetPlaylist`) |
| FR13 | Shuffle on/off and continuous play (Repeat Off / All / One); when an item ends the next one starts according to the mode. | Must | `BasePlayerForm.Next`, `PlayNextOrStop`, `PickRandomIndex`; `RepeatMode`; `TapedeckForm.CycleRepeat` |

## 2.5 Non-functional requirements
| ID | Requirement | How it is met |
|---|---|---|
| NFR1 | **Reliability:** no crashes in the demo. Every file and media operation is in try-catch; no empty catch blocks; errors are reported to the user. | try-catch in `TapedeckForm.Library.cs`, `AddMedia.cs`, `Theme`; `ShowError` / `SetStatus`; `Application.ThreadException` handler in `Program.cs`. |
| NFR2 | **Usability:** every main action has a button and a keyboard shortcut; text stays readable on the skin. | Menu shortcuts (Help > Keyboard Shortcuts, Ctrl+K); tooltips on every button. |
| NFR3 | **Portability:** runs on Windows 10/11 64-bit without Visual Studio. | Self-contained `win-x64` Release publish (`docs/07_Deployment_Guide.md`). |
| NFR4 | **Maintainability / separation of concerns:** UI, data classes and file I/O in separate classes; all skin settings in `Theme.cs`. | `Core/`, `Data/`, `UI/` folders; `ApplyTheme()` override. |
| NFR5 | **Coding standards:** PascalCase / camelCase, control prefixes, XML `///` comments, constants instead of magic values. | Section 6 of the project guide; the build has no XML-comment warnings. |
| NFR6 | **Display scaling:** usable at 100% and 125% Windows display scaling. | Menu and playlist columns are placed from the scaled layout (`PlaceMenu`, `FitColumns`). |
| NFR7 | **Performance:** start-up and loading a library of a few hundred items take under 2 seconds; elapsed time updates every 500 ms. | Plain text file read once; insertion sort on small lists. |

## 2.6 Data requirements
`Library.txt` (next to `Tapedeck.exe`, UTF-8), one item per line, fields separated by `|`:

```
Type|Title|Artist|Duration (hh:mm:ss)|FilePath|Album or Resolution
Audio|Sunrise|The Examples|00:03:25|C:\Music\sunrise.mp3|Morning Album
Video|Demo Clip|Sample Studio|00:02:10|C:\Videos\demo.mp4|1920x1080
```

`Favorites.txt`: `FilePath|1 or 0 (favorite)|rating 0–5`, only for items that are a favorite or rated.

A line with the wrong number of fields, a bad duration, an empty path or an unsupported extension is skipped and counted; the user is told how many lines were skipped.

## 2.7 Main use cases
The use case diagram and the full description of each use case are in [`02a_UML_Use_Case_and_Sequence.md`](02a_UML_Use_Case_and_Sequence.md). Summary:

| Use case | Requirement |
|---|---|
| Add Media | FR1, FR2 |
| Load / Save Library | FR3, FR7 |
| Play / Pause / Stop / Next / Previous | FR4, FR5, FR11, FR13 |
| Sort and Search Library | FR6, FR12 |
| Mark Favorite / Rate | FR8 |
| View Splash / About | FR9 |
