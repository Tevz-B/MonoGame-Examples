# MonoGame Examples

## Notes

### Run
- To run examples you need MonoGame plugins installed as well as .NET 6 sdk
- Tested on Windows 10 and MacOS Sonoma (M1 processor)

### Edit
- Examples were written in JetBrains Rider IDE and have relevant config files attached
- To edit content resources run ```dotnet mgcb-editor .\Content.mgcb``` from inside Project's Content dir
    - To change how the resouce is loaded or to load SpiteFonts and other non-default resources, click on the resource in mgcb gui and select appropriate load properties

### How to
- Create new empty solution (project): Rider -> New Solution -> select 'MonoGame Crossplatfrom Desktop Application' template or 'MonoGame Library' template
- Add your own library to solution: right click on solution -> Add -> Existing project -> select *.proj file

### Code style
- JetBrains Rider IDE code style can be configured in the settings or for advanced users in .editorconfig file