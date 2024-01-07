using friHockey_v4.Players.AI.Agents;
using friHockey_v4.Scene;
using friHockey_v4.Scene.Objects;
using Microsoft.Xna.Framework;

namespace friHockey_v4.Players.AI.Opponents;

public class Iceman : ReflexAgent
{
    public Iceman(Game theGame, Mallet theMallet, Level theLevel, PlayerPosition thePosition)
        : base(theGame, theMallet, theLevel, thePosition)
    {
        // AI properties
        _speed = 0.1f; // 100
        _attackSpeed = 0.2f; // 150
        _attackFactor = 100; // 500
        
        // Gameplay
        _quotes.Add("So here you're just hardcore, or what? No softies around?");
    }
}