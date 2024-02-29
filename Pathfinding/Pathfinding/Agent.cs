using Express.Scene.Objects;
using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Composites;
using Microsoft.Xna.Framework;

namespace Pathfinding;

public class Agent : IParticle, ICustomUpdate, ICustomCollider
{
    protected Vector2 _position = new();
    protected Vector2 _velocity = new();
    protected Vector2? _target;
    protected float _mass = 1;
    protected float _radius = 0.2f;

    public Vector2? Target => _target;
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

    public void GoTo(Vector2 theTarget)
    {
        _target = theTarget;
    }

    public virtual void Update(GameTime gameTime)
    {
        if (_target is not null)
        {
            _velocity = _target.Value - _position;
            float length = _velocity.Length();
            if (length > 0.01f)
            {
                _velocity.Normalize();
            }
            else
            {
                _velocity = Vector2.Zero;
                _target = null;
            }

        }

    }

}