# 6. Testing Documentation

**Project:** Tapedeck (OOP10 media player) · **OOP Group 3 – BSCS 2-A**
**Test cases and test plan:** Valencia Genevieve · **Release build, display scaling and second-computer checks:** Joey Cuison
**Updated:** November 12, 2026 (Milestone 4)

---

## 6.1 Test Plan

### 6.1.1 Objectives, Scope and Strategy

The test plan verifies that Tapedeck meets the functional requirements FR1–FR13 in the requirements specification (`01_Requirements.md`), handles error conditions without crashing, and runs from the self-contained Release build on a computer without Visual Studio. Testing is manual and follows the Appendix B.1 test case template of the project guide.

- **Functional testing:** adding files and folders, item information, saving and reloading the library, playback controls, volume and seek, playlist and Now Playing, sorting, searching, shuffle, repeat, elapsed time, and the favorites and star ratings enhancement.
- **Error and exception testing:** missing file, corrupt lines in `Library.txt`, empty library, unsupported format, a file locked by another program, and a folder renamed while the program was closed.
- **Interface testing:** splash screen, menu strip and keyboard shortcuts, and skin readability at 100%, 125% and 200% display scaling.

### 6.1.2 Test Environment

**Table 1. Test environment**

| Item | Details |
|---|---|
| **Development test machine** | Huawei MateBook 14, Windows 11 Home Single Language (25H2, Build 26200.9711), 64-bit, Intel Core Ultra 5 125H (3.60 GHz), 16.0 GB RAM, Visual Studio 2026; tested at 100% and 200% display scaling |
| **Build tested** | Debug build from Visual Studio and the self-contained Release build (win-x64) |
| **Second computer (no Visual Studio)** | A classmate's laptop (BSCS 2-A), Windows 11 Pro, 64-bit, no Visual Studio installed (Section 6.2.2 and `07_Deployment_Guide.md` Section 8.5) |
| **Playback engine** | LibVLCSharp.WinForms 3.9.0, VideoLAN.LibVLC.Windows 3.0.21 |
| **Test media** | MP3 files with and without ID3 tags; MP4 files at 1280×720 and 640×480; unsupported files (`.wav`, `.txt`, `.jpg`); `docs/test-data/Library_corrupt.txt` |
| **Tester(s)** | Valencia Genevieve (test cases), Joey Cuison (Release build, display scaling and second-computer checks) |
| **Test period** | November 9–12, 2026 |

### 6.1.3 Test Cases

The test cases below follow the Appendix B.1 format. The Actual result and Pass / Fail columns were filled in after each test was executed. Negative tests are marked **[N]**.

**Table 2. Test cases**

