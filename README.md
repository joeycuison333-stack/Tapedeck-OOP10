# Tapedeck – OOP10 Media Player Project

A Winamp-style desktop media player in **C# Windows Forms (.NET 8)** with **LibVLCSharp**. It gathers mp3 and mp4 files into a library saved in `Library.txt`, plays them through an original skin, and adds **favorites and star ratings** as our enhancement.

**Course:** OOP10 · **Instructor:** Prof. Ulysses Raymond F. Monsale · **Group:** Group 3 · BSCS 2A

> 🆕 **New to Git/GitHub?** Start with **[GETTING_STARTED.md](GETTING_STARTED.md)** — one-time setup, your branch, and the exact commands to push each of your tasks.

## 👥 Team and responsibilities

| Member | Role | Owns (files) | Requirements |
|---|---|---|---|
| **Joey Cuison** (leader) | Foundation, playback engine, UI and integration | `Core/IMediaControls.cs`, `MediaItem.cs`, `AudioItem.cs`, `VideoItem.cs`, `MediaFormats.cs`, `TimeFormat.cs`, `RepeatMode.cs`, `UI/BasePlayerForm.cs`, `Theme.cs`, `TapedeckForm.cs` + `.Designer`, `.Playback`, `.View`, `.AddMedia`, `.Menu`, `Program.cs`, release build | FR1, FR2, FR4, FR5, FR10, FR11, FR13 |
| **Heaven** | Library saving, loading and deployment | `Data/LibraryStore.cs`, `Data/FavoritesStore.cs`, `UI/TapedeckForm.Library.cs`, use case + sequence diagrams, deployment guide | FR3, FR7, FR8 (file) |
| **Ammaar Azmy** | Sorting and searching | `Core/LibrarySorter.cs`, `UI/TapedeckForm.SortSearch.cs`, requirements, class diagram | FR6, FR12 |
| **Christian Maddox Rivera** (Maddox) | Skin design and splash screen | `UI/TeamInfo.cs`, `UI/SplashForm.cs`, `Assets/` (Canva), credits, slides | FR9, skin |
| **Valencia Genevieve** (Genevieve) | Favorites, testing and user manual | `UI/TapedeckForm.Favorites.cs`, test plan, bug log, user manual, portfolio | FR8 (UI), testing |

Each member's step-by-step tasks, commit messages and **Antigravity prompts** are in **[PRD.md](PRD.md)** (section 6 and 7).

## ✅ Features

| Requirement | Feature |
|---|---|
| FR1–FR2 | Add files or a whole folder; title, artist, duration, album or resolution read automatically |
| FR3, FR7 | Library saved to `Library.txt` and reloaded at start; damaged lines, missing files, unsupported formats and an empty library are handled |
| FR4–FR5 | Play, Pause, Stop, Next, Previous, volume, seek; playlist and Now Playing |
| FR6, FR12 | Hand-written **insertion sort** (title, artist, album/resolution, duration, rating) and **binary search** |
| FR8 | **Favorites and 1–5 star ratings**, saved in `Favorites.txt` |
| FR9 | Splash screen with logo, group and all five members |
| FR10 | Menu strip with 23 keyboard shortcuts |
| FR11 | Elapsed / total time, updated every 500 ms |
| FR13 | Shuffle and Repeat Off / All / One |

## ▶️ Run it
1. Open `Tapedeck.sln` in **Visual Studio 2026** with the **.NET desktop development** workload and the **.NET 8.0 Runtime** individual component. Everyone in the group uses VS 2026. If VS offers to convert the solution to `.slnx`, choose **No**, and keep the target framework at `net8.0-windows`.
2. NuGet restores `LibVLCSharp.WinForms` and `VideoLAN.LibVLC.Windows` on the first build.
3. Press **F5**.

## 🔀 How we work (Git)
1. Clone: `git clone https://github.com/joeycuison333-stack/Tapedeck-OOP10.git`
2. Keep the project ZIP extracted in a **separate** `Tapedeck-reference` folder. Never commit from it.
3. For each task: `git checkout main` → `git pull` → `git checkout -b feature/<name>`
4. Wait for Joey's "foundation is in main" message (Mon Nov 9, noon). Replace **your own placeholder file** using the Antigravity prompt in PRD.md, make sure **Build** succeeds, then:
   ```
   git add <your files>
   git commit -m "feat: <message from PRD.md>"
   git push -u origin feature/<name>
   ```
5. Open a **pull request** to `main`; another member reviews and merges.
6. Commit prefixes: `feat:` `fix:` `docs:` `test:` `refactor:` `build:` `chore:` – at least **10 commits per member**.
7. Only edit **your own files** (PRD.md section 4). Nobody waits for anybody: every combination of finished and placeholder files builds.

## 📁 Project structure
```
Tapedeck/
  Core/        IMediaControls, MediaItem, AudioItem, VideoItem, MediaFormats, LibrarySorter, RepeatMode, TimeFormat
  Data/        LibraryStore, FavoritesStore
  UI/          BasePlayerForm, TapedeckForm (+ Playback, View, AddMedia, Menu, Library, SortSearch, Favorites parts), SplashForm, Theme, TeamInfo
  Assets/      skin, logo.png, logo.ico, button images, Team/ photos
docs/          requirements, UML (02a, 02b), development log, version control, tests, user manual, deployment, presentation, credits
integration/   placeholder files Joey pushes first so main always builds (not part of the final app)
PRD.md         requirements and task division with prompts
```

## 📅 Milestones
| Milestone | Due | Deliverables |
|---|---|---|
| M1 Analysis and design | Nov 10 | Requirements, use case and class diagrams, design concept, repo with all members |
| M2 Core logic | Nov 10 | MediaItem hierarchy, LibraryStore, add and save/load, first test cases |
| M3 Player and skin | Nov 10 | Form, controls, skin, splash, menu, elapsed time, sorting, shuffle/repeat, enhancement |
| M4 Testing, docs, deployment | Nov 12 | Test report, bug log, user manual, deployment guide, release build |
| M5 Final submission | Nov 14 | Demo video, documentation portfolio, final repository |

## 📜 Credits
LibVLCSharp and libVLC by VideoLAN (LGPL-2.1). Starter code from the OOP10 project guide. Skin, logo and icons are original to the group. Details in `docs/09_Credits.md`.
