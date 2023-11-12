using Express.Scene.Objects;
using Microsoft.Xna.Framework;

namespace friHockey_v3.Scene.Objects;

public class Mallet : IParticle
{
    private Vector2 _position;
    private Vector2 _velocity;
    private float _mass = 40;
    private float _radius = 60;

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