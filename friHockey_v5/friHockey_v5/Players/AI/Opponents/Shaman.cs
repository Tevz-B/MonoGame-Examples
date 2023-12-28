using friHockey_v5.Players.AI.Agents;
using friHockey_v5.Scene;
using friHockey_v5.Scene.Objects;
using Microsoft.Xna.Framework;

namespace friHockey_v5.Players.AI.Opponents;

public class Shaman : ReflexAgent
{
    public Shaman(Game theGame, Mallet theMallet, Level theLevel, PlayerPosition thePosition)
        : base(theGame, theMallet, theLevel, thePosition)
    {
        // Gameplay
        _name = "Shaman";
        _quotes.Add("Monkeys can communicate over distances of a few kilometers and even across continents.");

        _opponentType = Opponents.OpponentType.Shaman;
        _levelType = Scene.LevelType.Hockey;

        _portraitPath = "shaman-small";
        _hiddenPortraitPath = "shaman-hidden";
        _fullPortraitPath = "shaman";
    }

    public override void Initialize()
    {
        base.Initialize();
        
        _speed = 60;
        _attackSpeed = 300;
        _attackFactor = 1.5f;
    }

}