# Tapedeck – Product Requirements Document (PRD) and Task Division

| | |
|---|---|
| **Product** | Tapedeck – Winamp-style desktop media player |
| **Course** | OOP10 – Object-Oriented Programming with C# and Windows Forms |
| **Instructor** | Prof. Ulysses Raymond F. Monsale |
| **Group / Section** | Group 3 · BSCS 2A |
| **Team** | **Joey Cuison** (leader) · **Heaven** · **Ammaar Azmy** · **Christian Maddox Rivera (Maddox)** · **Valencia Genevieve (Genevieve)** |
| **Repository** | github.com/joeycuison333-stack/Tapedeck-OOP10 (private) |
| **Final due date** | Saturday, November 14, 2026 |
| **Canva skin source** | https://www.canva.com/d/xEJW69wTCj1GAl8 – page 1 is the skin. Export page 1 only, as PNG, **980 × 620 px**, to `Tapedeck/Assets/tapedeck_skin.png`. |
| **IDE (everyone)** | **Visual Studio 2026** + .NET desktop development workload + .NET 8.0 Runtime component. Keep `.sln` (do not convert to `.slnx`) and keep `net8.0-windows`. Antigravity is used only to run the prompts. |

---

## 1. Product overview
Tapedeck lists and plays **mp3** and **mp4** files. It reads each file's information (title, artist, duration, album or resolution), saves the library to **Library.txt**, reloads it at start-up, and plays media through an original **retro late-90s desktop skin** (silver bevelled window, navy title bar, cassette logo) designed in Canva. It is built on the instructor's base player using **inheritance, abstraction and polymorphism**, with **LibVLCSharp** as the playback engine.

**Target users:** students and casual listeners on Windows 10/11 who keep music and videos in scattered folders.
**Out of scope:** streaming, tag editing, formats other than mp3/mp4, macOS/Linux, multiple user accounts.

## 2. Functional requirements

| ID | Requirement | Priority | Owner |
|---|---|---|---|
| FR1 | Add mp3 and mp4 files through a file dialog (and a whole folder). | Must | **Joey** |
| FR2 | Show title, artist, duration, file path, media type, album (audio) or resolution (video). | Must | **Joey** |
| FR3 | Save the library to `Library.txt` and reload it when the program starts. | Must | **Heaven** |
| FR4 | Play, Pause, Stop, Next, Previous, volume and seek. | Must | **Joey** |
| FR5 | Playlist list and Now Playing area. | Must | **Joey** |
| FR6 | Search or sort by title or artist (sorting algorithm + binary search). | Should | **Ammaar** |
| FR7 | Handle missing files, corrupt lines, unsupported formats, empty library. | Must | **Heaven** (file) · **Joey** (playback) |
| FR8 | Enhancement: **Favorites and 1–5 star ratings**, saved in `Favorites.txt`. | Should | **Genevieve** (UI) · **Heaven** (file) |
| FR9 | Splash screen: logo, title, group, section, every member's photo, name and role; closes after ~3 s or on click/key. | Must | **Maddox** |
| FR10 | MenuStrip (File, Playback, Library, Help) with keyboard shortcuts calling the same methods as the buttons. | Must | **Joey** |
| FR11 | Elapsed time next to total length (00:01:12 / 00:03:25), updated ≥ once per second. | Must | **Joey** |
| FR12 | Hand-written **insertion sort** by title and artist (no List.Sort/OrderBy); keeps the current item. | Must | **Ammaar** |
| FR13 | Shuffle on/off and Repeat Off/All/One; the next item starts automatically. | Must | **Joey** |

## 3. Non-functional requirements
| ID | Requirement |
|---|---|
| NFR1 | No crashes in the demo: every file/media operation in try-catch, no empty catch blocks, global error handler. |
| NFR2 | Every main action has a button **and** a keyboard shortcut; text readable on the skin. |
| NFR3 | Runs on Windows 10/11 64-bit without Visual Studio (self-contained Release). |
| NFR4 | UI, data classes and file I/O in separate classes; all skin settings in `Theme.cs`. |
| NFR5 | Coding standards: PascalCase/camelCase, control prefixes, XML `///` comments, constants instead of magic values. |
| NFR6 | Works at 100% and 125% Windows display scaling. |

---

## 4. How we avoid waiting on each other

**Rule 1 – Joey builds the foundation first.** Joey pushes a **working player** to `main` by **Monday Nov 9, 12:00 noon**. It includes every shared class, the form and all playback. It also includes **placeholder versions** of everyone else's files, so the app already builds and runs.

**Rule 2 – Each member owns whole files.** Your job is to **replace your own placeholder files** with the real ones. You never edit a file owned by someone else, so there are no merge conflicts and **no member waits for another member**. You can merge in any order; every combination builds.

**Rule 3 – You can test your own feature right away.** The foundation can already add and play files, so each person sees their own feature working as soon as they replace their placeholder.

