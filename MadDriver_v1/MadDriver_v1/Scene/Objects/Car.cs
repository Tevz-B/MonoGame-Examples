using Microsoft.Xna.Framework;

namespace MadDriver_v1.Scene.Objects;

public class Car
{
    protected CarType _type;
    protected Vector2 _position = new();
    protected Vector2 _velocity = new();
    protected float _damage;

    public CarType Type
    {
        get => _type;
        set => _type = value;
    }

    public float Damage
    {
        get => _damage;
        set => _damage = value;
    }

}