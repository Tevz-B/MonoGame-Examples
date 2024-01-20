using Express.Scene.Objects.Composites;
using Microsoft.Xna.Framework;

namespace friHockey_v2.Scene.Objects;

public class Puck : IParticle
{
    private Vector2 _position;
    private Vector2 _velocity;
    private float _mass = 2;
    private float _radius = 40;
    
    public ref Vector2 Position => ref _position;
    public ref Vector2 Velocity => ref _velocity;

    public float Radius
    {
        get => _radius;
        set => _radius = value;
    }
    
    public float Mass
    {
        get => _mass;
        set => _mass = value;
    }
}