```
Tapedeck/
├─ Program.cs ................................ JOEY
├─ Core/
│  ├─ IMediaControls.cs, RepeatMode.cs, TimeFormat.cs  JOEY
│  ├─ MediaItem.cs, AudioItem.cs, VideoItem.cs ....... JOEY
│  ├─ MediaFormats.cs ........................ JOEY
│  └─ LibrarySorter.cs ....................... AMMAAR     (placeholder from Joey)
├─ Data/
│  ├─ LibraryStore.cs ........................ HEAVEN     (placeholder from Joey)
│  └─ FavoritesStore.cs ...................... HEAVEN     (placeholder from Joey)
├─ UI/
│  ├─ BasePlayerForm.cs ...................... JOEY
│  ├─ Theme.cs ............................... JOEY
│  ├─ TapedeckForm.cs / .Designer.cs ...... JOEY
│  ├─ TapedeckForm.Playback.cs ............ JOEY
│  ├─ TapedeckForm.View.cs ................ JOEY
│  ├─ TapedeckForm.AddMedia.cs ............ JOEY
│  ├─ TapedeckForm.Menu.cs ................ JOEY
│  ├─ TapedeckForm.Library.cs ............. HEAVEN     (placeholder from Joey)
│  ├─ TapedeckForm.SortSearch.cs .......... AMMAAR     (placeholder from Joey)
│  ├─ TapedeckForm.Favorites.cs ........... GENEVIEVE  (placeholder from Joey)
│  ├─ TeamInfo.cs, SplashForm.cs ............. MADDOX     (placeholder from Joey)
└─ Assets/ ................................... MADDOX (Joey pushes starter images; Maddox replaces them with the Canva versions)
docs/
├─ 00_Cover.md, 05_Testing.md, 06_User_Manual.md, 08_Presentation_and_Reflections.md, 10_Appendix_Source.md  GENEVIEVE
├─ 01_Requirements.md, 02b_UML_Class_and_Sorting.md, diagrams/class_diagram.*  AMMAAR
├─ 02a_UML_Use_Case_and_Sequence.md, 07_Deployment_Guide.md, diagrams/use_case*, sequence*, test-data/  HEAVEN
├─ 03_Development_Log.md, 04_Version_Control.md  JOEY
└─ 09_Credits.md, design/  MADDOX
README.md, PRD.md, .gitignore, .sln, .csproj ... JOEY
```
The placeholders are in `integration/placeholders/` in the project ZIP.

## 5. Roles

| Member | Role | Area |
|---|---|---|
| **Joey Cuison** | Team lead | Foundation (shared classes, playback engine, form, playback, playlist view, adding files, menu, theme), merging pull requests, release build, development log, version-control record, demo video |
| **Heaven** | Library and deployment | Library.txt and Favorites.txt saving/loading, load/save/remove/reload in the form, use case and sequence diagrams, test data, deployment guide and release test |
| **Ammaar Azmy** | Sorting and searching | Insertion sort and binary search, sort/search in the form, requirements specification, class diagram, sorting explanation |
| **Christian Maddox Rivera (Maddox)** | Design and splash screen | Team info and splash screen, Canva skin/logo/icon, design concept, credits, slides |
| **Valencia Genevieve (Genevieve)** | Favorites and quality | Favorites and star ratings, test plan, test results and bug log, user manual, presentation plan, portfolio assembly |

Everyone needs **at least 10 meaningful commits** from their **own GitHub account**. Your grade is multiplied by a contribution factor (0.7–1.0) based on commits, your role, peer evaluation and how well you explain your code.

---

## 6. Detailed tasks per member

### How every member works (read first)
1. Accept the GitHub invite. Clone the repo into its own folder:
   `git clone https://github.com/joeycuison333-stack/Tapedeck-OOP10.git`
2. Download `Tapedeck-reference.zip` and extract it to Documents (PowerShell: `Expand-Archive "$env:USERPROFILE\Downloads\Tapedeck-reference.zip" "$env:USERPROFILE\Documents" -Force`). This gives `Documents\Tapedeck-reference`, the finished reference. **Never commit from it**.
3. Wait for Joey's message: **"foundation is in main"** (Mon Nov 9, noon). Then, for each task: `git checkout main` → `git pull` → `git checkout -b <your-branch>`.
4. Build your file with the **Antigravity prompt** (section 7). It **replaces the placeholder** with the same file name. Compare with the same file in `Tapedeck-reference` and make sure you can explain every line.
5. Build in Visual Studio (**Ctrl+Shift+B**) – it must succeed – then commit with the exact message given, and push.
6. Open a **pull request** to `main`. Joey reviews and merges.
7. Only touch **your own files** (section 4). Commit prefixes: `feat:` `fix:` `docs:` `test:` `refactor:` `build:` `chore:`.

---

### 🎵 JOEY CUISON – Team lead: foundation, playback and integration
**Branches:** `main` (setup), `feature/foundation`, `release/v1`, `docs/lead`
**Also:** review and merge every PR, keep `main` building, meeting log, submissions.

**Part 0 – Set up your laptop (once, ~10 minutes)**
1. **Visual Studio Installer:** workload **.NET desktop development** + individual component **.NET 8.0 Runtime (LTS)**.
2. **Rename the GitHub repo** (it was created as `Pulse-Player-OOP10`): on GitHub open the repo → **Settings** → Repository name → `Tapedeck-OOP10` → **Rename**. Then add the 4 members and the instructor under **Settings → Collaborators**.
3. **Get the reference project on the laptop:** download `Tapedeck-reference.zip` on the laptop, then in a VS **Developer PowerShell** tab:
   ```powershell
   Expand-Archive "$env:USERPROFILE\Downloads\Tapedeck-reference.zip" "$env:USERPROFILE\Documents" -Force
   Test-Path "$env:USERPROFILE\Documents\Tapedeck-reference\Tapedeck.sln"   # must print True
   ```
4. **Point your clone at the renamed repo and set your Git name** (inside your cloned repo folder):
   ```powershell
   cd "C:\Users\Joey Cuison\source\repos\joeycuison333-stack\Pulse-Player-OOP10"
   git remote set-url origin https://github.com/joeycuison333-stack/Tapedeck-OOP10.git
   git config --global user.name "Joey Cuison"
   git config --global user.email "<the email on your GitHub account>"
   ```

**Part 1 – Foundation (Sun Nov 8 night → Mon Nov 9, 12:00).** One commit per step below.
The script `integration/joey_foundation.ps1` does J1–J14 for you: it copies each step's files from `Tapedeck-reference`, builds after each step (J9–J13 build together at J14, because the layout needs every form part), commits with the exact message, and pushes when everything builds. Run it from inside your cloned repo:
```powershell
powershell -ExecutionPolicy Bypass -File "$env:USERPROFILE\Documents\Tapedeck-reference\integration\joey_foundation.ps1"
```
Then in Visual Studio: **File → Open → Project/Solution → `Tapedeck.sln`** (click **No** if asked to convert to .slnx), press **F5**, and post **"foundation is in main"**. Read every file you committed – you present them.

