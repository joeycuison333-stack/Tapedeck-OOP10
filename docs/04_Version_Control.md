
# 5. Version Control and Collaboration Record

## 5.1 Repository
- **Host:** GitHub, private repository `Tapedeck-OOP10` under the team lead's account.
- **Repository link:** https://github.com/joeycuison333-stack/Tapedeck-OOP10
- **Collaborators (write access):** Sean Caling / Heaven (`SeanC1801`), Ammaar Azmy (`aksilol`), Christian Maddox Rivera (`Maddox44907`), Genevieve Valencia (`gdvalenciah`). Owner: Joey Cuison (`joeycuison333-stack`).
- **Instructor:** Prof. Ulysses Raymond F. Monsale – to be added under Settings → Collaborators.
- **Files:** `README.md`, `PRD.md`, Visual Studio `.gitignore` (`bin/`, `obj/`, `.vs/` and publish output are not committed), `Tapedeck.sln`, `Tapedeck/Tapedeck.csproj`, `docs/`.

## 5.2 Branching and merging
- `main` always compiles — verified after every one of Joey's foundation commits (`dotnet build`, 0 errors) and again after `dotnet publish -c Release`.
- One branch per feature (see PRD.md Section 6 for each member's branches).
- **No member depends on another:** Joey pushed a working foundation (`J1`–`J14`) with placeholder versions of every teammate's file, so the app already builds and runs. Each member replaces only their own files; pull requests merge in any order without conflicts.
- Merge through a **pull request**; Joey reviews and merges, building after each merge (`J15`).
- Commit prefixes: `feat:`, `fix:`, `docs:`, `test:`, `refactor:`, `build:`, `chore:`. At least **10 meaningful commits per member**.

## 5.3 Role matrix

| Member (GitHub) | Primary role | Main classes / files | Documentation section |
|---|---|---|---|
| **Joey Cuison** (leader)<br>`joeycuison333-stack` | Foundation, playback engine, UI and integration; repository owner, merges every PR | `IMediaControls.cs`, `RepeatMode.cs`, `TimeFormat.cs`, `MediaItem.cs`, `AudioItem.cs`, `VideoItem.cs`, `MediaFormats.cs`, `Theme.cs`, `BasePlayerForm.cs`, `TapedeckForm.cs` + `.Designer`, `.View`, `.AddMedia`, `.Playback`, `.Menu`, `Program.cs`, release build | 4 Development log, 5 Version control |
| **Sean Caling** (Heaven)<br>`SeanC1801` | Library saving, loading and deployment | `LibraryStore.cs`, `FavoritesStore.cs`, `TapedeckForm.Library.cs`, test data | 3 UML Part A (use case, sequence), 8 Deployment guide |
| **Ammaar Azmy**<br>`aksilol` | Sorting and searching | `LibrarySorter.cs`, `TapedeckForm.SortSearch.cs` | 2 Requirements, 3 UML Part B (class diagram, sorting) |
| **Christian Maddox Rivera** (Maddox)<br>`Maddox44907` | Skin design and splash screen | `TeamInfo.cs`, `SplashForm.cs`, `Assets/` (Canva) | Design concept, credits, slides |
| **Genevieve Valencia**<br>`gdvalenciah` | Favorites, testing and user manual | `TapedeckForm.Favorites.cs` | Cover, 6 Test plan and bug log, 7 User manual, 9 Presentation and reflections, portfolio assembly |

## 5.4 Merge conflict note
Conflicts were mostly avoided by design: the form is split into partial-class files with one owner each, and every member edits only their own files. Two conflicts still happened:

| # | Pull request | Files | How it was resolved |
|---|---|---|---|
| 1 | `feature/splash` into `main` (PR #13, fixed through `fix/splash-conflict`, PR #16) | Splash skin image (`Assets/` background) | Kept the newer version of the image, then rebuilt and checked the splash screen. |
| 2 | `test/test-plan` into `main` (M4, PRs #21 and #24) | `docs/05_Testing.md`, `docs/06_User_Manual.md` | Merged both versions: kept the executed test results, bug log and full user manual from the testing branch, and the M4 updates from `main`. |

**Commit messages:** most commits use the `feat:`, `fix:`, `docs:`, `test:`, `refactor:` prefixes. A few early commits (mostly in Heaven's `feature/library-store` and `docs/uml-deployment` branches, and Ammaar's A8–A10 commits) were made without a prefix; they were left as they are rather than rewriting shared history.

## 5.5 Meeting log

| Date | Attendees | Decisions |
|---|---|---|
| Nov 8 | All five | Chose app name and theme (Tapedeck, retro late-90s desktop look). Enhancement: favorites + ratings. Sorting: insertion sort. Roles and workload as in 5.3 and PRD.md. Joey built and pushed the foundation (`J1`–`J14`) same night, ahead of the Nov 9 noon target. |
| Nov 9 | | _(fill in — foundation announcement, start of H1–H5 / A1–A4 / M1–M4 / G2–G3)_ |
| Nov 10 | | M1–M3 check-in with instructor. |
| Nov 12 | | M4 check-in; release tested on another computer. |
| Nov 13 | | Demo rehearsal (10 minutes, every member speaks). |

## 5.6 Commit history screenshot
_(Insert a screenshot of GitHub → Insights → Contributors and the commit list here once teammates' commits are in.)_ Joey's foundation currently stands at **14 commits** on `main` (`5199d3d` through `78f0217`), already past the 10-commit minimum per member.
