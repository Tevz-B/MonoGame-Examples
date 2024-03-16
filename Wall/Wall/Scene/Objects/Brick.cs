using System;
using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Wall.Scene.Objects;

public enum BrickStyle
{
    Blue = 0,
    Red,
    Magenta,
    Green,
    Yellow,
    Last
}

public class Brick : IAARectangleCollider, ICustomCollider
{
    protected BrickStyle _style;
    protected Vector2 _position = new();
    protected float _width = 50;
    protected float _height = 24;
    protected bool _destroyed;

    public BrickStyle Style
    {
        get => _style;
        set => _style = value;
    }
    
    public ref Vector2 Position => ref _position;

    public float Width
    {
        get => _width;
        set => _width = value;
    }

    public float Height
    {
        get => _height;
        set => _height = value;
    }

    public bool Destroyed => _destroyed;

    public bool CollidingWith(object item)
    {
        return true;
    }

    public void CollidedWith(object item)
    {
        _destroyed = true;
        if (item is Ball ball)
        {
            float minY = Constants.MinimumBallVerticalVelocity;
            if (MathF.Abs(ball.Velocity.Y) < minY)
            {
                ball.Velocity.Y = ball.Velocity.Y < 0 ? -minY : minY;
            }
        }
    }


}