| # | Task | Files | Commit message |
|---|---|---|---|
| J1 | Create private repo, invite 4 members + instructor; push setup. Use `integration/Program.setup.cs` as the first `Program.cs`. | `.gitignore`, `README.md`, `PRD.md`, `Tapedeck.sln`, `Tapedeck/Tapedeck.csproj`, `Tapedeck/Program.cs` | `chore: initial solution, project file, README and PRD` |
| J2 | Interface and small core types | `Core/IMediaControls.cs`, `Core/RepeatMode.cs`, `Core/TimeFormat.cs` | `feat: add IMediaControls, RepeatMode and TimeFormat` |
| J3 | Abstract media item | `Core/MediaItem.cs` | `feat: add abstract MediaItem with validated properties` |
| J4 | Audio and video items | `Core/AudioItem.cs`, `Core/VideoItem.cs` | `feat: add AudioItem and VideoItem` |
| J5 | Supported formats and factory | `Core/MediaFormats.cs` | `feat: add supported formats and MediaItem factory` |
| J6 | Theme + starter images | `UI/Theme.cs`, `Assets/*` | `feat: add Theme and starter skin images` |
| J7 | Placeholders for teammates' non-form files | from `integration/placeholders/`: `Data/LibraryStore.cs`, `Data/FavoritesStore.cs`, `Core/LibrarySorter.cs`, `UI/TeamInfo.cs`, `UI/SplashForm.cs` | `chore: add placeholders for Heaven, Ammaar and Maddox files` |
| J8 | Base player | `UI/BasePlayerForm.cs` | `feat: add BasePlayerForm with LibVLCSharp playback, shuffle and repeat` |
| J9 | Form layout + main form + form placeholders | `UI/TapedeckForm.Designer.cs`, `UI/TapedeckForm.cs`, placeholders `UI/TapedeckForm.Library.cs`, `.SortSearch.cs`, `.Favorites.cs` | `feat: add main form layout and theme wiring` |
| J10 | Playlist view and Now Playing | `UI/TapedeckForm.View.cs` | `feat: add playlist view and Now Playing area` |
| J11 | Add files and folders | `UI/TapedeckForm.AddMedia.cs` | `feat: add files and folders to the library` |
| J12 | Playback controls, shuffle/repeat, volume, seek, elapsed time | `UI/TapedeckForm.Playback.cs` | `feat: add playback controls and elapsed time` |
| J13 | Menu strip with shortcuts | `UI/TapedeckForm.Menu.cs` | `feat: add menu strip with keyboard shortcuts` |
| J14 | Real Program.cs: splash then player, global error handler → **push, build, post "foundation is in main"** | `Program.cs` | `feat: show splash then player with global error handler` |

**Part 2 – Rest of the week**

| # | Task | Files | Commit message | Due |
|---|---|---|---|---|
| J15 | Merge everyone's PRs, build after each merge | — | (merge commits) | Nov 10 |
| J16 | Fix bugs in your files from Genevieve's bug log (one commit per bug) | your files | `fix: …` | Nov 11 |
| J17 | Release build: self-contained win-x64 publish, `G3_BSCS2A_Release.zip` | `.csproj` | `build: configure self-contained Release publish` | Nov 11 |
| J18 | Development log (all milestones, refactoring record) | `docs/03_Development_Log.md` | `docs: incremental development log` | Nov 12 |
| J19 | Version-control record: role matrix, meeting log, merge note, commit screenshot | `docs/04_Version_Control.md` | `docs: version control and collaboration record` | Nov 13 |
| J20 | Record the demo (everyone narrates their part), final README, tag `v1.0`, submit | `README.md` | `docs: final README and demo link` | Nov 14 |

**Presentation:** live demo of playback (2 min) + version-control evidence (1 min).

---

### 💾 HEAVEN – Library saving, loading and deployment
**Branches:** `feature/library-store`, `feature/library-form`, `docs/uml-deployment`
**Your files:** `Data/LibraryStore.cs`, `Data/FavoritesStore.cs`, `UI/TapedeckForm.Library.cs`, `docs/02a_UML_Use_Case_and_Sequence.md`, `docs/07_Deployment_Guide.md`, `docs/diagrams/use_case_diagram.png`, `docs/diagrams/sequence_*`, `docs/test-data/`

| # | Task | Files | Commit message | Due |
|---|---|---|---|---|
| H1 | Save Library.txt (write .tmp, then move) | `Data/LibraryStore.cs` | `feat: save library to Library.txt` | Nov 9 |
| H2 | Load Library.txt (missing file → empty, skip blank lines, count damaged lines) | same | `feat: load Library.txt and count damaged lines` | Nov 9 |
| H3 | `ParseLine` (6 fields, duration, path, `MediaFormats.Create`) | same | `feat: parse Library.txt lines` | Nov 9 |
| H4 | Save Favorites.txt | `Data/FavoritesStore.cs` | `feat: save favorites and ratings` | Nov 9 |
| H5 | Apply Favorites.txt to the loaded items | same | `feat: load favorites and ratings` | Nov 9 |
| H6 | Load at start-up and save in the form | `UI/TapedeckForm.Library.cs` | `feat: load and save the library in the form` | Nov 10 |
| H7 | Remove items (stop if playing, fix CurrentIndex) | same | `feat: remove items from the library` | Nov 10 |
| H8 | Reload, open in Notepad, count missing files | same | `feat: reload library and open it in Notepad` | Nov 10 |
| H9 | Corrupt Library.txt for testing | `docs/test-data/Library_corrupt.txt` | `test: add corrupt Library.txt test data` | Nov 10 |
| H10 | Use case diagram and use case descriptions | `docs/02a_…md` §3.1, `docs/diagrams/use_case_diagram.png` | `docs: use case diagram and descriptions` | Nov 10 |
| H11 | Sequence diagrams (Play, Load at start-up) | `docs/02a_…md` §3.3, `docs/diagrams/sequence_*` | `docs: sequence diagrams` | Nov 10 |
| H12 | Deployment guide | `docs/07_Deployment_Guide.md` | `docs: deployment guide` | Nov 12 |
| H13 | Publish the Release on your own PC, run the deployment checklist, record results | `docs/07_Deployment_Guide.md` §8.5 | `test: record release test on a second computer` | Nov 12 |

