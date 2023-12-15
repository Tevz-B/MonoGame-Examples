using Express.Scene.Objects;
using Microsoft.Xna.Framework;

namespace friHockey_v3.Scene.Objects;

public class Puck : IParticle, ICoefficientOfRestitution
{
    private Vector2 _position;
    private Vector2 _velocity = new Vector2(0, 50);
    private float _mass = 1;
    private float _radius = 20;
    private float _coefficientOfRestitution = Constants.PuckCoefficientOfRestitution();

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

    public float CoefficientOfRestitution
    {
        get => _coefficientOfRestitution;
        set => _coefficientOfRestitution = value;
    }
}