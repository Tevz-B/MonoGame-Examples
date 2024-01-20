using Express.Math;
using Express.Scene.Objects.Colliders;

namespace Physics_World_2.SceneObjects;

public class Wall : IConvexCollider
{
    protected ConvexPolygon _bounds;

    public ConvexPolygon Bounds
    {
        get => _bounds;
        set => _bounds = value;
    }

    public Wall(ConvexPolygon bounds)
    {
        _bounds = bounds;
    }
}