**Presentation:** live demo of the library (1 min): add a folder, close and reopen, the error case with a renamed file.

---

### 🧩 AMMAAR AZMY – Sorting and searching
**Branches:** `feature/sorter`, `feature/sort-search-form`, `docs/requirements-class`
**Your files:** `Core/LibrarySorter.cs`, `UI/TapedeckForm.SortSearch.cs`, `docs/01_Requirements.md`, `docs/02b_UML_Class_and_Sorting.md`, `docs/diagrams/class_diagram.*`

| # | Task | Files | Commit message | Due |
|---|---|---|---|---|
| A1 | `Compare(a, b, key)` for all sort keys | `Core/LibrarySorter.cs` | `feat: compare media items by sort key` | Nov 9 |
| A2 | Hand-written insertion sort | same | `feat: add hand-written insertion sort` | Nov 9 |
| A3 | Binary search by title | same | `feat: add binary search by title` | Nov 9 |
| A4 | Partial match on title or artist | same | `feat: add partial-match search` | Nov 9 |
| A5 | `SortLibrary` (sorts a copy, keeps the playing item) | `UI/TapedeckForm.SortSearch.cs` | `feat: sort the playlist and keep the current item` | Nov 10 |
| A6 | Sort by clicking column headers | same | `feat: sort by clicking column headers` | Nov 10 |
| A7 | Search box | same | `feat: search the library` | Nov 10 |
| A8 | Requirements specification | `docs/01_Requirements.md` | `docs: requirements specification` | Nov 10 |
| A9 | Class diagram + OOP mapping | `docs/02b_…md` §3.2, `docs/diagrams/class_diagram.*` | `docs: class diagram` | Nov 10 |
| A10 | Insertion-sort trace, running time, why we chose it | `docs/02b_…md` §3.4 | `docs: sorting algorithm explanation` | Nov 10 |

**Presentation:** UML walk-through and sorting (2 min).

---

### 🎨 CHRISTIAN MADDOX RIVERA (MADDOX) – Skin design and splash screen
**Branches:** `feature/splash`, `design/canva-skin`, `docs/design`
**Your files:** `UI/TeamInfo.cs`, `UI/SplashForm.cs`, `Assets/` (all images), `docs/design/`, `docs/09_Credits.md`

| # | Task | Files | Commit message | Due |
|---|---|---|---|---|
| M1 | Team info with all five members | `UI/TeamInfo.cs` | `feat: add team info for all members` | Nov 9 |
| M2 | Five member photos | `Assets/Team/*.jpg` | `feat: add member photos` | Nov 9 |
| M3 | Splash layout: logo, title, group | `UI/SplashForm.cs` | `feat: add splash screen layout` | Nov 9 |
| M4 | Member cards with placeholder for a missing photo | same | `feat: show team cards on splash screen` | Nov 9 |
| M5 | 3-second timer and click/key to skip | same | `feat: close splash after 3 seconds or on click` | Nov 10 |
| M6 | Refine the shared Canva design (link at the top of this PRD), export page 1 as PNG at 980 × 620 (replace the starter background) | `Assets/tapedeck_skin.png` | `feat: add Canva skin background` | Nov 10 |
| M7 | Canva logo (512 × 512) and icon (.ico 16/32/48/256) | `Assets/logo.png`, `Assets/logo.ico` | `feat: add Canva logo and app icon` | Nov 10 |
| M8 | Design concept sketch + Canva view link | `docs/design/` | `docs: design concept and Canva source` | Nov 10 |
| M9 | Credits and licenses | `docs/09_Credits.md` | `docs: credits and licenses` | Nov 12 |
| M10 | Presentation slides (8 slides) | `docs/slides.pdf` | `docs: presentation slides` | Nov 13 |

**Presentation:** problem and design rationale (1 min).

---

### ⭐ VALENCIA GENEVIEVE (GENEVIEVE) – Favorites, testing and user manual
**Branches:** `test/test-plan`, `feature/favorites`, `docs/manual-portfolio`
**Your files:** `UI/TapedeckForm.Favorites.cs`, `docs/00_Cover.md`, `docs/05_Testing.md`, `docs/06_User_Manual.md`, `docs/08_Presentation_and_Reflections.md`, `docs/10_Appendix_Source.md`, `docs/screenshots/`

| # | Task | Files | Commit message | Due |
|---|---|---|---|---|
| G1 | Test plan: environment, test media, how to test | `docs/05_Testing.md` §6.1 | `docs: add test plan` | Nov 9 |
| G2 | Test cases TC01–TC12 | same | `test: add test cases for adding, files and playback` | Nov 9 |
| G3 | Test cases TC13–TC24 | same | `test: add test cases for sorting, shuffle, repeat and favorites` | Nov 9 |
| G4 | Toggle favorite + ♥ button | `UI/TapedeckForm.Favorites.cs` | `feat: toggle favorites` | Nov 10 |
| G5 | Star rating (0–5) | same | `feat: add star ratings` | Nov 10 |
| G6 | ♥ ★ column text | same | `feat: show favorites and ratings in the playlist` | Nov 10 |
| G7 | Run all 24 tests on `main`, fill in Actual / Pass-Fail | `docs/05_Testing.md` | `test: record test results` | Nov 11 |
| G8 | Bug log (tell the file owner about each bug) | same §6.3 | `docs: bug log` | Nov 11 |
| G9 | User manual | `docs/06_User_Manual.md` | `docs: user manual` | Nov 12 |
| G10 | Annotated screenshots | `docs/screenshots/`, `docs/06_User_Manual.md` | `docs: user manual screenshots` | Nov 12 |
| G11 | Presentation plan; collect everyone's reflections | `docs/08_Presentation_and_Reflections.md` | `docs: presentation plan and reflections` | Nov 13 |
| G12 | Cover page + assemble the portfolio → `G3_BSCS2A_Portfolio.pdf` | `docs/00_Cover.md`, `docs/10_Appendix_Source.md` | `docs: final documentation portfolio` | Nov 14 |

