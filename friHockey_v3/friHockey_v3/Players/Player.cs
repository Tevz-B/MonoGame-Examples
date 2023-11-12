using System.Collections;
using friHockey_v3.Scene.Objects;
using Microsoft.Xna.Framework;

namespace friHockey_v3.Players;

public enum PlayerPosition
{
    Top,
    Bottom
}

public abstract class Player
{
    protected Mallet _mallet;
    protected ArrayList _scene;
    protected PlayerPosition _position;

    protected Player(Mallet mallet, ArrayList scene, PlayerPosition position)
    {
        _mallet = mallet;
        _scene = scene;
        _position = position;
    }

    public virtual void Update(GameTime gameTime)
    {
    }
}