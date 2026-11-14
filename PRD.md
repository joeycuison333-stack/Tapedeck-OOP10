# Tapedeck – Product Requirements Document (PRD) and Task Division

| | |
|---|---|
| **Product** | Tapedeck – Winamp-style desktop media player |
| **Course** | OOP10 – Object-Oriented Programming with C# and Windows Forms |
| **Instructor** | Prof. Ulysses Raymond F. Monsale |
| **Group / Section** | OOP Group 3 · BSCS 2-A |
| **Team** | **Joey Cuison** (leader) · **Heaven** · **Ammaar Azmy** · **Christian Maddox Rivera (Maddox)** · **Valencia Genevieve (Genevieve)** |
| **Repository** | github.com/joeycuison333-stack/Tapedeck-OOP10 (private) |
| **Final due date** | Saturday, November 14, 2026 |
| **Canva skin source** | https://canva.link/u1lv3kiz80z63am – page 1 is the skin. Export page 1 only, as PNG, **980 × 620 px**, to `Tapedeck/Assets/tapedeck_skin.png`. |
| **IDE (everyone)** | **Visual Studio 2026** + .NET desktop development workload + .NET 8.0 Runtime component. Keep `.sln` (do not convert to `.slnx`) and keep `net8.0-windows`. |

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
| NFR7 | Long-running work (reading file metadata, scanning a folder, starting the playback engine) does not freeze the UI thread. |

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
4. Write your file so it **replaces the placeholder** with the same file name. Compare with the same file in `Tapedeck-reference` and make sure you can explain every line.
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

## 7. Schedule

| When | Joey | Heaven | Ammaar | Maddox | Genevieve |
|---|---|---|---|---|---|
| **Sun Nov 8 (tonight)** | J1–J8 | accept invite, clone, set up reference folder | same | same | same |
| **Mon Nov 9 AM** | J9–J14 → **"foundation is in main" by 12:00** | read the PRD and your tasks | same | same | G1 |
| **Mon Nov 9 PM** | review + merge PRs | H1–H5 | A1–A4 | M1–M4 | G2–G3 |
| **Tue Nov 10** ⏰ M1–M3 due | merge, build, submit | H6–H11 | A5–A10 | M5–M8 | G4–G6 |
| **Wed Nov 11** | J16–J17 | fixes | fixes | fixes | G7–G8 |
| **Thu Nov 12** ⏰ M4 due | J18 | H12–H13 | — | M9 | G9–G10 |
| **Fri Nov 13** | J19, demo recording | reflection | reflection | M10, reflection | G11, reflection |
| **Sat Nov 14** ⏰ M5 due | J20, tag v1.0, submit | — | — | — | G12 portfolio PDF |

After the foundation, **merge order does not matter**: every combination of real and placeholder files builds.

## 8. Definition of done (check before Nov 14)
- [ ] All FR1–FR13 work in the Release build on a second PC.
- [ ] `main` builds with 0 errors and no placeholder left (search the code for "PLACEHOLDER").
- [ ] Each member has ≥ 10 meaningful commits and at least one merged pull request.
- [ ] 5 team photos in `Assets/Team`, group number filled in everywhere, Canva link in credits.
- [ ] 24 test cases with actual results; bug log filled in.
- [ ] Portfolio PDF, release ZIP and repo link submitted as `G3_BSCS2A_*`.
- [ ] Every member can explain their own file and one teammate's file.
