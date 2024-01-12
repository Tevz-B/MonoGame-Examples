using Express.Math;
using Express.Scene.Objects.Colliders;

namespace Physics_World_2;


public class AALimit : IAaHalfPlaneCollider
{
    protected AaHalfPlane _limit;

    public AALimit(AaHalfPlane theLimit)
    {
        _limit = theLimit;
    }

    ref AaHalfPlane IAaHalfPlaneCollider.AaHalfPlane => ref _limit;
}