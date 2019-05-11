Install the following the Package Manager
* Lightweight RP
* Shader Graph

(Not sure if the Lightweight Rendering Pipeline is really needed or not I should try not using it and seeing if there is a difference)

Create > Rendering > Lightweight Rendering Pipeline > Pipeline Asset

Edit > Projects Settings > Graphics > Scriptable Render Pipeline Settings > Select the previous step's Pipeline Asset

Create > Shader > PBR Graph

Create new Material and attach the previous step's Shader

Attach the Material to a Sphere

Double click Shader to open in Shader Graph

Right Click > Create Node > Type Position

Followed this video:
https://www.youtube.com/watch?v=vh85pzT959M