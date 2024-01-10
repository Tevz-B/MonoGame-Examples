using Express.Math;
using Express.Scene.Objects.Colliders;

namespace Physics_World_1;


public class AaLimit : IAaHalfPlaneCollider
{
    protected AaHalfPlane _limit;

    public AaLimit(AaHalfPlane theLimit)
    {
        _limit = theLimit;
    }

    ref AaHalfPlane IAaHalfPlaneCollider.AaHalfPlane => ref _limit;
}