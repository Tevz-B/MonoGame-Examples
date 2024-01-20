- this.Game.Content ... -> this.Content (delete Game)
- open mgcb editor (dotnet mgcb-editor) in Rider integrated terminal, and run it from Content folder
- for custom rules for Protected members in Rider use .editoconfig, put it in ~/ (home) folder for default, or in project for specific project
- build on ARM (M1) Mac: https://stackoverflow.com/questions/63607158/xcode-building-for-ios-simulator-but-linking-in-an-object-file-built-for-ios-f 
	- Basically, you have to exclude `arm64` for the simulator architecture, both from your project and the Pod project.
- create new version: create new solution from template (Monogame Crossplatform desktop application), then copy most files and delete Game1.cs, fix Program.cs to run propper file. 
  (dont copy obj and bin dirs, copy only Content.mgcb and resources from Content dir)
- add lib/proj : solution->add existing project -> select *.proj file 
