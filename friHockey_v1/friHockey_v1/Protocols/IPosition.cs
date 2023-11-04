using Microsoft.Xna.Framework;

namespace friHockey_v1.Protocols;

public interface IPosition
{
    // We need reference to be able to change X and Y directly
    ref Vector2 Position { get; }
}