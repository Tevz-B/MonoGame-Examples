using friHockey_v6.Level;
using friHockey_v6.Players.AI.Agents;
using friHockey_v6.SceneObjects;
using Microsoft.Xna.Framework;

namespace friHockey_v6.Players.AI.Opponents;

public class Shaman : ReflexAgent
{
    public const string Name = "Shaman";
    public const string PortraitPath = "shaman-small";
    public const string HiddenPortraitPath = "shaman-hidden";
    public const string FullPortraitPath = "shaman";

    public new const LevelType LevelType = Level.LevelType.Hockey;
    
    public Shaman(Game theGame, Mallet theMallet, LevelBase theLevelBase, PlayerPosition thePosition)
        : base(theGame, theMallet, theLevelBase, thePosition)
    {
        // Gameplay
        _quotes.Add("Monkeys can communicate over distances of a few kilometers and even across continents.");
        _opponentType = OpponentType.Shaman;
    }
    
    public override LevelType GetLevelType()
    {
        return LevelType;
    }

    public override void Initialize()
    {
        base.Initialize();
        
        // AI properties
        _speed = 60; 
        _attackSpeed = 300; 
        _attackFactor = 1.5f; 
    }

}