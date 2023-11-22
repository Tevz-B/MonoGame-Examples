using Express.Math;

namespace Express.Scene.Objects.Colliders;

public interface IHalfPlaneCollider
{
    ref HalfPlane HalfPlane { get; }
}