**Presentation:** favorites demo (1 min) + testing and lessons learned (2 min).

---

## 7. Antigravity prompts
Open **your clone** of the repo in Antigravity (Agent mode). Paste the **Context** block first in every new conversation, then the prompt for your task. When the agent finishes, open the same file in `Tapedeck-reference` and ask the agent to compare the two.

### 7.0 Context (everyone, paste first)
```
Project: Tapedeck, an OOP10 media player in C# Windows Forms (.NET 8, net8.0-windows, x64), built with Visual Studio 2026. Do not change the target framework or convert the .sln.
Playback engine: LibVLCSharp.WinForms 3.9.0 + VideoLAN.LibVLC.Windows 3.0.21.
Namespace: mediaPlayerPrj (file-scoped). Folders: Core/ (data classes, algorithms), Data/ (file I/O), UI/ (forms, Theme).
TapedeckForm is split into partial files; each file has one owner. Some files are PLACEHOLDERS with the right
member names but empty bodies. When I ask you to write a file, REPLACE the whole placeholder and keep every
member name and signature that is already there. Never edit files I did not name.
Course rules – follow all of them:
- PascalCase classes/methods/properties, camelCase locals, control prefixes (btnPlay, lblTitle, trackVolume, listView1).
- XML /// comments on every public class and member.
- No repeated magic strings or numbers: use const.
- Wrap file and media operations in try-catch; never an empty catch; tell the user (SetStatus or ShowError).
- Short single-purpose methods. UI, data classes and file I/O stay in separate classes.
- Polymorphism: the playlist is List<MediaItem>; no long if/else type checks.
- Never use List.Sort, OrderBy or LINQ to sort the playlist.
- Library.txt line format: Type|Title|Artist|hh:mm:ss|FilePath|Album or Resolution (6 fields).
Read PRD.md and docs/01_Requirements.md before writing code.
```

### 7.1 JOEY prompts
**J2–J5 – Core classes**
```
Create, one file at a time (stop after each so I can commit):
Core/IMediaControls.cs – interface with Play, Pause, Stop, Next, Previous.
Core/RepeatMode.cs – enum Off, All, One.
Core/TimeFormat.cs – static ToClock(TimeSpan) -> "hh:mm:ss" (negative -> zero), const Zero "00:00:00"; XML summary says it replaces the duplicated FormatTime/DurationText logic (Appendix C.5 refactoring).
Core/MediaItem.cs – abstract: consts UnknownTitle, UnknownArtist, MaxRating 5, FieldSeparator '|'; validated properties Title (blank -> "Unknown Title"), Artist (blank -> "Unknown Artist"), Duration (negative -> zero), FilePath (null -> ""), IsFavorite, Rating (clamped 0-5); read-only DurationText, FileName, RatingStars; protected constructor; abstract MediaType, Extra, ExtraLabel; virtual ToFileLine() joining the 6 fields with '|' (a '|' inside a field becomes '/'); ToString() "Artist - Title".
Core/AudioItem.cs – Album (blank -> "Unknown Album"); Core/VideoItem.cs – Resolution (blank -> "Unknown Resolution").
Core/MediaFormats.cs – consts .mp3/.mp4 and DialogFilter (with "All files"); IsSupported, IsVideoFile; factory Create(type, title, artist, duration, path, extra) with ONE switch ("Audio" -> AudioItem, "Video" -> VideoItem, else null).
```

**J6 – Theme**
```
Create UI/Theme.cs (static) holding ALL skin settings: AssetsFolder, BackgroundFile "tapedeck_skin.png", LogoFile, IconFile, Borderless = true, TransparentColor, FormSize 980x620; TitleBarHeight 46, MenuHeight 24; colours BackColor and CardColor silver (192,192,192), AccentColor navy (0,0,128), AccentAltColor teal (0,128,128), LabelColor black, MutedColor (80,80,80), TitleTextColor white, TitleMutedColor (200,210,235), ListBackColor white, ListTextColor black, NowPlayingRowColor navy, NowPlayingTextColor white, ToggleOnColor (214,222,238), MenuBackColor silver, MenuHighlightColor (184,196,228); const FontName "Tahoma"; const names of every button PNG (btn_play, btn_pause, btn_stop, btn_next, btn_prev, btn_shuffle, btn_repeat, btn_close, btn_min, btn_heart).
LoadImage (FileNotFoundException when missing; copy through a MemoryStream so the file is not locked), LoadIcon, GetAssetPath, StyleImageButton and StyleTextButton giving classic raised buttons (FlatStyle.Standard, UseVisualStyleBackColor false, silver face; image buttons keep their text and return false if the image fails), EnableDrag (ReleaseCapture + SendMessage WM_NCLBUTTONDOWN/HTCAPTION), nested MenuColorTable : ProfessionalColorTable (silver menu, soft navy highlight).
```

**J8 – BasePlayerForm**
```
Create UI/BasePlayerForm.cs: concrete class BasePlayerForm : Form, IMediaControls with a parameterless constructor.
- Private LibVLC _libVLC, MediaPlayer _player, Random _random, string _loadedPath, bool _isPaused. Protected VideoView VideoSurface, List<MediaItem> Playlist, int CurrentIndex = -1. Public ShuffleEnabled, RepeatMode Repeat.
- Constructor returns early in design mode (LicenseManager.UsageMode). Otherwise Core.Initialize(), create engine, player and VideoView (Dock Fill, black); subscribe EndReached and EncounteredError; catch VLCException and InvalidOperationException -> ShowError.
- Protected IsDesignTime, IsPlaying, IsPaused, ElapsedTime, PlaybackPosition, CurrentItem; AttachVideo(Control); SetPlaylist(List<MediaItem>) keeping CurrentIndex on the same item.
- virtual Play (empty library message; resume if paused on the same file; missing file -> stop, SetStatus + ShowError; else play new Media(_libVLC, path, FromType.FromPath) and OnNowPlayingChanged), Pause, Stop, Next (shuffle -> random index that is not the current one), Previous, SetVolume(0-100), Seek(0-1).
- EndReached/EncounteredError run on a VLC thread: BeginInvoke to the UI thread. PlayNextOrStop: One -> Play(); shuffle/All/not last -> Next(); else Stop().
- protected virtual OnNowPlayingChanged, ApplyTheme, SetStatus, ShowError (MessageBox with TeamInfo.AppTitle).
- protected async Task<MediaItem> ReadMediaItemAsync(path): Media.Parse(ParseLocal, 5000); title (fallback file name), artist, duration, album; for mp4 the first video track's WidthxHeight.
- OnFormClosed: unsubscribe, stop, dispose VideoView, player, LibVLC.
```

