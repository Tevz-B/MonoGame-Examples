using Microsoft.Xna.Framework;

namespace MadDriver_v2.Scene.Levels;

public class City : Level
{
    public City(Game theGame)
        : base (theGame)
    {
        _type = LevelType.City;
    }
}