using friHockey_v5.Level;
using friHockey_v5.Players.AI.Agents;
using friHockey_v5.SceneObjects;
using Microsoft.Xna.Framework;

namespace friHockey_v5.Players.AI.Opponents;

public class Shaman : ReflexAgent
{
    public Shaman(Game theGame, Mallet theMallet, LevelBase theLevelBase, PlayerPosition thePosition)
        : base(theGame, theMallet, theLevelBase, thePosition)
    {
        // Gameplay
        _name = "Shaman";
        _quotes.Add("Monkeys can communicate over distances of a few kilometers and even across continents.");

        _opponentType = OpponentType.Shaman;
        _levelType = LevelType.Hockey;

        _portraitPath = "shaman-small";
        _hiddenPortraitPath = "shaman-hidden";
        _fullPortraitPath = "shaman";
    }

    public static LevelType LevelClassType()
    {
        return _levelType;
    }

    public override void Initialize()
    {
        base.Initialize();
        
        _speed = 60;
        _attackSpeed = 300;
        _attackFactor = 1.5f;
    }

}