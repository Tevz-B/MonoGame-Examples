using friHockey_v5.Level;
using friHockey_v5.Players.AI.Agents;
using friHockey_v5.SceneObjects;
using Microsoft.Xna.Framework;

namespace friHockey_v5.Players.AI.Opponents;

public class Iceman : ReflexAgent
{
    public Iceman(Game theGame, Mallet theMallet, LevelBase theLevelBase, PlayerPosition thePosition)
        : base(theGame, theMallet, theLevelBase, thePosition)
    {
        // Gameplay
        _name = "Iceman";
        _quotes.Add("So here you're just hardcore, or what? No softies around?");

        _opponentType = OpponentType.Iceman;
        _levelType = LevelType.Hockey;

        _portraitPath = "iceman-small";
        _hiddenPortraitPath = "iceman-hidden";
        _fullPortraitPath = "iceman";
    }

    public override void Initialize()
    {
        base.Initialize();
        
        // AI properties
        _speed = 1; // 70
        _attackSpeed = 1; // 150
        _attackFactor = 100; // 600
    }
}