**J9–J13 – The form**
```
1) UI/TapedeckForm.Designer.cs (980x620, AutoScaleMode Font, Tahoma 9): pnlHeader (0,0,980,46) – the title bar – with picLogo, lblAppTitle, lblGroup, btnMinimize (912,11,26x24), btnClose (942,11,26x24); the menu strip is placed at y=46 in code; pnlVideo (20,80,560,315) black with nothing on it; lblNowCaption "Now playing" (32,411) and lblLibrary "Library" (612,91) as group-box captions, lblTitle 16pt bold, lblArtist, lblExtra, lblElapsed (Consolas, right); trackSeek (0-1000); btnPrevious, btnPlay + btnPause (same spot), btnStop, btnNext, btnShuffle, btnRepeat, lblRepeat, lblVolume, trackVolume (0-100 = 80); lblLibrary, lblCount, txtSearch, btnSearch, listView1 (Details, columns Title, Artist, Album/Res, Length, "♥ ★"); btnAdd, btnAddFolder, btnRemove, btnFavorite, lblRating, cboRating ("Not rated".."★★★★★"); lblStatus (14,594,782x18) and lblCount (812,594,154x18) inside the two sunken status-bar panels; toolTip1. Wire every event to <control>_<Event>.
2) UI/TapedeckForm.cs: constants, fields (tmrElapsed, menuMain, shuffleItem, repeatItem, _boldListFont, _userIsSeeking, _updatingRating), constructor (InitializeComponent; return in design time; BuildMenu; ApplyTheme; AttachVideo(pnlVideo); SetVolume; tooltips; start 500 ms timer), OnShown -> LoadLibraryFromFile, overrides SetStatus, ShowError, ApplyTheme (title-bar labels white, captions navy on silver, classic buttons, borderless + EnableDrag on the title bar), close/minimize, OnFormClosing saves, OnFormClosed disposes.
3) UI/TapedeckForm.View.cs: SelectedIndex, SelectedItem, RefreshListView (rows with FavoriteText, tooltip type+path, grey if missing, current row navy with white bold text, keep selection, lblCount), SelectRow, ShowNowPlaying, double-click/Enter -> PlaySelected, Delete -> btnRemove_Click, SelectedIndexChanged updates cboRating (guarded) and the ♥ button text.
4) UI/TapedeckForm.AddMedia.cs: btnAdd_Click, btnAddFolder_Click (recursive, IgnoreInaccessible, supported only), AddFilesAsync (count added/unsupported/duplicates/failed; await into a variable before Playlist.Add; save; message), ContainsPath.
5) UI/TapedeckForm.Playback.cs: overrides Play/Pause/Stop/OnNowPlayingChanged; btnPlay_Click (picks the selected row – not inside the override, because Next() calls Play()); other buttons; SetShuffle, CycleRepeat, UpdateModeButtons (pressed-in face when on); volume, seek, tmrElapsed_Tick.
6) UI/TapedeckForm.Menu.cs: menu placed under the title bar (Dock None, Location (0, Theme.TitleBarHeight), width of the form); File / Playback / Library / Help with the shortcuts in docs/06_User_Manual.md 7.8; one CreateItem helper; items call the button handlers; ShowAbout, ShowShortcuts.
Then copy the placeholders TapedeckForm.Library.cs, .SortSearch.cs, .Favorites.cs from integration/placeholders/Tapedeck/UI/ and build. Stop after each numbered file so I can commit.
```

**J14 – Program**
```
Replace Tapedeck/Program.cs: SetUnhandledExceptionMode(CatchException) + ThreadException handler (MessageBox); ApplicationConfiguration.Initialize(); show SplashForm with ShowDialog in a using; Application.Run(new TapedeckForm()).
```

**J17 – Release**
```
Walk me through publishing this WinForms app as Release, win-x64, self-contained, not single-file, not trimmed, to bin\Release\publish; list what must be inside (exe, Assets, libvlc\win-x64) and a test checklist for another PC.
```

### 7.2 HEAVEN prompts
**H1–H3 – LibraryStore** *(replaces the placeholder)*
```
Replace Data/LibraryStore.cs (static class). Keep FileName "Library.txt" and DefaultPath. Add private consts FieldCount = 6 and the six field indexes.
Save(path, List<MediaItem> items): write every item.ToFileLine() to path + ".tmp" (UTF-8), then File.Move(temp, path, true) so a crash cannot corrupt the library.
Load(path, out int skippedLines): missing file -> empty list (first run); skip blank lines; ParseLine returns null for a damaged line, which is counted.
ParseLine(string line): Split on MediaItem.FieldSeparator; exactly 6 fields; TimeSpan.TryParse the duration; path not blank; then MediaFormats.Create(...).
XML comments must say callers catch IOException and UnauthorizedAccessException.
Do it in three steps (Save, Load, ParseLine) and stop after each so I can commit.
```

**H4–H5 – FavoritesStore** *(replaces the placeholder)*
```
Replace Data/FavoritesStore.cs (static). Keep FileName "Favorites.txt" and DefaultPath.
Save(path, items): write "FilePath|1or0|rating" only for items that are favorite or rated (UTF-8).
Apply(path, items, out int skippedLines): missing file -> nothing; build a case-insensitive Dictionary from FilePath to item; for each line split on '|', need 3 fields and an int rating, else count as skipped; set IsFavorite and Rating on the matching item.
Stop after Save so I can commit, then add Apply.
```

