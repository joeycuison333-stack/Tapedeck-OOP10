# 8. Deployment Guide

**Project:** Tapedeck (OOP10 media player) · **OOP Group 3 – BSCS 2-A** · **Author:** Heaven

This guide explains how to turn the source code into a program that runs on any 64-bit Windows 10/11 computer **without Visual Studio or .NET installed** (NFR3), how to package it as `G3_BSCS2A_Release.zip`, and how to test it on a second computer.

---

## 8.1 System requirements

| | To **run** Tapedeck (end user) | To **build** Tapedeck (developer) |
|---|---|---|
| Operating system | Windows 10 or 11, **64-bit** | Windows 10 or 11, 64-bit |
| .NET | Not needed (included in the release) | .NET 8 SDK (installed with Visual Studio) |
| Software | None – no VLC, no codecs | Visual Studio 2026 with the **.NET desktop development** workload |
| Disk space | About **360 MB** unzipped (the ZIP is about 150 MB) | About 1 GB for the SDK, NuGet packages and build output |
| Screen | 1024 × 680 or larger; tested at 100% and 125% scaling | – |
| Files to play | `.mp3` and `.mp4` | – |

## 8.2 Publish with Visual Studio

1. Open `Tapedeck.sln` in Visual Studio 2026 and set the configuration to **Release**. Build once (**Ctrl+Shift+B**) and make sure there are **0 errors**.
2. In **Solution Explorer**, right-click the **Tapedeck** project → **Publish…**
3. Target: **Folder** → Next → Folder location: `bin\Release\publish\` → **Finish**.
4. On the publish profile, click **Show all settings** and set:

   | Setting | Value |
   |---|---|
   | Configuration | **Release** |
   | Target framework | **net8.0-windows** |
   | Deployment mode | **Self-contained** |
   | Target runtime | **win-x64** |
   | Produce single file | **Off** – LibVLC's native plugins must sit next to the .exe |
   | Trim unused code | **Off** – Windows Forms does not support trimming |

5. **Save**, then click **Publish**. When it finishes, click the folder link to open `Tapedeck\bin\Release\publish\`.

## 8.3 Publish from the command line

The project file already sets the Release settings (`RuntimeIdentifier` win-x64, `SelfContained` true, `PublishSingleFile` false, `PublishTrimmed` false), so no extra options are needed. From the repository folder, in PowerShell or the Developer Command Prompt:

```powershell
dotnet publish Tapedeck\Tapedeck.csproj -c Release -o Tapedeck\bin\Release\publish
```

The same command with every setting written out (gives the same result):

```powershell
dotnet publish Tapedeck\Tapedeck.csproj -c Release -r win-x64 --self-contained true -p:PublishSingleFile=false -p:PublishTrimmed=false -o Tapedeck\bin\Release\publish
```

## 8.4 Check the publish folder and zip it as G3_BSCS2A_Release.zip

### What the publish folder must contain

Check `Tapedeck\bin\Release\publish\` before zipping:

| Item | Why it is needed |
|---|---|
| `Tapedeck.exe` | The program. Double-click it to start. |
| `Tapedeck.dll`, `Tapedeck.runtimeconfig.json`, `Tapedeck.deps.json` | The compiled code and how .NET starts it. |
| About 240 other `.dll` files (`System.*.dll`, `Microsoft.*.dll`, `coreclr.dll`, …) and language folders (`de`, `fr`, `ja`, …) | The .NET 8 runtime and Windows Forms, included because the build is self-contained. |
| `LibVLCSharp.dll`, `LibVLCSharp.WinForms.dll` | The C# side of the playback engine. |
| `libvlc\win-x64\` (`libvlc.dll`, `libvlccore.dll`, `plugins\`, …) | The native VLC engine that actually decodes mp3 and mp4. **Without it nothing plays.** |
| `Assets\` (skin, buttons, `logo.png`, `logo.ico`) and `Assets\Team\` (member photos) | The skin and the splash screen. A missing image does not crash the app, but it falls back to plain colours. |

Do **not** copy `Library.txt` or `Favorites.txt` from your own computer into the release – each user starts with an empty library, and the files are created next to `Tapedeck.exe` on first save.

### Zip it

1. Open `Tapedeck\bin\Release\publish\`.
2. Make sure it has no `Library.txt` or `Favorites.txt` (delete them if you ran the program from this folder).
3. Zip the **contents** of the folder (not the folder above it):
   - **File Explorer:** select everything inside `publish` (Ctrl+A) → right-click → **Compress to → ZIP file** (Windows 11) or **Send to → Compressed (zipped) folder** (Windows 10) → rename it `G3_BSCS2A_Release.zip`.
   - **PowerShell:**
     ```powershell
     Compress-Archive -Path Tapedeck\bin\Release\publish\* -DestinationPath G3_BSCS2A_Release.zip -Force
     ```
4. The ZIP should be about 150 MB.

## 8.5 Test on a second computer

Use a Windows 10/11 64-bit computer that has **never had Visual Studio or the .NET SDK installed** (a classmate's or a lab PC). Copy `G3_BSCS2A_Release.zip` and a few test files (at least one `.mp3` and one `.mp4`) to it.

### Checklist

| # | Check | Expected result |
|---|---|---|
| D1 | Right-click the ZIP → **Extract All** to `Documents\Tapedeck` (do not run it from inside the ZIP). | A `Tapedeck` folder with `Tapedeck.exe`, `libvlc\` and `Assets\`. |
| D2 | Double-click `Tapedeck.exe`. If **"Windows protected your PC"** appears, click **More info → Run anyway**. | The splash screen shows the logo, title, group, section and every member's photo, name and role, then closes after about 3 seconds or on a click/key. |
| D3 | Look at the main window. | The retro skin, logo and button images load; text is readable. Status: "The library is empty. Add files with Ctrl+O." |
| D4 | **Add Folder** (Ctrl+Shift+O) with the test files. | Every mp3/mp4 appears with title, artist, album or resolution and duration. |
| D5 | Play an mp3, then an mp4. | Sound plays; the video shows in the video area; elapsed time counts up (00:00:05 / 00:03:25). |
| D6 | Pause, Stop, Next, Previous, volume, seek bar, Shuffle, Repeat, and their keyboard shortcuts (Help > Keyboard Shortcuts). | Each one works from the button and from the shortcut. |
| D7 | Close the program and open it again. | The same items load ("Loaded N item(s) from Library.txt"). `Library.txt` is now next to `Tapedeck.exe`. |
| D8 | Rename one test file in File Explorer, then reopen Tapedeck and try to play it. | Its row is greyed out, the status counts 1 missing file, and playing it shows "This file was moved or deleted". No crash. |
| D9 | Close Tapedeck, copy `docs\test-data\Library_corrupt.txt` over `Library.txt`, and start it again. | "7 damaged line(s) in Library.txt were skipped." and 4 items load. No crash. |
| D10 | File > Open Library in Notepad (Ctrl+L), delete one line, save, then File > Reload Library (Ctrl+Shift+L). | Notepad opens `Library.txt`; after reloading, the deleted item is gone. |
| D11 | Mark a favorite and give a rating, close and reopen. | The ♥ and ★ come back (`Favorites.txt` next to the .exe). |
| D12 | Windows **Settings → Display → Scale: 125%**, sign out and in, and start Tapedeck. | The window and text still fit and are readable (NFR6). |
| D13 | Copy the extracted folder into `C:\Program Files\Tapedeck` and add a file. | A clear message that the library could not be saved, with the hint to move the program to Documents. No crash. |

### Test results

*To be filled in on the second computer (task H13).*

| | |
|---|---|
| **Tester** | |
| **Date** | |
| **Computer** (brand / model) | |
| **Windows version** (Settings → System → About) | |
| **.NET or Visual Studio installed?** | |
| **Display scaling** | |
| **Release ZIP** (file name and size) | |

| # | Result (Pass / Fail) | Notes (what happened, screenshot file name) |
|---|---|---|
| D1 | | |
| D2 | | |
| D3 | | |
| D4 | | |
| D5 | | |
| D6 | | |
| D7 | | |
| D8 | | |
| D9 | | |
| D10 | | |
| D11 | | |
| D12 | | |
| D13 | | |

## 8.6 Known limitations

- **Windows 64-bit only.** The release is built for win-x64; it does not run on 32-bit Windows, macOS or Linux.
- **Only `.mp3` and `.mp4`** can be added; other formats are skipped with a message.
- **Large download.** The release includes the whole .NET runtime and the VLC engine, so it is about 360 MB unzipped. It also contains a `libvlc\win-x86\` folder (about 95 MB) that the 64-bit build never uses.
- **Unsigned program.** Windows SmartScreen may warn the first time (**More info → Run anyway**). A downloaded ZIP may also be marked as blocked: right-click the ZIP → **Properties → Unblock** before extracting.
- **Library files sit next to `Tapedeck.exe`.** In a protected folder such as `C:\Program Files`, the library cannot be saved; keep the program in Documents or on the Desktop.
- **Library entries are file paths.** If a media file is moved or renamed, its entry is greyed out and must be removed and added again.
- **One library per copy of the program.** There are no separate user accounts (out of scope).
