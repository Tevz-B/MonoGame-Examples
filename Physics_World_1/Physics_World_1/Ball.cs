using Express.Scene.Objects;
using Express.Scene.Objects.Physical_Properties;
using Microsoft.Xna.Framework;

namespace Physics_World_1;

public class Ball : IParticle, ICoefficientOfRestitution
{
    protected Vector2 _position = new();
    protected Vector2 _velocity = new();
    protected float _radius;
    protected float _mass;
    protected float _coefficientOfRestitution = 0.85f;

    public ref Vector2 Position => ref _position;

    public ref Vector2 Velocity => ref _velocity;

    public float Mass { get; set; }
    public float Radius { get; set; }
    public float CoefficientOfRestitution { get; set; }
}