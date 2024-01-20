using Express.Math;
using Express.Scene.Objects.Colliders;

namespace Physics_World_2;


public class AALimit : IAAHalfPlaneCollider
{
    protected AAHalfPlane _limit;

    public AALimit(AAHalfPlane theLimit)
    {
        _limit = theLimit;
    }

    ref AAHalfPlane IAAHalfPlaneCollider.AAHalfPlane => ref _limit;
}