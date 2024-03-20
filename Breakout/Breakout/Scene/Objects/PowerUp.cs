using Express.Scene;
using Express.Scene.Objects;
using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Movement;
using Microsoft.Xna.Framework;

namespace Breakout.Scene.Objects;

public abstract class PowerUp 
    : IMovable, IAARectangleCollider, ICustomCollider, 
        ICustomUpdate, ILifetime, ISceneUser
{
    protected Vector2 _position;
    protected Vector2 _velocity;
    protected float _width;
    protected float _height;
    protected PowerUpType _type;
    protected double _duration;
    protected bool _active;
    protected Paddle _parent;
    protected Lifetime _lifetime;
    protected IScene _scene;

    public PowerUpType Type => _type;
    
    public ref Vector2 Position => ref _position;

    public ref Vector2 Velocity => ref _velocity;

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
    
    public Lifetime Lifetime
    {
        get => _lifetime;
        set => _lifetime = value;
    }

    public IScene Scene
    {
        get => _scene;
        set => _scene = value;
    }
    
    protected PowerUp(PowerUpType theType, double theDuration = 0)
    {
        _position = new Vector2();
        _velocity = new Vector2();
        _width = 50;
        _height = 50;
        _type = theType;
        _duration = theDuration;
    }
    
    public bool CollidingWith(object item, bool defaultValue = true)
    {
        return item is Paddle;
    }

    public virtual void Activate(Paddle theParent)
    {
        _active = true;
        _parent = theParent;
        if (_duration > 0)
        {
            _lifetime = new Lifetime(0, _duration);
        }

    }

    public virtual void Deactivate()
    {
        _active = false;
    }
  

    public virtual void Update(GameTime gameTime)
    {
        if (_lifetime is not null)
        {
            _lifetime.Update(gameTime);
            if (!_lifetime.IsAlive)
            {
                _lifetime = null;
                this.Deactivate();
            }

        }    
    }
   
    public virtual void AddedToScene(IScene scene)
    {
        // throw new System.NotImplementedException();
    }

    public virtual void RemovedFromScene(IScene scene)
    {
        // throw new System.NotImplementedException();
    }
}