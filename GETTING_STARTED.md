# Getting Started with Git & GitHub

**This file is for Heaven, Ammaar, Maddox and Genevieve.** You don't need to know Git already — follow the steps in order, copy-paste the commands exactly, and ask Joey if anything doesn't match what's written here.

## Five words you'll see everywhere
- **Repo** — the project folder, tracked by Git, living on GitHub.
- **Clone** — your own copy of the repo, downloaded to your laptop.
- **Branch** — a separate "lane" to do your work in, so you never touch someone else's files. Yours already exist (see Part 2).
- **Commit** — a saved checkpoint of your changes, with a short message describing what you did.
- **Push** — sending your commits from your laptop up to GitHub, so everyone (and Joey) can see them.
- **Pull Request (PR)** — asking Joey to merge your branch into `main`, the official version everyone builds from.

---

## Part 1 — One-time setup (do this first, tonight)

1. **Accept the GitHub invite** — check your email for an invite to `Tapedeck-OOP10`, click **Accept invitation**.
2. **Install Git** if you don't have it: https://git-scm.com/downloads. Check it worked by opening a terminal (PowerShell or VS's Developer PowerShell) and typing `git --version`.
3. **Install Visual Studio 2026** with the **.NET desktop development** workload and the **.NET 8.0 Runtime (LTS)** individual component (same as everyone — see PRD.md Part 0).
4. **Clone the repo** — open a terminal where you want the project (e.g. `source\repos`) and run:
   ```
   git clone https://github.com/joeycuison333-stack/Tapedeck-OOP10.git
   cd Tapedeck-OOP10
   ```
5. **Tell Git who you are.** Use your own name and the email on *your own* GitHub account — this is how your commits get counted as yours for grading:
   ```
   git config --global user.name "Your Full Name"
   git config --global user.email "the-email-on-your-GitHub-account"
   ```
6. **Get the reference project** (a finished version you compare against — never commit from it). Download `Tapedeck-reference.zip`, then in PowerShell:
   ```
   Expand-Archive "$env:USERPROFILE\Downloads\Tapedeck-reference.zip" "$env:USERPROFILE\Documents" -Force
   ```
7. **Wait for Joey to post "foundation is in main"** before doing anything below — the app needs to build first.

---

## Part 2 — Your branches already exist

Joey already created your branches on GitHub so you don't type `git checkout -b` (which *creates* a new branch — you don't need to, yours is already there). You only need to **switch** to it:

```
git fetch
git checkout <your-branch-name>
```

Your exact branch names are in **Part 5** below, under your name.

---

## Part 3 — The cycle you'll repeat for every task

Every task in Part 5 follows the exact same five steps:

1. **Check you're on the right branch:**
   ```
   git status
   ```
   The first line must say `On branch <your-branch-name>`. If it says `main`, run `git checkout <your-branch-name>` first.

2. **Write the code.** Open the repo in Antigravity (Agent mode). Paste the **Context block** from `PRD.md` section 7.0 first (only once per new conversation), then paste the prompt for your task from your section of `PRD.md` section 7 (7.2 Heaven, 7.3 Ammaar, 7.4 Maddox, 7.5 Genevieve). Compare the result with the same file in `Tapedeck-reference`.

3. **Build it.** In Visual Studio: **Ctrl+Shift+B**. It must say `Build succeeded` with 0 errors before you continue.

4. **Save and push your work** — always these three commands, in this order:
   ```
   git add <the file(s) for this task — see Part 5>
   git commit -m "<the exact message from Part 5>"
   git push
   ```
   The **very first** push on a branch, Git will tell you to use this instead (one-time, per branch):
   ```
   git push -u origin <your-branch-name>
   ```
   After that one time, plain `git push` works for the rest of that branch's tasks.

5. **Repeat** for your next task on the same branch. When every task on a branch is done, open a Pull Request — Part 4.

---

## Part 4 — Opening a Pull Request (first time walkthrough)

