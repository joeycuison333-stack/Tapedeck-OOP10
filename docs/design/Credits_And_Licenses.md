# Credits and Licenses

**Project:** Tapedeck (media player)
**Group:** OOP Group 3 | **Section:** BSCS 2-A

## Team

| Member | GitHub | Role |
|---|---|---|
| Joey Cuison | [joeycuison333-stack](https://github.com/joeycuison333-stack) | Team lead / player engine, UI and integration |
| Sean Caling (Heaven) | [SeanC1801](https://github.com/SeanC1801) | Library saving, loading and deployment |
| Ammaar Azmy | [aksilol](https://github.com/aksilol) | Sorting and searching |
| Christian Maddox Rivera (Maddox) | [Maddox44907](https://github.com/Maddox44907) | Skin design and splash screen |
| Genevieve Valencia | [gdvalenciah](https://github.com/gdvalenciah) | Favorites, testing and user manual |

## Design and splash screen (Maddox)

- Splash screen code: `UI/SplashForm.cs`, `UI/TeamInfo.cs`
- Skin background, logo and app icon: designed in Canva (`Assets/tapedeck_skin.png`, `Assets/splash_bg.png`, `Assets/logo.png`, `Assets/logo.ico`)
- Design concept and Canva link: `docs/design/`

## Assets

| Asset | Files | Source / author | License |
|---|---|---|---|
| Skin and splash backgrounds | `Assets/tapedeck_skin.png`, `Assets/splash_bg.png` | Designed for this project (retro cassette design) | Original work of the group |
| Logo and app icon | `Assets/logo.png`, `Assets/logo.ico` | Created for this project | Original work of the group |
| Member photos | `Assets/Team/*.jpg` | Provided by each member | Used with the member's consent |
| Button images | `Assets/btn_*.png` | Icons made in Canva for this project | Canva Content License Agreement |
| Fonts | Segoe UI, Tahoma, Consolas, Arial Black | Microsoft (included with Windows) | Windows system fonts, not redistributed |

Canva designs follow the Canva Content License Agreement. Any Canva element used is covered by that license for this project.

## Tools and libraries

| Tool / library | Version | Use | License |
|---|---|---|---|
| Visual Studio 2026 | – | Development environment | Microsoft Visual Studio license |
| .NET 8 and Windows Forms | 8.0 | Application framework (included in the self-contained release) | MIT License |
| LibVLCSharp, LibVLCSharp.WinForms (VideoLAN) | 3.9.0 | C# bindings for the playback engine and the `VideoView` control | LGPL-2.1 |
| VideoLAN.LibVLC.Windows – libVLC / VLC engine (VideoLAN) | 3.0.21 | Native engine that decodes and plays mp3 and mp4 (`libvlc\win-x64\`) | LGPL-2.1-or-later |
| Git and GitHub | – | Version control and teamwork | GitHub Terms of Service |
| Canva | – | Skin, logo and design work | Canva Content License Agreement |

LibVLCSharp and libVLC are used unmodified as separate libraries (DLLs shipped next to `Tapedeck.exe`), as the LGPL allows. Their source code is available from VideoLAN at videolan.org and code.videolan.org.

## Notes

- Everything not listed above was written by the members of OOP Group 3.