**H6–H8 – Library part of the form** *(replaces the placeholder)*
```
Replace UI/TapedeckForm.Library.cs ("public partial class TapedeckForm"; usings System.ComponentModel and System.Diagnostics). Keep the same member names as the placeholder.
1) LoadLibraryFromFile: LibraryStore.Load + FavoritesStore.Apply inside try-catch (IOException, UnauthorizedAccessException -> ShowError and start empty); CurrentIndex = -1; SetPlaylist(items); RefreshListView(); if lines were skipped ShowError("N damaged line(s)… were skipped"); status "Loaded N item(s)…" or "The library is empty. Add files with Ctrl+O." plus how many files are missing.
   SaveLibraryToFile: LibraryStore.Save + FavoritesStore.Save; catch IOException and UnauthorizedAccessException (hint: move the app to Documents). SaveWithStatus: save + status.
2) btnRemove_Click: nothing selected -> status; removing the playing item -> Stop(), CurrentIndex = -1, ShowNowPlaying(null); removing an item above it -> CurrentIndex--; RemoveAt; RefreshListView; save; status.
3) ReloadLibrary (Stop, ShowNowPlaying(null), LoadLibraryFromFile); btnOpenNotepad_Click (save first; Process.Start notepad.exe with the quoted path, UseShellExecute false; catch Win32Exception and InvalidOperationException; status tells the user to use File > Reload Library); CountMissingFiles.
Stop after each numbered part so I can commit.
```

**H10–H11 – Diagrams**
```
Read UI/BasePlayerForm.cs, UI/TapedeckForm.Playback.cs and Data/LibraryStore.cs. Write PlantUML for (1) a use case diagram with actors User and File System (Add Media, Play/Pause/Stop, Next/Previous, Volume/Seek, Sort, Search, Shuffle/Repeat, Favorite/Rate, Remove, Splash/About, Open in Notepad, Save Library, Load Library, Read Media Info, Report Error, Show Elapsed Time) with «include» Save Library and «extend» Report Error; (2) a sequence diagram "User presses Play" from the click to OnNowPlayingChanged, with an alt block for a missing file and a loop for the 500 ms timer; (3) a sequence diagram "Load Library.txt at start-up". Every message must match a real method name. Then write short descriptions (actor, main flow, alternate flow) for Add Media, Play Media, Save/Load Library, Sort, Search, Shuffle/Repeat and Favorite/Rate.
```

**H12–H13 – Deployment**
```
Using Tapedeck/Tapedeck.csproj, write the deployment guide: system requirements; Visual Studio publish steps (Release, net8.0-windows, win-x64, self-contained, no single file, no trimming); the dotnet publish command; what the publish folder must contain; how to zip it as G3_BSCS2A_Release.zip; a checklist to test it on a second computer; known limitations. Then give me a table to record my own test results.
```

### 7.3 AMMAAR prompts
**A1–A4 – LibrarySorter** *(replaces the placeholder)*
```
Replace Core/LibrarySorter.cs. Keep enum SortKey { Title, Artist, AlbumOrResolution, Duration, Rating } exactly as it is.
Static class LibrarySorter:
Compare(a, b, key): OrdinalIgnoreCase for Title, Artist and Extra; Duration.CompareTo; Rating reversed so 5 stars come first.
InsertionSort(List<MediaItem> items, SortKey key): written by hand – no List.Sort, OrderBy or LINQ.
BinarySearchByTitle(items, title): index or -1 (list must already be sorted by title).
FindFirstContaining(items, text): first index whose Title or Artist contains the text (OrdinalIgnoreCase), or -1.
In the class XML summary explain why insertion sort (stable, fast on nearly-sorted lists, in place), best O(n), worst O(n^2).
Add one method at a time and stop after each so I can commit.
```

**A5–A7 – Sort and search in the form** *(replaces the placeholder)*
```
Replace UI/TapedeckForm.SortSearch.cs ("public partial class TapedeckForm"). Keep the same member names as the placeholder.
SortLibrary(SortKey key): fewer than 2 items -> return; copy Playlist into a new List; LibrarySorter.InsertionSort(copy, key); SetPlaylist(copy) so the playing item stays current; RefreshListView(); SaveLibraryToFile(); SetStatus("Sorted by " + key + " (insertion sort).").  [commit]
listView1_ColumnClick: TitleColumn..RatingColumn -> the matching SortKey.  [commit]
public SearchLibrary(string text): trim; empty -> status; empty library -> status; SortLibrary(SortKey.Title); BinarySearchByTitle; if -1 FindFirstContaining; no match -> "No match for …"; else SelectRow, focus the list, status naming the method used. btnSearch_Click calls it; txtSearch_KeyDown on Enter (SuppressKeyPress).  [commit]
```

**A8–A10 – Docs**
```
1) Check docs/01_Requirements.md against PRD.md section 2 and the code: for every FR, confirm the "Where it is implemented" column names real methods. Fix anything wrong.
2) Check docs/02b_UML_Class_and_Sorting.md and docs/diagrams/class_diagram.puml against the code: every class, interface, inheritance arrow, override and association must match. List differences and fix the diagram source.
3) Write an insertion-sort trace by title on [Sunrise, Apple, Night, Bass] (pass, current item, comparisons and shifts, list after the pass), total comparisons and shifts, best/worst/average running time, and why we chose insertion sort over selection and bubble sort. Under 300 words.
```

### 7.4 MADDOX prompts
**M1 – Team info** *(replaces the placeholder)*
```
Replace UI/TeamInfo.cs. Keep class TeamMember (Name, Role, PhotoFile, constructor) and static TeamInfo with AppTitle "Tapedeck", GroupName, Section, LogoFile.
Members, in this order: Joey Cuison – "Team lead / player engine, UI and integration" – Team\joey.jpg; Heaven – "Library saving, loading and deployment" – Team\heaven.jpg; Ammaar Azmy – "Sorting and searching" – Team\ammaar.jpg; Christian Maddox Rivera – "Skin design and splash screen" – Team\maddox.jpg; Valencia Genevieve – "Favorites, testing and user manual" – Team\genevieve.jpg.
```

