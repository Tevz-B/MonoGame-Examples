using friHockey_v5.Level;
using friHockey_v5.Players.AI.Agents;
using friHockey_v5.SceneObjects;
using Microsoft.Xna.Framework;

namespace friHockey_v5.Players.AI.Opponents;

public class Iceman : ReflexAgent
{
    public const string Name = "Iceman";
    public const string PortraitPath = "iceman-small";
    public const string HiddenPortraitPath = "iceman-hidden";
    public const string FullPortraitPath = "iceman";
    
    public new const LevelType LevelType = friHockey_v5.Level.LevelType.Hockey;
    
    public Iceman(Game theGame, Mallet theMallet, LevelBase theLevelBase, PlayerPosition thePosition)
        : base(theGame, theMallet, theLevelBase, thePosition)
    {
        // Gameplay
        _quotes.Add("So here you're just hardcore, or what? No softies around?");
        _opponentType = OpponentType.Iceman;
    }
    
    public override LevelType GetLevelType()
    {
        return LevelType;
    }

    public override void Initialize()
    {
        base.Initialize();
        
        // AI properties
        _speed = 100; 
        _attackSpeed = 150; 
        _attackFactor = 500; 
    }
}