| ID | Feature | Steps | Expected result | Actual result | Pass / Fail |
|---|---|---|---|---|---|
| TC01 | Add media files | Ctrl+O, select one .mp3 and one .mp4 | Both items added; status "Added 2 item(s)." | Both files added; status showed "Added 2 item(s)." | Pass |
| TC02 | Add folder | Ctrl+Shift+O, choose a folder with media in subfolders | All .mp3 and .mp4 files in the folder and subfolders are added | Files from the folder and its subfolder were added; no errors. | Pass |
| TC03 | Duplicate file | Add the same file twice | Not added again; "1 already in the library." | Status showed "Added 0 item(s). 1 already in the library." | Pass |
| TC04 | Unsupported format **[N]** | Add .wav, .txt, .jpg files | Files skipped with a warning; no crash | Warning "Added 0 item(s). 1 skipped: only .mp3 and .mp4 are supported." Playlist unchanged. | Pass |
| TC05 | Item information | Add a tagged .mp3 and an .mp4; check the columns | Audio shows album; video shows resolution; missing tags show "Unknown" | Tags shown for the mp3; resolution (1280x720) shown for the mp4. | Pass |
| TC06 | Save and reload | Add items, close, reopen | `Library.txt` in 6-field format; same items reload | `Library.txt` correct; status "Loaded 6 item(s) from Library.txt." | Pass |
| TC07 | Edit in Notepad and reload | Ctrl+L, edit a title, save; Ctrl+Shift+L | Edited title shown without restarting | Edited title appeared after reload. | Pass |
| TC08 | Play, Pause, Stop | F5, F6, F5, F7 | Pause keeps the position; Stop resets elapsed time to 00:00:00 | All three worked; Stop returned to 00:00:00. | Pass |
| TC09 | Next and Previous | Ctrl+Right past the last item; Ctrl+Left on the first item | Wraps from last to first and first to last | Both wrapped as expected. | Pass |
| TC10 | Volume | Ctrl+Up and Ctrl+Down repeatedly | Changes in steps of 10; stops at 0 and 100 | Volume changed smoothly and stopped at 0 and 100. | Pass |
| TC11 | Seek | Drag the seek bar; arrow keys on the bar | Playback jumps to the chosen position | Position changed immediately with mouse and keys (at 100% scaling). | Pass |
| TC12 | Playlist and Now Playing | Double-click item 2 | Item plays; Now Playing filled in; row highlighted in teal; window title "Title - Artist - Tapedeck" | Item played; row highlighted in teal (`06_User_Manual.md` Figure 4); Now Playing showed title, artist, type and resolution. Window title not yet checked. | To be confirmed |
| TC13 | Empty library **[N]** | No items; press F5, Next, Previous, Stop | "The library is empty. Add some files first."; no exception | Message shown; no crash. | Pass |
| TC14 | Corrupt `Library.txt` **[N]** | Replace `Library.txt` with `Library_corrupt.txt`, start | Damaged lines skipped and counted; valid lines load | App opened; damaged lines skipped and reported; valid items loaded. | Pass |
| TC15 | Missing file **[N]** | Add a file, rename it in Explorer, play it | Row greyed out; "This file was moved or deleted"; no crash | Row greyed out; status and warning shown (`06_User_Manual.md` Figure 6); no crash. | Pass |
| TC16 | Search | Search an exact title, part of a title, part of an artist | Found by binary search or partial-match search; row selected | All three searches selected the correct row. | Pass |
| TC17 | No match / blank search **[N]** | Search with an empty box, then a title not in the list | Status message; selection unchanged | "Type a title or artist…" and "No match for …" shown. | Pass |
| TC18 | Sort while playing | Play item 3, then Ctrl+1 to Ctrl+5 | List re-sorted with insertion sort; playing item stays selected and playing | Each sort was correct; playback not interrupted. | Pass |
| TC19 | Toggle favorite | Select an item, Ctrl+D twice | ♥ added then removed; button text changes | Both states displayed correctly. | Pass |
| TC20 | Star rating saved | Give 4 stars, close and reopen | "4★" shown and restored from `Favorites.txt` | Rating shown, saved and restored. | Pass |
| TC21 | Splash screen | Start and wait; start again and press a key | Logo, title, group, section and five member cards; closes after ~3 s or on a key | All shown (`06_User_Manual.md` Figure 1); closed after about 3 s and on a key press. | Pass |
| TC22 | One-item list **[N]** | Library of 1: Next, Previous, Shuffle + Next, Sort | Same item replays; no hang; sort does nothing | All behaved as expected. | Pass |
| TC23 | Repeat Off, last item **[N]** | Let the last item finish | Playback stops | Playback stopped; did not wrap to item 1. | Pass |
| TC24 | Repeat One, Repeat All, Shuffle | Let an item finish in each mode | One replays; All wraps to item 1; Shuffle picks another item | All three modes worked. | Pass |
| TC25 | Locked file **[N]** | Add a file locked by another program | "1 could not be read."; no crash | Message shown; playlist unchanged. | Pass |
| TC26 | Folder renamed while closed **[N]** | Add a file, close, rename its folder, reopen, play | Row greyed out; moved/deleted warning; no crash | Behaved as expected. | Pass |
| TC27 | Sort by rating | Give ratings 5 and 3, Ctrl+5 | 5★ first, then 3★, then unrated | Order correct (`06_User_Manual.md` Figure 4); playback continued. | Pass |
| TC28 | Shuffle with Repeat All | Four items, shuffle on, Repeat All | Plays in random order and keeps going; no item twice in a row | Played in random order and kept looping. | Pass |

### 6.1.4 Requirements Traceability

**Table 3. Traceability of test cases to requirements**

| Requirement | Test cases | Note |
|---|---|---|
| FR1, FR2 | TC01–TC05, TC25 | File dialog, folder, duplicates, formats and item information. |
| FR3 | TC06, TC07, TC14 | Library persistence and recovery from damaged lines. |
| FR4, FR10 | TC08–TC11, TC13 | Buttons, menu items and keyboard shortcuts. |
| FR5 | TC12 | Playlist selection and Now Playing area. |
| FR6, FR12 | TC16–TC18, TC27 | Hand-written insertion sort, binary search and partial-match search. |
| FR7 | TC04, TC13, TC14, TC15, TC25, TC26 | Negative tests required by the guide. |
| FR8 | TC19, TC20, TC27 | Favorites and 1–5 star ratings enhancement. |
| FR9 | TC21 | Splash screen and its automatic and manual closing. |
| FR11 | TC08, TC11 | Elapsed time display and seek bar. |
| FR13 | TC22, TC23, TC24, TC28 | Shuffle on/off, each repeat mode, last item, one-item list. |

