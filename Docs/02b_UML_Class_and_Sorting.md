# 2. UML Class Diagram and Sorting Analysis

**Project:** Tapedeck – OOP10 Media Player  
**Group:** Group 3 – BSCS 2-A  
**Author:** Ammaar

## 2.1 Class Diagram

The class diagram shows the main classes, interfaces, inheritance relationships, and associations used in Tapedeck.

**Main classes and their responsibilities:**

| Class / Interface | Responsibility |
|---|---|
| `MediaItem` | Abstract base class containing shared media information such as title, artist, duration, and file path. |
| `AudioItem` | Represents an MP3 audio file and provides audio-specific information. |
| `VideoItem` | Represents an MP4 video file and provides video-specific information. |
| `IMediaControls` | Defines the standard playback operations: Play, Pause, Stop, Next, and Previous. |
| `BasePlayerForm` | Implements the playback controls and manages the LibVLC media player. |
| `TapedeckForm` | Main application form. Handles the playlist, user interface, playback events, sorting, searching, and library operations. |
| `LibrarySorter` | Provides comparison, insertion sorting, binary search, and partial search operations. |
| `MediaFormats` | Checks supported file formats and creates the appropriate media item. |
| `LibraryStore` | Loads and saves the media library. |
| `FavoritesStore` | Saves and restores favorite and rating information. |

### Inheritance and relationships

- `AudioItem` and `VideoItem` inherit from `MediaItem`.
- `BasePlayerForm` implements `IMediaControls`.
- `TapedeckForm` inherits from `BasePlayerForm`, allowing it to reuse the playback functionality.
- `MediaFormats` creates `AudioItem` or `VideoItem` objects and returns them as `MediaItem`.
- `LibrarySorter` operates on collections of `MediaItem` objects.
- `TapedeckForm` uses `LibraryStore` and `FavoritesStore` to manage saved data.

**Class diagram source:** `diagrams/class_diagram.puml`

## 2.2 Sorting Algorithm

Tapedeck uses a hand-written insertion sort algorithm to arrange media items based on a selected sorting key, such as title, artist, album or resolution, duration, or rating.

Insertion sort builds a sorted section of the list one item at a time. Each new item is compared with the items before it and moved into its correct position.

### Insertion Sort Trace

**Initial list:**

`[Sunrise, Apple, Night, Bass]`

Sorting is performed alphabetically by title.

| Pass | Current Item | Comparisons | Shifts | List After Pass |
|---|---|---:|---:|---|
| 1 | Apple | 1 | 1 | [Apple, Sunrise, Night, Bass] |
| 2 | Night | 2 | 1 | [Apple, Night, Sunrise, Bass] |
| 3 | Bass | 3 | 3 | [Apple, Bass, Night, Sunrise] |

**Pass explanations:**

- **Pass 1:** Apple is compared with Sunrise. Since Apple comes first alphabetically, Sunrise shifts right.
- **Pass 2:** Night is compared with Sunrise, which shifts right. Night is then compared with Apple and placed after it.
- **Pass 3:** Bass is compared with Sunrise, Night, and Apple. All three items shift right until Bass reaches the second position.

**Total comparisons:** 6  
**Total shifts:** 5

### Time Complexity

| Case | Time Complexity | Explanation |
|---|---|---|
| Best Case | O(n) | The list is already sorted, so each item needs only one comparison. |
| Average Case | O(n²) | Items may need to be compared and shifted several times. |
| Worst Case | O(n²) | The list is in reverse order, requiring the maximum number of comparisons and shifts. |

### Why Insertion Sort?

Insertion sort was selected because it is simple, stable, and works well with small or nearly sorted collections.

Compared with selection sort, insertion sort can finish faster when the list is already or nearly sorted. Compared with bubble sort, it generally performs fewer unnecessary swaps because it shifts items directly into their correct positions.

It is suitable for Tapedeck because the application manages a relatively small media library and allows users to sort it using different properties.