using System;
using Express.Scene.Objects;
using Microsoft.Xna.Framework;

namespace friHockey_v4.Scene.Objects;

public class Mallet : IParticle, ICustomUpdate
{
    private Vector2 _position;
    private Vector2 _velocity;
    private Vector2 _previousPosition;
    private float _mass = 20;
    private float _radius = 30;
    
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

    public void Update(GameTime gameTime)
    {
        var dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        if (dt == 0) return;
        
        Vector2 distance = _position - _previousPosition;
        Vector2 newVelocity = distance * (1.0f / dt);
        float s = Constants.VelocitySmoothing();
        _velocity = (_velocity * s) + (newVelocity * (1 - s));

        _previousPosition = _position;
    }

    public void ResetVelocity()
    {
        _velocity = Vector2.Zero;
        _previousPosition = _position;
    }
}