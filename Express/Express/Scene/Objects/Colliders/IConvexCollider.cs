using Express.Math;

namespace Express.Scene.Objects.Colliders;

public interface IConvexCollider
{
    ConvexPolygon Bounds { get; set; }
}