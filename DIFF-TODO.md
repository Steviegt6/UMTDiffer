# UMTDiffer To-Do List
This article serves as a tracker for tasks that are completed, in progress, and need to be worked on.

Illustrated here is the general scope of my fork, and its planned features.

## Diffing
The main focus of this tool is enabling collaborate mod development by implementing a tracking system for modified attributes and content in data files. This is called diffing and applying these diffs is called patching.

Listed below is a list of features in their various stages of development.

### To-Do
* General info data (low priority)
  * Options stuff
* Audio groups (low priority)
* Sounds (low priority)
  * Adding and modifying items
* Sprites (low priority)
  * Adding and modifying items
* Backgrounds & Tile sets (low priority)
  * Adding and modifying items
* Paths (low priority)
* Scripts (high priority)
  * Adding new scripts
* Shaders (medium priority)
  * Diffing raw source (thank god lol)
* Fonts (low priority)
  * Adding and modifying items
* Timelines (low priority)
* Game objects (medium priority)
  * Adding and modifying items
* Rooms
  * Adding and modifying items (low priority)
* Extensions (low priority)
* Texture page items (low priority)
  * Adding and modifying items, adding additional atlases?
* Code (high priority)
  * Adding new code objects
* Variables (high priority)
  * Adding and modifying items
* Functions (high priority)
  * Adding and modifying items
* Code locals (low priority)
* Embedded textures (high priority)
  * Adding and modifying items
* Embedded audio (low priority)

### Finished
* Scripts
  * Diffing DECOMPILED code
* Code
  * Diffing existing code
* Strings
  * Adding and modifying items

## Mod Loading
Additionally, I plan to include a feature in the CLI program that lets you process the data file and make in-memory changes prior to running it. This is called mod loading, as it will load and interface with .NET assemblies, which will have access to a data file's various qualities, such as code and what-not. Additionally, a dynamic GML patching API will be provided, allowing you to write GML code that will be added to the data file, as well as writing GML code which can then be inserted at the beginning or end of functions as well as entirely replacing functions.
