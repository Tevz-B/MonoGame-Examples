using Express.Math;

namespace Physics_World_2;

public class Wall
{
    protected ConvexPolygon _bounds;

    public Wall(ConvexPolygon bounds)
    {
        _bounds = bounds;
    }
}