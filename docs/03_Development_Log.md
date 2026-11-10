# 4. Incremental Development Log

One entry per milestone. Each increment compiles and runs before it is committed. *Problems met* and *What changed* reflect what actually happened building each milestone in Visual Studio / `dotnet build`.

## M1 – Analysis and design (due Nov 10)
- **Goals:** requirements list, use case and class diagrams, design concept sketch, private repository with all members and the instructor invited.
- **Completed:**
  - Repository created and renamed to `Tapedeck-OOP10`, `.gitignore`, `README.md`, `PRD.md` with the full task division (`5199d3d`).
  - Requirements specification – problem statement, target users, scope, FR1–FR13, NFR1–NFR7, data format (`docs/01_Requirements.md`).
  - Use case diagram with descriptions and two sequence diagrams, Play and Load Library (`docs/02a_UML_Use_Case_and_Sequence.md`, Heaven, PR #10 `docs/uml-deployment`).
  - Class diagram with the OOP mapping (`docs/02b_UML_Class_and_Sorting.md`, `docs/diagrams/class_diagram.puml`).
  - Design concept and Canva source link (`Docs/Design/`, Maddox, PR #8).
- **Changed from plan:** NFR7 (performance) was added while writing the requirements specification.
- **Problems met:** the group number and section were written two ways ("Group 3 · BSCS 2A" and "OOP Group 2 · BSCS 2-A"). We first settled on "OOP Group 2", then corrected it to **OOP Group 3 · BSCS 2-A** (the group's actual number) and changed it everywhere (README, PRD, `.csproj`, docs, skin).
- **Next steps:** M2 core classes (already underway below).

## M2 – Core logic (due Nov 10)
- **Goals:** `MediaItem` hierarchy, `LibraryStore` with file I/O and exception handling, working add and save/load, first test cases.
- **Completed:**
  - `ea02431` – `IMediaControls`, `RepeatMode`, `TimeFormat`.
  - `c673813` – abstract `MediaItem` with validated properties (Title/Artist fallback text, clamped Rating, `FieldSeparator`).
  - `f61e618` – `AudioItem` and `VideoItem`.
  - `4db4698` – `MediaFormats` supported-extension consts, dialog filter, and the single-switch `Create` factory.
  - `c18961e` – placeholder `Data/LibraryStore.cs`, `Data/FavoritesStore.cs`, `Core/LibrarySorter.cs` so the solution builds before Heaven/Ammaar replace them with the real file I/O and sorting logic.
- **Changed from plan:** `MediaFormats.Create` is the only place that switches on media type (`"Audio"` / `"Video"`); every other class treats the playlist as `List<MediaItem>` polymorphically, per the course rule against type-check chains.
- **Also completed:** `LibraryStore` with real file I/O – damaged lines skipped and counted, atomic save through a `.tmp` file (Heaven, PR #1 `feature/library-store`); `TapedeckForm.Library.cs` load / save / remove / reload / open in Notepad (Heaven, PR #2); first test cases TC01–TC24 (Genevieve, PR #6 `test/test-plan`).
- **Problems met:** none — each class built clean on first pass.
- **Next steps:** M3 player, skin and features (also underway).

## M3 – Player and skin (due Nov 10)
- **Goals:** form inheriting from `BasePlayerForm`, working controls, skin, splash screen, menu strip, elapsed time, sorting, shuffle and repeat, enhancement.
- **Completed:**
  - `fb6ef73` – `Theme` (all skin constants in one class) and starter skin PNGs.
  - `322bef3` – `BasePlayerForm` : `Form`, `IMediaControls`, wrapping LibVLCSharp (`LibVLC`, `MediaPlayer`, `VideoView`), shuffle, Off/All/One repeat, `EndReached`/`EncounteredError` marshalled back to the UI thread via `BeginInvoke`.
  - `f589b57` – `TapedeckForm` + `.Designer.cs` layout (980×620 borderless window, title bar, video well, library/now-playing group boxes) with placeholder partials for Heaven (`.Library.cs`), Ammaar (`.SortSearch.cs`) and Genevieve (`.Favorites.cs`).
  - `7498fc9` – `TapedeckForm.View.cs`: `RefreshListView`, `ShowNowPlaying`, row selection and double-click/Enter-to-play.
  - `408145e` – `TapedeckForm.AddMedia.cs`: add files/folder, duplicate and unsupported-format counting, async metadata read.
  - `3175dec` – `TapedeckForm.Playback.cs`: Play/Pause/Stop/Next/Previous wiring, shuffle/repeat buttons, volume, seek bar, 500 ms elapsed-time timer.
  - `f69d049` – `TapedeckForm.Menu.cs`: File/Playback/Library/Help menu with keyboard shortcuts calling the same handlers as the buttons.
  - `78f0217` – real `Program.cs`: global unhandled-exception handler, `SplashForm` shown first, then `TapedeckForm`.
  - `LibrarySorter`: hand-written insertion sort, binary search by title and partial-match search (Ammaar, PR #3 `feature/sorter`).
  - Splash screen with logo, group, section and member cards that closes after 3 s or on click / key (Maddox, PR #4 and #5 `feature/splash`); Canva skin, logo and app icon (Maddox, PR #7 `design/canva-skin`).
  - Sorting from the Library menu and column headers, and the search box with binary search then partial match (`TapedeckForm.SortSearch.cs`, Ammaar, PR #11 `feature/sort-search-form`).
  - **Retro cassette redesign:** the splash is now drawn as a cassette tape (`splash_bg.png`: charcoal shell, cream label, red / orange / green / teal stripes, reels window, bottom tab) with polaroid-style member cards; photos of any shape are cropped to a square in code. `tapedeck_skin.png` and the `Theme` colours use the same palette, so the player matches the splash. The skin no longer paints the title, group or menu text, so nothing appears twice.
  - Enhancement – favorites and 1–5 star ratings: file side done (`FavoritesStore`, Heaven); the form side (`TapedeckForm.Favorites.cs`, Genevieve) is **in progress**.
- **Changed from plan:** Play and Pause share one position on the skin and swap visibility instead of having separate always-visible buttons. `btnPlay_Click` (not the `Play` override) resolves the selected row, because `Next()` calls `Play()` directly and must not re-trigger row selection.
- **Problems met and fixes (PR #9):**
  - **Menu strip did not respond at 125%+ display scaling.** The menu was added in code at a fixed `y = 46` after the designer layout had been scaled, so the taller title-bar panel covered it and took its clicks. Fix: `PlaceMenu()` positions it under the scaled title bar and brings it to the front.
  - **Playlist columns were cut off** ("LAN… Un…") because column widths are not scaled with the display. Fix: `FitColumns()` shares the list width between the columns.
  - Missing XML comments on `TeamInfo`/`TeamMember` and a broken `</returns` tag in `LibrarySorter` (coding standard). Fixed; the build now has no warnings.
- **Still open:**
  - `TapedeckForm.Favorites.cs` is still the placeholder (Genevieve, FR8).
  - Retest the retro splash and skin on Windows (fonts Arial Black, Consolas and Segoe UI are expected there).
  - Retest the window at 100% and 125% scaling on Windows.
- **Next steps:** M4 – run all test cases on `main`, bug log, user manual, deployment guide, Release build.

## M4 – Testing, documentation, deployment (due Nov 12)
- **Goals:** test report and bug log, user manual, deployment guide, self-contained Release build, refactored code.
- **Status:** not started — depends on teammates' PRs landing first (library I/O, sorting, favorites, splash) and on Genevieve's test pass.
- **Next steps:** `J17` self-contained win-x64 publish once the feature set is complete; dev log and version-control record kept current as milestones close.

## M5 – Final submission (due Nov 14)
- **Goals:** recorded demonstration, complete documentation portfolio, final repository.
- **Status:** not started.

## Refactoring record

| # | Refactoring | Why |
|---|---|---|
| R1 | Shared `TimeFormat.ToClock(TimeSpan)` used by both `MediaItem.DurationText` and the elapsed-time label instead of duplicating hh:mm:ss formatting in two places. | One formatting rule, no drift between duration and elapsed display. |
| R2 | `MediaFormats.Create(type, …)` is the only `switch` on Audio/Video; `LibraryStore.ParseLine` (once Heaven replaces the placeholder) will call into it instead of branching itself. | Keeps the rest of the codebase polymorphic over `MediaItem`; no scattered type checks. |
| R3 | One `CreateItem` helper builds every menu entry in `TapedeckForm.Menu.cs`. | Avoids repeating shortcut-assignment and click-wiring 20+ times by hand. |
| R4 | `Theme.StyleImageButton` / `StyleTextButton` style every button from one place. | Skin logic lives in `Theme.cs` only (NFR4); a missing PNG falls back to text instead of crashing or leaving a blank button. |
| R5 | `btnPlay_Click` resolves the selected row; the `Play()` override does not. | `Next()`/`Previous()` call `Play()` directly — if row-selection logic lived in the override, calling `Next()` would re-read the old selected row instead of the new `CurrentIndex`. |
| R6 | `AddFilesAsync` awaits metadata into a local variable before calling `Playlist.Add`. | `Playlist.Add(await ReadMediaItemAsync(...))` would evaluate `Playlist` before the await suspends; a sort or remove during the await could target the wrong list state. |
| R7 | `TapedeckForm` split into one partial class per owner (`.View`, `.AddMedia`, `.Playback`, `.Menu`, `.Library`, `.SortSearch`, `.Favorites`). | Lets five people own whole files with zero merge conflicts (PRD Section 4), instead of five people editing one form file. |
| R8 | Kept only insertion sort in `Core/LibrarySorter.cs`; the starter selection and bubble sort methods were removed. | The guide says to keep the chosen algorithm and delete the others; no unused code. |
| R9 | `PlaceMenu()` and `FitColumns()` compute positions from the scaled layout instead of fixed pixel values. | One fix for every display scaling (NFR6) instead of separate layouts. |