1. Go to https://github.com/joeycuison333-stack/Tapedeck-OOP10 in your browser.
2. GitHub usually shows a yellow banner: *"`<your-branch>` had recent pushes"* → click **Compare & pull request**.
   - If you don't see the banner: click the branch dropdown (says `main`), pick your branch, then click **Contribute → Open pull request**.
3. Base branch: `main` ← Compare branch: yours. Add a short title (e.g. "Library saving and loading"). Click **Create pull request**.
4. **Message Joey** — he reviews it and merges it into `main`.
5. After it's merged, before starting your *next* branch, update your local `main`:
   ```
   git checkout main
   git pull
   ```

---

## Part 5 — Your parts

For every row: `git add` only the listed file(s), then `git commit -m` with the message **exactly as written** (commit prefixes `feat:`/`fix:`/`docs:`/`test:` matter for grading).

### 💾 Heaven
**Your files:** `Data/LibraryStore.cs`, `Data/FavoritesStore.cs`, `UI/TapedeckForm.Library.cs`, `docs/02a_UML_Use_Case_and_Sequence.md`, `docs/07_Deployment_Guide.md`, `docs/diagrams/`, `docs/test-data/`

**Branch `feature/library-store`** (Antigravity prompts: PRD.md 7.2 "H1–H3" and "H4–H5")
| Task | `git add` | `git commit -m` | Due |
|---|---|---|---|
| H1 Save Library.txt | `Data/LibraryStore.cs` | `feat: save library to Library.txt` | Nov 9 |
| H2 Load Library.txt | `Data/LibraryStore.cs` | `feat: load Library.txt and count damaged lines` | Nov 9 |
| H3 ParseLine | `Data/LibraryStore.cs` | `feat: parse Library.txt lines` | Nov 9 |
| H4 Save Favorites.txt | `Data/FavoritesStore.cs` | `feat: save favorites and ratings` | Nov 9 |
| H5 Apply Favorites.txt | `Data/FavoritesStore.cs` | `feat: load favorites and ratings` | Nov 9 |

→ Push, then open a Pull Request for `feature/library-store` (Part 4).

**Branch `feature/library-form`** (Antigravity prompt: PRD.md 7.2 "H6–H8")
| Task | `git add` | `git commit -m` | Due |
|---|---|---|---|
| H6 Load/save in the form | `UI/TapedeckForm.Library.cs` | `feat: load and save the library in the form` | Nov 10 |
| H7 Remove items | `UI/TapedeckForm.Library.cs` | `feat: remove items from the library` | Nov 10 |
| H8 Reload / open in Notepad | `UI/TapedeckForm.Library.cs` | `feat: reload library and open it in Notepad` | Nov 10 |
| H9 Corrupt test data | `docs/test-data/Library_corrupt.txt` | `test: add corrupt Library.txt test data` | Nov 10 |

→ Push, then open a Pull Request for `feature/library-form`.

**Branch `docs/uml-deployment`** (Antigravity prompts: PRD.md 7.2 "H10–H11" and "H12–H13")
| Task | `git add` | `git commit -m` | Due |
|---|---|---|---|
| H10 Use case diagram | `docs/02a_UML_Use_Case_and_Sequence.md` `docs/diagrams/use_case_diagram.png` | `docs: use case diagram and descriptions` | Nov 10 |
| H11 Sequence diagrams | `docs/02a_UML_Use_Case_and_Sequence.md` `docs/diagrams/sequence_*` | `docs: sequence diagrams` | Nov 10 |
| H12 Deployment guide | `docs/07_Deployment_Guide.md` | `docs: deployment guide` | Nov 12 |
| H13 Release test record | `docs/07_Deployment_Guide.md` | `test: record release test on a second computer` | Nov 12 |

→ Push, then open a Pull Request for `docs/uml-deployment`.

---

### 🧩 Ammaar Azmy
**Your files:** `Core/LibrarySorter.cs`, `UI/TapedeckForm.SortSearch.cs`, `docs/01_Requirements.md`, `docs/02b_UML_Class_and_Sorting.md`, `docs/diagrams/class_diagram.*`

