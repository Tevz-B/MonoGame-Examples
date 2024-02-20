using Artificial_I.Artificial.Utils;
using Express.Scene.Objects;
using Express.Scene.Objects.Movement;
using Microsoft.Xna.Framework;

namespace MadDriver_v1.Scene.Objects;

public class Explosion : IPosition, ILifetime
{
    protected Vector2 _position;
    protected Lifetime _lifetime;
    protected int _random;

    public Explosion(GameTime gameTime)
    {
        _position = new Vector2();
        _lifetime = new Lifetime(gameTime.TotalGameTime.TotalSeconds, 1);
        _random = SRandom.Int();
    }

    public int Random => _random;

    public ref Vector2 Position => ref _position;

    public Lifetime Lifetime
    {
        get => _lifetime;
        set => _lifetime = value;
    }
}