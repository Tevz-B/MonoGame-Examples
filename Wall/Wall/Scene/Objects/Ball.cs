using Express.Scene.Objects.Composites;
using Microsoft.Xna.Framework;

namespace Wall.Scene.Objects;

public class Ball : IParticle
{
    protected Vector2 _position = new();
    protected Vector2 _velocity = new();
    protected float _radius = 8;
    protected float _mass = 1;

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
}