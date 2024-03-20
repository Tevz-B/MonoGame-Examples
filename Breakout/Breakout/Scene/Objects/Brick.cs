using System;
using Artificial_I.Artificial.Utils;
using Express.Scene;
using Express.Scene.Objects;
using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Breakout.Scene.Objects;

public enum BrickStyle
{
    Blue = 0,
    Red,
    Magenta,
    Green,
    Yellow,
    Last
}

public class Brick : IAARectangleCollider, ICustomCollider, ISceneUser
{
    protected BrickStyle _style;
    protected Vector2 _position = new();
    protected float _width = 50;
    protected float _height = 24;
    protected int _power = 1;
    protected bool _destroyed;
    protected IScene _scene;

    public int Power
    {
        get => _power;
        set => _power = value;
    }

    public BrickStyle Style
    {
        get => _style;
        set => _style = value;
    }
    
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

    public bool Destroyed => _destroyed;

    public bool CollidingWith(object item, bool defaultValue = true)
    {
        if (item is not Ball)
        {
            return false;
        }

        var ball = (Ball)item;
        _power--;
        if (_power == 0)
        {
            _scene.Remove(this);
            
            // Create power up randomly, but only if ball is not in breakthrough mode.
            if (ball.BreakthroughPower == 0 && SRandom.Float() < Constants.PowerUpChance)
            {
                PowerUp powerUp = PowerUpFactory.CreateRandomPowerUp();
                powerUp.Position = _position;
                powerUp.Velocity.Y = Constants.PowerUpSpeed;
                _scene.Add(powerUp);
            }
        }
        
        return true;
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
}