# Design concept and Canva source

**Designer:** Christian Maddox Rivera (Maddox)
**Project:** Tapedeck - OOP Group 3, BSCS 2-A

## Canva link

https://canva.link/gjiv1j4cuvzy9hu

## Files

- `design_concept.png` - design concept (this folder)
- `Assets/tapedeck_skin.png` - skin background, 980 x 620
- `Assets/splash_bg.png` - splash background (cassette tape), 980 x 620
- `Assets/logo.png` - logo, 512 x 512
- `Assets/logo.ico` - app icon, 16/32/48/256

## Retro cassette palette

Both PNGs and `Theme.cs` use these colours. If the art is re-exported from Canva, keep the
same colours and the same positions (panels, video area, card area), because the controls
are placed on top of them.

| Use | Colour | Hex |
|---|---|---|
| Shell, title bar | charcoal | `#343437` |
| Label, panels, menu | cream | `#F8EADB` |
| Stripe 1 | red | `#DE4852` |
| Stripe 2 | orange | `#ECA83E` |
| Stripe 3 | green | `#A7C758` |
| Stripe 4 | teal | `#58BDB8` |
| Tape, video frame | brown | `#4A2E1E` |
| Text | charcoal ink | `#2B2B2D` |
| Secondary text | warm brown | `#7A5A44` |

Splash layout (`SplashForm.cs`): label 56,34 to 924,482; stripes at y 128-176;
member cards in 77,190 (826 x 274); bottom tab from y 494.