**M3–M5 – Splash screen** *(replaces the placeholder)*
```
Replace UI/SplashForm.cs (no designer file, follows Appendix C.2 of the course guide):
Step 1: borderless with a raised 3D border painted with ControlPaint.DrawBorder3D, CenterScreen, 784x370, Theme.BackColor/LabelColor, no taskbar button, KeyPreview; logo PictureBox 72x72 from TeamInfo.LogoFile; title 22pt bold in Theme.AccentColor; "Group | Section | OOP10" in Theme.MutedColor.  [commit]
Step 2: a FlowLayoutPanel at (24,112), 736x205, with one 144 px card per TeamInfo member: 96 px photo (Zoom), bold name, muted role. If the photo is missing or not an image (catch IOException and ArgumentException) show a grey placeholder and add " (no photo)" to the name.  [commit]
Step 3: a System.Windows.Forms.Timer of 3000 ms; close on tick, on any click (also on child controls) or key press by setting DialogResult = OK; hint label "Click or press any key to skip"; dispose the timer in OnFormClosed.  [commit]
```

**M6–M8 – Canva**
```
I am recreating our retro late-90s desktop skin in Canva at exactly 980 x 620 px (silver window with 3D bevels, navy-to-blue title bar, etched group boxes, sunken status bar – inspired by Win9x music players, but all our own graphics). Using Assets/tapedeck_skin.png and docs/design/design_concept_mockup.png as the layout, list every area and its pixel rectangle (title bar 0-46, menu row 46-70, sunken video well 18,78-581,396, "Now playing" group box 22,418-578,584, "Library" group box 602,98-958,584, playlist well 614,150-945,479, status panels 8,590-800,614 and 806,590-971,614), the colours from UI/Theme.cs, and a step-by-step Canva checklist for exporting the background PNG, a 512x512 transparent cassette-tape logo and a .ico with 16/32/48/256 sizes. Then write a 120-word design rationale for the portfolio.
```

### 7.5 GENEVIEVE prompts
**G1–G3 – Test plan**
```
Read PRD.md sections 2–3 and docs/05_Testing.md. Check that every Must requirement (FR1–FR5, FR7, FR9–FR13) and FR6/FR8 has at least one test case, and that there are negative tests for: missing file, corrupt line in Library.txt, empty library, unsupported format, one-item list, last item with Repeat Off. List any gaps and write the missing cases in the same table (ID | Feature | Steps | Expected result | Actual result | Pass / Fail).
```

**G4–G6 – Favorites in the form** *(replaces the placeholder)*
```
Replace UI/TapedeckForm.Favorites.cs ("public partial class TapedeckForm"). Keep the same member names as the placeholder.
1) public ToggleFavorite(): SelectedItem, or CurrentItem if nothing is selected; none -> SetStatus("Select an item to mark as favorite."); flip IsFavorite; RefreshListView(); SaveLibraryToFile(); if it is the playing item ShowNowPlaying(item); btnFavorite.Text "♥ Unfav" / "♥ Fav"; status. btnFavorite_Click calls it.  [commit]
2) public SetRating(int stars): SelectedItem only (none -> status); item.Rating = stars; refresh, save, update Now Playing if playing; status with item.RatingStars. cboRating_SelectedIndexChanged returns when _updatingRating is true or nothing is selected, else SetRating(cboRating.SelectedIndex).  [commit]
3) private static FavoriteText(MediaItem item): "♥" if favorite plus "N★" if rated.  [commit]
```

**G9 – User manual**
```
Using the running app, docs/06_User_Manual.md and UI/TapedeckForm.Menu.cs, check every keyboard shortcut in the manual matches the code, and that the troubleshooting table covers every message the app can show (search the code for ShowError and SetStatus). List mismatches and fix the manual.
```

### 7.6 Everyone – before the presentation
```
Explain <my file>.cs line by line in simple English. Then ask me 5 questions the instructor could ask about it (OOP concepts, exception handling, why this design) and check my answers. Do not change the code.
```
```
Build the solution. For each compiler error or warning, explain the cause and propose the smallest fix that follows the course rules in the Context block. Do not rename public members.
```

---

## 8. Schedule

| When | Joey | Heaven | Ammaar | Maddox | Genevieve |
|---|---|---|---|---|---|
| **Sun Nov 8 (tonight)** | J1–J8 | accept invite, clone, set up reference folder | same | same | same |
| **Mon Nov 9 AM** | J9–J14 → **"foundation is in main" by 12:00** | read the PRD and your prompts | same | same | G1 |
| **Mon Nov 9 PM** | review + merge PRs | H1–H5 | A1–A4 | M1–M4 | G2–G3 |
| **Tue Nov 10** ⏰ M1–M3 due | merge, build, submit | H6–H11 | A5–A10 | M5–M8 | G4–G6 |
| **Wed Nov 11** | J16–J17 | fixes | fixes | fixes | G7–G8 |
| **Thu Nov 12** ⏰ M4 due | J18 | H12–H13 | — | M9 | G9–G10 |
| **Fri Nov 13** | J19, demo recording | reflection | reflection | M10, reflection | G11, reflection |
| **Sat Nov 14** ⏰ M5 due | J20, tag v1.0, submit | — | — | — | G12 portfolio PDF |

After the foundation, **merge order does not matter**: every combination of real and placeholder files builds.

## 9. Definition of done (check before Nov 14)
- [ ] All FR1–FR13 work in the Release build on a second PC.
- [ ] `main` builds with 0 errors and no placeholder left (search the code for "PLACEHOLDER").
- [ ] Each member has ≥ 10 meaningful commits and at least one merged pull request.
- [ ] 5 team photos in `Assets/Team`, group number filled in everywhere, Canva link in credits.
- [ ] 24 test cases with actual results; bug log filled in.
- [ ] Portfolio PDF, release ZIP and repo link submitted as `G3_BSCS2A_*`.
- [ ] Every member can explain their own file and one teammate's file.
