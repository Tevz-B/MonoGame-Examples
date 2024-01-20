using Microsoft.Xna.Framework;

namespace Express.Scene.Objects.Movement;

public interface IPosition
{
    ref Vector2 Position { get; }
}