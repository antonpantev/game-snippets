Install the following the Package Manager
* Lightweight RP
* Shader Graph

**NOTE:** I dont have the Lightweight Rendering Pipeline on by default because it causes bugs in other scenes so reapply it when I want this scene to work

Create > Rendering > Lightweight Rendering Pipeline > Pipeline Asset

Edit > Projects Settings > Graphics > Scriptable Render Pipeline Settings > Select the previous step's Pipeline Asset

Create > Shader > PBR Graph

Create new Material and attach the previous step's Shader

Attach the Material to a Sphere

Double click Shader to open in Shader Graph

Right Click > Create Node > Type Position

Hit "Save Asset" in the Shader Graph window to see all the changes in play mode. Normal saving doesn't really save everything in Shader Graph.

Followed this video:
https://www.youtube.com/watch?v=vh85pzT959M