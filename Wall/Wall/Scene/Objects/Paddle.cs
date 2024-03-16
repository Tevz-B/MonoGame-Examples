using System;
using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Wall.Scene.Objects;

public class Paddle : IAARectangleCollider, ICustomCollider
{
    protected Vector2 _position = new();
    protected float _width = 112;
    protected float _height = 40;
    
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

    public bool CollidingWith(object item)
    {
        return true;
    }

    public void CollidedWith(object item)
    {
        if (item is Ball ball)
        {
            float speed = ball.Velocity.Length();
            float hitPosition = (ball.Position.X - _position.X) / _width * 2;
            float angle = hitPosition * Constants.MaximumBallAngle;
            ball.Velocity.X = MathF.Sin(angle);
            ball.Velocity.Y = -MathF.Cos(angle);
            ball.Velocity *= speed;
            float minY = Constants.MinimumBallVerticalVelocity;
            if (MathF.Abs(ball.Velocity.Y) < minY)
            {
                ball.Velocity.Y = ball.Velocity.Y < 0 ? -minY : minY;
            }

        }

    }

}