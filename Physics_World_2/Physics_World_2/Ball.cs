using Express.Scene.Objects;
using Express.Scene.Objects.Physical_Properties;
using Microsoft.Xna.Framework;

namespace Physics_World_2;

public class Ball : ICircle, ICoefficientOfRestitution
{
    protected Vector2 _position = new();
    protected Vector2 _velocity = new();

    public Ball()
    {
        CoefficientOfRestitution = 1f;
    }

    public ref Vector2 Position => ref _position;
    public ref Vector2 Velocity => ref _velocity;

    public float Mass { get; set; }
    public float Radius { get; set; }
    public float CoefficientOfRestitution { get; set; }
    public float RotationAngle { get; set; }
    public float AngularVelocity { get; set; }
    public float AngularMass { get; set; }
}