using Microsoft.Xna.Framework;

namespace Express.Scene.Objects.Movement;

public interface IVelocity
{
    ref Vector2 Velocity { get; }
}