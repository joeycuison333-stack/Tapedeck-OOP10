# 1. Requirements Specification

**Project:** Tapedeck – OOP10 Media Player  
**Group:** OOP Group 3 – BSCS 2-A  
**Author:** Ammaar

## 1.1 Functional Requirements

This section describes the functional requirements of Tapedeck and identifies the classes, methods, and components responsible for each requirement.

| ID | Functional Requirement | Where it is implemented |
|---|---|---|
| FR1 | Add MP3 and MP4 files using file selection or folder selection. | `TapedeckForm.AddMedia.cs` – `AddFilesAsync()`, `ReadMediaItemAsync()` |
| FR2 | Display media information, including title, artist, duration, file path, media type, and album or resolution. | `Core/MediaItem.cs` – `Title`, `Artist`, `Duration`, `FilePath`, `MediaType`, `Extra`; `TapedeckForm.View.cs` – `RefreshListView()` |
| FR3 | Save the library and reload it when the application starts. | `Data/LibraryStore.cs` – `Load()`, `Save()`, `ParseLine()`; `TapedeckForm.Library.cs` – `LoadLibraryFromFile()`, `ReadLibraryFile()`, `SaveLibraryToFile()`, `TrySaveLibrary()` |
| FR4 | Provide playback controls, including play, pause, stop, next, previous, volume, and seeking. | `UI/BasePlayerForm.cs` – `Play()`, `Pause()`, `Stop()`, `Next()`, `Previous()`, `SetVolume()`, `Seek()`; `Core/IMediaControls.cs` – playback control interface; `TapedeckForm.Playback.cs` – playback event handlers |
| FR5 | Display the playlist and currently playing media. | `TapedeckForm.View.cs` – `RefreshListView()`, `SelectRow()`, `ShowNowPlaying()`; `TapedeckForm.Playback.cs` – `OnNowPlayingChanged()` override |
| FR6 | Sort library items by title, artist, album or resolution, duration, and rating; search by exact title or partial title/artist. | `Core/LibrarySorter.cs` – `Compare()`, `InsertionSort()`, `BinarySearchByTitle()`, `FindFirstContaining()`; `TapedeckForm.SortSearch.cs` – `SortLibrary()`, `SearchLibrary()` |
| FR7 | Handle missing files, corrupted library entries, unsupported formats, and an empty library. | `Data/LibraryStore.cs` – `Load()`, `ParseLine()`; `Core/MediaFormats.cs` – `IsSupported()`, `Create()`; `TapedeckForm.Library.cs` – `ReadLibraryFile()`, `ReportSkippedLines()`, `LoadedStatus()`, `CountMissingFiles()`; `UI/BasePlayerForm.cs` – playback error handling |
| FR8 | Allow users to mark favorites and assign ratings from 1 to 5 stars. | **Pending implementation** – `TapedeckForm.Favorites.cs` currently contains placeholder methods; `Data/FavoritesStore.cs` contains persistence methods. |

## 1.2 Notes

The implementation references identify the main classes and methods responsible for each requirement. Some requirements involve multiple components working together, such as the user interface, media classes, and data storage classes.

FR8 is still pending implementation. Its requirement is included in the specification, but the current user-interface methods are placeholders and do not yet provide working favorite or rating functionality.
