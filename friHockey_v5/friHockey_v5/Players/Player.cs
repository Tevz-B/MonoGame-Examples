using System.Collections;
using friHockey_v5.SceneObjects;
using Microsoft.Xna.Framework;

namespace friHockey_v5.Players;

public enum PlayerPosition
{
    Top,
    Bottom
}

public abstract class Player : GameComponent
{
    protected Mallet _mallet;
    protected ArrayList _scene;
    protected PlayerPosition _position;

    protected Player(Game theGame, Mallet mallet, PlayerPosition position) 
        : base(theGame)
    {
        _mallet = mallet;
        _position = position;
    }
    
    public virtual void Reset()
    {}
}