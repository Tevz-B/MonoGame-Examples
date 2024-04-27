# MonoGame Examples

## Description
This is a rewrite of game demos in C# and MonoGame. The original demos were developed by Matej Jan and Bojan Klemenc for Game Development class at Faculty of Computer and Information Science, University of Ljubljana. The original examples used Objective-C with a proprietary [engine XNI](https://github.com/thes3m/XNI) (developed by Matej Jan for [his thesis](http://eprints.fri.uni-lj.si/1545/1/Jan1.pdf)).

## Game Demos
- FriHockey
- Physics World
- Pathfinding
- Mad Driver
- Wall
- Breakout
- Sprite Batch Rendering Examples

## Libraries
- Express
- Artificial I

Most Examples use libraries 'Artificial I' and 'Express' which focus on physics and some other utilities.

### Run
- To run examples you need MonoGame plugins installed as well as .NET 6 sdk
- Tested on Windows 10 and MacOS Sonoma (MacBook Air M1)

### Edit
- Examples were written in JetBrains Rider IDE and have relevant config files attached
- To edit content resources run ```dotnet mgcb-editor .\Content.mgcb``` from inside Project's Content dir
    - To change how the resouce is loaded or to load SpiteFonts and other non-default resources, click on the resource in mgcb gui and select appropriate load properties

### How to
- Create new empty solution (project): Rider -> New Solution -> select 'MonoGame Crossplatfrom Desktop Application' template or 'MonoGame Library' template
- Add your own library to solution: right click on solution -> Add -> Existing project -> select *.proj file

### Code style
- JetBrains Rider IDE code style can be configured in the settings or for advanced users in .editorconfig file

### Linux problems
- Make sure to have dotnet-sdk 6.0 installed and running in Rider terminal `dotnet --version`
- XImporter/FbxImporter Error: missing `libdl.so` - try creating sym link to `libdl.so`.
   - `ln -s /usr/lib/x86_64-linux-gnu/libdl.so.2 /usr/lib/x86_64-linux-gnu/libdl.so`
- Other build errors: the solution is usually to install missing libraries.
   - `sudo apt install libminizip-dev`
   - nvidia: nvtt - needs manual install
   - ...
