using System;
using System.Collections.Generic;
using Express.Scene;
using Express.Scene.Objects;
using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Breakout.Scene.Objects;

public class Paddle : IAARectangleCollider, ICustomCollider, ISceneUser, ICustomUpdate
{
    protected Vector2 _position = new();
    protected float _width = Constants.InitialPaddleWidth;
    protected float _height = 23;
    protected int _magnetPower = 0;

    protected List<PowerUp> _powerUps = new();
    protected List<CaughtBall> _caughtBalls = new();
    private IScene _scene;

    public ref Vector2 Position => ref _position;

    public float Width
    {
        get => _width;
        set => _width = value;
    }

    public float Height
    {
        get => _height;
        set => _height = value;
    }



    public void AddPowerUp(PowerUp powerUp)
    {
        _powerUps.Add(powerUp);
        powerUp.Activate(this);
    }

    public void RemoveAllPowerUps()
    {
        foreach (var powerUp in _powerUps)
        {
            powerUp.Deactivate();
        }
        _powerUps.Clear();
    }

    public void ReleaseBalls()
    {
        // Pick up power-ups
        if (_magnetPower > 0)
        {
            _magnetPower--;
            _caughtBalls.Clear();
        }
    }
    
    public bool CollidingWith(object item, bool defaultValue = true)
    {
        if (item is PowerUp powerUp)
        {
            AddPowerUp(powerUp);
            _scene.Remove(powerUp);
            return false;
        }
        
        // Bounce off the rest
        return true;
    }

    public void CollidedWith(object item)
    {
        if (item is Ball ball)
        {
            float speed = ball.Velocity.Length() * Constants.BallSpeedUp;

            float offset = ball.Position.X - _position.X;
            float hitPosition = offset / _width * 2;
            
            float angle = hitPosition * Constants.MaximumBallAngle;
            
            ball.Velocity.X = MathF.Sin(angle);
            ball.Velocity.Y = -MathF.Cos(angle);
            ball.Velocity *= speed;

            if (_magnetPower > 0)
            {
                _caughtBalls.Add(new CaughtBall(ball, offset));
            }

        }

    }

    public int MagnetPower
    {
        get => _magnetPower;
        set => _magnetPower = value;
    }

    public IScene Scene
    {
        get => _scene;
        set => _scene = value;
    }

    public void AddedToScene(IScene scene)
    {
        throw new NotImplementedException();
    }

    public void RemovedFromScene(IScene scene)
    {
        throw new NotImplementedException();
    }

    public void Update(GameTime gameTime)
    {
        foreach (var powerUp in _powerUps)
        {
            powerUp.Update(gameTime);
        }

        foreach (CaughtBall ball in _caughtBalls)
        {
            ball.Ball.Position.X = _position.X + ball.Offset;
            ball.Ball.Position.Y = _position.Y - _height / 2f - ball.Ball.Radius;

        }
    }
}