**Branch `feature/sorter`** (Antigravity prompt: PRD.md 7.3 "A1–A4")
| Task | `git add` | `git commit -m` | Due |
|---|---|---|---|
| A1 Compare by sort key | `Core/LibrarySorter.cs` | `feat: compare media items by sort key` | Nov 9 |
| A2 Insertion sort | `Core/LibrarySorter.cs` | `feat: add hand-written insertion sort` | Nov 9 |
| A3 Binary search | `Core/LibrarySorter.cs` | `feat: add binary search by title` | Nov 9 |
| A4 Partial-match search | `Core/LibrarySorter.cs` | `feat: add partial-match search` | Nov 9 |

→ Push, then open a Pull Request for `feature/sorter`.

**Branch `feature/sort-search-form`** (Antigravity prompt: PRD.md 7.3 "A5–A7")
| Task | `git add` | `git commit -m` | Due |
|---|---|---|---|
| A5 SortLibrary | `UI/TapedeckForm.SortSearch.cs` | `feat: sort the playlist and keep the current item` | Nov 10 |
| A6 Sort by column header | `UI/TapedeckForm.SortSearch.cs` | `feat: sort by clicking column headers` | Nov 10 |
| A7 Search box | `UI/TapedeckForm.SortSearch.cs` | `feat: search the library` | Nov 10 |

→ Push, then open a Pull Request for `feature/sort-search-form`.

**Branch `docs/requirements-class`** (Antigravity prompt: PRD.md 7.3 "A8–A10")
| Task | `git add` | `git commit -m` | Due |
|---|---|---|---|
| A8 Requirements spec | `docs/01_Requirements.md` | `docs: requirements specification` | Nov 10 |
| A9 Class diagram | `docs/02b_UML_Class_and_Sorting.md` `docs/diagrams/class_diagram.*` | `docs: class diagram` | Nov 10 |
| A10 Sorting explanation | `docs/02b_UML_Class_and_Sorting.md` | `docs: sorting algorithm explanation` | Nov 10 |

→ Push, then open a Pull Request for `docs/requirements-class`.

---

### 🎨 Christian Maddox Rivera (Maddox)
**Your files:** `UI/TeamInfo.cs`, `UI/SplashForm.cs`, `Assets/` (all images), `docs/design/`, `docs/09_Credits.md`

**Branch `feature/splash`** (Antigravity prompts: PRD.md 7.4 "M1", "M3–M5")
| Task | `git add` | `git commit -m` | Due |
|---|---|---|---|
| M1 Team info | `UI/TeamInfo.cs` | `feat: add team info for all members` | Nov 9 |
| M2 Member photos | `Assets/Team/*.jpg` | `feat: add member photos` | Nov 9 |
| M3 Splash layout | `UI/SplashForm.cs` | `feat: add splash screen layout` | Nov 9 |
| M4 Member cards | `UI/SplashForm.cs` | `feat: show team cards on splash screen` | Nov 9 |
| M5 Timer + skip | `UI/SplashForm.cs` | `feat: close splash after 3 seconds or on click` | Nov 10 |

→ Push, then open a Pull Request for `feature/splash`.

**Branch `design/canva-skin`** (Antigravity prompt: PRD.md 7.4 "M6–M8")
| Task | `git add` | `git commit -m` | Due |
|---|---|---|---|
| M6 Canva skin background | `Assets/tapedeck_skin.png` | `feat: add Canva skin background` | Nov 10 |
| M7 Logo + icon | `Assets/logo.png` `Assets/logo.ico` | `feat: add Canva logo and app icon` | Nov 10 |

→ Push, then open a Pull Request for `design/canva-skin`.

**Branch `docs/design`** (Antigravity prompt: PRD.md 7.4 "M6–M8", plus M9–M10)
| Task | `git add` | `git commit -m` | Due |
|---|---|---|---|
| M8 Design concept doc | `docs/design/` | `docs: design concept and Canva source` | Nov 10 |
| M9 Credits | `docs/09_Credits.md` | `docs: credits and licenses` | Nov 12 |
| M10 Slides | `docs/slides.pdf` | `docs: presentation slides` | Nov 13 |

→ Push, then open a Pull Request for `docs/design`.

---

