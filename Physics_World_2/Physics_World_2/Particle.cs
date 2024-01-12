using Express.Scene.Objects;
using Express.Scene.Objects.Physical_Properties;
using Microsoft.Xna.Framework;

namespace Physics_World_2;

public class Particle : IParticle, ICoefficientOfRestitution
{
    protected Vector2 _position;
    protected Vector2 _velocity;
    protected float _radius;
    protected float _mass;
    protected float _coefficientOfRestitution;

    public Particle()
    {
        _position = new Vector2();
        _velocity = new Vector2();
        _coefficientOfRestitution = 1;
    }

    public ref Vector2 Position => ref _position;

    public ref Vector2 Velocity => ref _velocity;

    public float Mass { get; set; }
    public float Radius { get; set; }
    public float CoefficientOfRestitution { get; set; }
}