---

## 6.2 Test Results

### 6.2.1 Results Summary

**Table 4. Summary of test results**

| Measure | Count | Remarks |
|---|---|---|
| **Test cases planned** | 28 | Including 9 negative test cases. |
| **Executed** | 28 | All planned test cases were run. |
| **Passed** | 27 | All other executed tests met the expected results. |
| **Failed** | 0 | — |
| **To be confirmed** | 1 | TC12: the window title in the taskbar still has to be checked. |

One defect, B06, was found outside the test cases while capturing the user-manual screenshots: at 200% display scaling the elapsed time and the seek bar are covered. At 100% they display correctly, which is why TC08 and TC11 passed. B06 is recorded in the bug log (Section 6.3).

### 6.2.2 Release Build Test on a Second Computer

Per guide Appendix E.2, the Release build was tested by Joey Cuison on a computer without Visual Studio: a classmate's laptop (Windows 11 Pro, 64-bit). The details of the installation are in `07_Deployment_Guide.md` Section 8.5.

**Table 5. Release build test checklist**

| Check | Result | Date | Remarks |
|---|---|---|---|
| Application starts from the extracted ZIP in a writable folder | Pass | Nov 12, 2026 | Extracted from `G3_BSCS2A_Release.zip` and started without installing anything else. |
| Splash screen and team photos display | Pass | Nov 12, 2026 | Logo, title, group, section and all five member cards shown; closed after about 3 seconds. |
| Add files; play, pause, stop; elapsed time | Pass | Nov 12, 2026 | Files added; playback controls worked; elapsed time counted up and reset on Stop. |
| Keyboard shortcuts | Pass | Nov 12, 2026 | F5, F6, F7, Ctrl+Right, Ctrl+Left and the menu shortcuts worked like the buttons. |
| Sorting, shuffle and repeat | Pass | Nov 12, 2026 | Insertion sort, search, shuffle and repeat modes (Off / All / One) worked. |
| Close and reopen: library reloads | Pass | Nov 12, 2026 | `Library.txt` was created next to `Tapedeck.exe` and the items reloaded. |
| One error case (renamed media file) | Pass | Nov 12, 2026 | The renamed file was greyed out and playing it showed the moved/deleted message without crashing. |

---

## 6.3 Bug Log

Each defect found during development and testing is recorded with how it was fixed and the retest result (guide Appendix B.2). Only defects that actually occurred are entered.

**Table 6. Bug log**

| Bug ID | Test | Description | Found by / date | Fix | Retest result | Status |
|---|---|---|---|---|---|---|
| B01 | TC07, TC10 | At 125% scaling the menu strip did not respond to clicks; the taller title bar covered it. | Joey Cuison, M3 | `PlaceMenu()` places the menu under the scaled title bar and brings it to the front. | Pass at 100% and 125% | Closed |
| B02 | TC05 | Playlist column headings were cut off because column widths did not scale. | Joey Cuison, M3 | `FitColumns()` divides the list width between the columns. | Pass | Closed |
| B03 | TC21 | Title bar and splash showed "Group 2" instead of OOP Group 3, BSCS 2-A. | Joey Cuison, M3 | Group name corrected in `TeamInfo`, the project file and the skin. | Pass | Closed |
| B04 | TC13 | At 200% scaling the status bar text slipped below its box and was cut off. | Joey Cuison, M3 | Status labels placed from the skin's coordinates after scaling. | Pass at 200% | Closed |
| B05 | TC01 | At 200% scaling the Add button was hidden by an overlapping control. | Joey Cuison, M3 | Library buttons brought to the front. | Pass at 200% | Closed |
| TD-01 | TC21 | On a cold start a blank window showed for 1.5–2 s after the splash, because the playback engine loaded only after the splash closed. | Joey Cuison, Nov 12, 2026 | The engine now loads on a background thread while the splash is showing (`PrepareEngineAsync`). | Pass (Debug and Release) | Closed |
| B06 | TC08, TC11 | At 200% scaling the elapsed time and the seek bar are not visible. The media information line is scaled twice (360×20 becomes 1242×75) and, being transparent, paints over both controls. | Joey Cuison, Nov 12, 2026 | The Now Playing labels, elapsed time and seek bar are sized from the skin's coordinates after scaling, and the elapsed time and seek bar are brought to the front (`PlaceSkinControls()`). | *Retest at 200% pending* | Open |
