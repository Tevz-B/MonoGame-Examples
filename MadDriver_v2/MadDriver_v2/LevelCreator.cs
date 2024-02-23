using System;
using MadDriver_v2.Scene;
using MadDriver_v2.Scene.Levels;
using Microsoft.Xna.Framework;

namespace MadDriver_v2;

public static class LevelCreator
{
    public static Level Create(LevelType levelType, Game game)
    {
        switch (levelType)
        {
            case LevelType._Intro: return new Intro(game);
            case LevelType.Suburbs: return new Suburbs(game);
            case LevelType.City: return new City(game);
            case LevelType.Highway:
            case LevelType.Graveyard:
            case LevelType.Warehouse:
            case LevelType.LastType:
            default:
                throw new ArgumentOutOfRangeException(nameof(levelType), levelType, "Can't create level!");
        }
    }
}