using System;
using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Composites;
using Microsoft.Xna.Framework;

namespace Breakout.Scene.Objects;

public class Ball : IParticle, ICustomCollider
{
    protected Vector2 _position = new();
    protected Vector2 _velocity = new();
    protected float _radius = 8;
    protected float _mass = 1;
    protected bool _breakthroughPower;

    public bool BreakthroughPower
    {
        get => _breakthroughPower;
        set => _breakthroughPower = value;
    }

    public ref Vector2 Position => ref _position;

    public ref Vector2 Velocity => ref _velocity;

    public float Mass
    {
        get => _mass;
        set => _mass = value;
    }

    public float Radius
    {
        get => _radius;
        set => _radius = value;
    }

    public bool CollidingWith(object item, bool defaultValue = true)
    {
        // Dont collide with balls
        if (item is Ball)
        {
            return false;
        }

        // Dont Collide with bricks when the ball has breakthrough power
        if (item is Brick && _breakthroughPower)
        {
            return false;
        }

        return true;
    }

    public void CollidedWith(object item)
    {
        // Make sure the vertical velocity is big enough after collision,
        // so we don't have to endlessly wait for the ball to come down.
        float minY = Constants.MinimumBallVerticalVelocity;
        if (MathF.Abs(_velocity.Length()) < minY)
        {
            float speed = _velocity.Length();
            float x = MathF.Sqrt(speed * speed - minY * minY);
            _velocity.Y = _velocity.Y < 0 ? -minY : minY;
            _velocity.X = _velocity.X < 0 ? -x : x;
        }
    }
}