### ⭐ Valencia Genevieve (Genevieve)
**Your files:** `UI/TapedeckForm.Favorites.cs`, `docs/00_Cover.md`, `docs/05_Testing.md`, `docs/06_User_Manual.md`, `docs/08_Presentation_and_Reflections.md`, `docs/10_Appendix_Source.md`, `docs/screenshots/`

**Branch `test/test-plan`** (Antigravity prompt: PRD.md 7.5 "G1–G3")
| Task | `git add` | `git commit -m` | Due |
|---|---|---|---|
| G1 Test plan | `docs/05_Testing.md` | `docs: add test plan` | Nov 9 |
| G2 Test cases TC01–12 | `docs/05_Testing.md` | `test: add test cases for adding, files and playback` | Nov 9 |
| G3 Test cases TC13–24 | `docs/05_Testing.md` | `test: add test cases for sorting, shuffle, repeat and favorites` | Nov 9 |
| G7 Record test results | `docs/05_Testing.md` | `test: record test results` | Nov 11 |
| G8 Bug log | `docs/05_Testing.md` | `docs: bug log` | Nov 11 |

→ Push, then open a Pull Request for `test/test-plan`. (G7–G8 can be a second PR from the same branch if you've already merged once — ask Joey if unsure.)

**Branch `feature/favorites`** (Antigravity prompt: PRD.md 7.5 "G4–G6")
| Task | `git add` | `git commit -m` | Due |
|---|---|---|---|
| G4 Toggle favorite | `UI/TapedeckForm.Favorites.cs` | `feat: toggle favorites` | Nov 10 |
| G5 Star rating | `UI/TapedeckForm.Favorites.cs` | `feat: add star ratings` | Nov 10 |
| G6 ♥★ column text | `UI/TapedeckForm.Favorites.cs` | `feat: show favorites and ratings in the playlist` | Nov 10 |

→ Push, then open a Pull Request for `feature/favorites`.

**Branch `docs/manual-portfolio`** (Antigravity prompt: PRD.md 7.5 "G9")
| Task | `git add` | `git commit -m` | Due |
|---|---|---|---|
| G9 User manual | `docs/06_User_Manual.md` | `docs: user manual` | Nov 12 |
| G10 Screenshots | `docs/screenshots/` `docs/06_User_Manual.md` | `docs: user manual screenshots` | Nov 12 |
| G11 Presentation plan | `docs/08_Presentation_and_Reflections.md` | `docs: presentation plan and reflections` | Nov 13 |
| G12 Cover + portfolio | `docs/00_Cover.md` `docs/10_Appendix_Source.md` | `docs: final documentation portfolio` | Nov 14 |

→ Push, then open a Pull Request for `docs/manual-portfolio`.

---

## Part 6 — If something goes wrong

| Message / problem | What it means | Fix |
|---|---|---|
| `fatal: not a git repository` | You're not inside the `Tapedeck-OOP10` folder. | `cd` into the folder you cloned (Part 1, step 4) and try again. |
| `Please tell me who you are` | You skipped the identity step. | Run the two `git config --global` commands from Part 1, step 5. |
| GitHub asks you to sign in | Normal the first time you push. | Sign in with **your own** GitHub account (the one Joey invited) — not Joey's. |
| Visual Studio asks to convert the solution to `.slnx` | VS 2026 offers a newer format by default. | Click **No**. Always keep `.sln`. |
| `error: failed to push ... (non-fast-forward)` | Someone else's commit is on your branch upstream, or you edited a file you don't own. | Run `git pull`, then push again. If it keeps happening, message Joey — don't force-push. |
| Build fails after `git pull` | A file you don't own changed (shouldn't happen — everyone owns separate files) or a NuGet package needs restoring. | Rebuild once more (Ctrl+Shift+B); if still failing, message Joey with the exact error. |
| `git push` says "The current branch has no upstream branch" | Normal the very first time you push a branch. | Use the `git push -u origin <branch-name>` form shown once in Part 3, step 4. |

**Golden rule:** only ever run `git add` on the files listed under your name in Part 5. If `git status` shows a file you don't recognize, stop and ask Joey before committing.
