using Microsoft.Xna.Framework;

namespace Express.Scene.Objects;

public interface IVelocity
{
    ref Vector2 Velocity { get; }
}