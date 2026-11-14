# 8. Deployment Guide

**Project:** Tapedeck (OOP10 media player) · **OOP Group 3 – BSCS 2-A**
**Author:** Heaven · **Release build and second-computer test:** Joey Cuison · **Updated:** November 12, 2026 (Milestone 4)

---

## 8.1 System Requirements

**Table 11. Deployment system requirements**

| | |
|---|---|
| **Operating system** | Windows 10 or 11, 64-bit |
| **.NET 8 Desktop Runtime** | **Not required** on the target computer. The application is published as a self-contained build that includes the .NET 8 runtime. |
| **Development tools (to build)** | Visual Studio 2026 (or 2022) with the .NET desktop development workload; internet access for the first NuGet restore |

## 8.2 Publish Configuration

**Table 12. Publish settings**

| Setting | Required by the guide | Setting used by the team |
|---|---|---|
| **Configuration** | Release | Release |
| **Target framework** | net8.0-windows | net8.0-windows |
| **Target runtime** | win-x64 | win-x64 |
| **Deployment mode** | Self-contained (or framework-dependent, if documented) | Self-contained |
| **Produce single file** | Off | Off |
| **Trim unused code** | Off | Off |
| **Assets files** | Copy to Output Directory | Copy to Output Directory |

These Release settings are written in `Tapedeck.csproj`, so the publish command needs no extra options.

## 8.3 Build and Publish Procedure

1. Set the build configuration to **Release**; build once (Ctrl+Shift+B) and confirm 0 errors and that every file in Assets is set to Copy to Output Directory.
2. Right-click the project in Solution Explorer and choose **Publish**; choose Target: **Folder** and the location `bin\Release\publish`.
3. In the publish settings, apply the values in Section 8.2 (Release, net8.0-windows, win-x64, Self-contained, single file off, trimming off), then click **Publish**.
4. Command-line equivalent, from the repository folder:

   ```powershell
   dotnet publish Tapedeck\Tapedeck.csproj -c Release -o Tapedeck\bin\Release\publish
   ```

5. Check that the publish folder contains **`Tapedeck.exe`**, the **Assets** folder (skin, buttons, logo, icon and `Assets\Team` photos) and the **`libvlc\win-x64`** folder with its plug-ins. Delete `Library.txt` and `Favorites.txt` if the program was run from this folder.
6. Close Tapedeck, then compress the contents of the publish folder to **`G3_BSCS2A_Release.zip`**:

   ```powershell
   Compress-Archive -Path Tapedeck\bin\Release\publish\* -DestinationPath G3_BSCS2A_Release.zip -Force
   ```

   The ZIP is submitted through the course page and is not committed to the repository (`*.zip` is in `.gitignore`).

## 8.4 Application Icon and Logo

The group logo, a cassette tape with a play symbol, was designed in Canva by Christian Maddox Rivera and saved as `Assets\logo.png` with its build property set to Copy to Output Directory. This image is shown on the splash screen and in the title bar of the main window. It was also saved as a multi-size icon file, `Assets\logo.ico`, which is set as the application icon in the project properties (`<ApplicationIcon>Assets\logo.ico</ApplicationIcon>`), so it appears on `Tapedeck.exe`, the taskbar and Alt+Tab. In the same project properties the product name is set to **Tapedeck**, the company to **OOP Group 3 - BSCS 2-A**, the authors to the five members, and the version to **1.0.0**, so the file properties of the program show the team's details.

![Tapedeck logo](../Tapedeck/Assets/logo.png)

**Figure 7. Tapedeck logo and application icon**

## 8.5 Installation Test on a Different Computer

**Table 13. Second-computer installation test**

| | |
|---|---|
| **Computer** | A classmate's laptop (BSCS 2-A), not used for development |
| **Operating system / version** | Windows 11 Pro, 64-bit |
| **Visual Studio installed?** | No (clean test environment) |
| **Release package** | `G3_BSCS2A_Release.zip` (self-contained, win-x64) |
| **Extracted to** | A writable folder in the user profile |
| **Date / tester** | November 12, 2026 / Joey Cuison |
| **Result** | Pass |
| **Issues found** | None. Splash screen, playback, saving and reloading the library, and error handling all worked (`05_Testing.md` Table 5). |

## 8.6 Known Limitations

- The application targets 64-bit Windows (x64) only.
- Only MP3 and MP4 files are supported; other formats are skipped with a message.
- The application folder must be writable, because `Library.txt` and `Favorites.txt` are created beside the .exe.
- The self-contained release is large (about 360 MB extracted) because it includes the .NET runtime and the VLC engine.
- The program is not digitally signed, so Windows SmartScreen may warn on the first start.
- The library stores file paths; a moved or renamed file must be added again.
- Because the LibVLC engine draws the video directly, no controls or graphics can be placed on top of the video panel.

## 8.7 Refactoring Done in Milestone 4

As required by guide Section 5.2, the refactoring made in this milestone is recorded with the reason for it. The complete record (R1–R11) is in `03_Development_Log.md`.

**Table 14. Milestone 4 refactoring**

| No. | Refactoring | Why |
|---|---|---|
| R10 | `BasePlayerForm.PrepareEngineAsync()` loads the LibVLC engine on a background thread; `Program.cs` starts it before showing the splash screen and passes it to the main form. The original constructors are kept so the Visual Studio designer still opens the forms. | Fixes TD-01: the engine's 1–2 s load now overlaps with the splash screen instead of delaying the main window. |
| R11 | `PlaceStatusBar()` was renamed `PlaceSkinControls()` and now places the Now Playing lines, the elapsed time and the seek bar from the skin's coordinates after scaling, and brings the elapsed time and seek bar to the front. | Fixes B06: at 200% scaling the elapsed time and seek bar were covered. |
