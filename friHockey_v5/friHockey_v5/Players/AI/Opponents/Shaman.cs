using friHockey_v5.Level;
using friHockey_v5.Players.AI.Agents;
using friHockey_v5.SceneObjects;
using Microsoft.Xna.Framework;

namespace friHockey_v5.Players.AI.Opponents;

public class Shaman : ReflexAgent
{
    public const string Name = "Shaman";
    public const string PortraitPath = "shaman-small";
    public const string HiddenPortraitPath = "shaman-hidden";
    public const string FullPortraitPath = "shaman";

    public new const LevelType LevelType = friHockey_v5.Level.LevelType.Hockey;
    
    public Shaman(Game theGame, Mallet theMallet, LevelBase theLevelBase, PlayerPosition thePosition)
        : base(theGame, theMallet, theLevelBase, thePosition)
    {
        // Gameplay
        // _name = "Shaman";
        _quotes.Add("Monkeys can communicate over distances of a few kilometers and even across continents.");

        _opponentType = OpponentType.Shaman;
        // _levelType = LevelType.Hockey;

        // _portraitPath = "shaman-small";
        // _hiddenPortraitPath = "shaman-hidden";
        // _fullPortraitPath = "shaman";
    }
    
    public override LevelType GetLevelType()
    {
        return LevelType;
    }

    // public static LevelType LevelClassType()
    // {
    //     return _levelType;
    // }

    public override void Initialize()
    {
        base.Initialize();
        
        _speed = 0.2f;
        _attackSpeed = 0.2f;
        _attackFactor = 1.5f;
    }

}