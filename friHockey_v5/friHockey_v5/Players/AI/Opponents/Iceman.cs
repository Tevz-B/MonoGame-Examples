using friHockey_v5.Players.AI.Agents;
using friHockey_v5.Scene;
using friHockey_v5.Scene.Objects;
using Microsoft.Xna.Framework;

namespace friHockey_v5.Players.AI.Opponents;

public class Iceman : ReflexAgent
{
    public Iceman(Game theGame, Mallet theMallet, Level theLevel, PlayerPosition thePosition)
        : base(theGame, theMallet, theLevel, thePosition)
    {
        // Gameplay
        _name = "Iceman";
        _quotes.Add("So here you're just hardcore, or what? No softies around?");

        _opponentType = Opponents.OpponentType.Iceman;
        _levelType = Scene.LevelType.Hockey;

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