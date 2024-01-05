using Microsoft.Xna.Framework;

namespace friHockey_v6.Graphics;

public interface IProjector
{
    Vector2 ProjectToWorld(Vector2 screenCoordinate);

}