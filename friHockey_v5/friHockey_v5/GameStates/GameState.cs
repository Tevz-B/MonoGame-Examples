using Microsoft.Xna.Framework;

namespace friHockey_v5.GameStates;

public abstract class GameState : GameComponent
{
    protected FriHockey _friHockey;

    protected GameState(Game theGame)
        : base (theGame)
    {
        _friHockey = (FriHockey)Game;
    }

    public virtual void Activate()
    {
    }

    public virtual void Deactivate()
    {
    }

}