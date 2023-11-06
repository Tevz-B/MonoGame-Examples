using Microsoft.Xna.Framework;

namespace Express.Scene.Objects;

public interface IPosition
{
    ref Vector2 Position { get; }
}