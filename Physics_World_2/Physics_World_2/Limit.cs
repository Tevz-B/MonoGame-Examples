using Express.Math;
using Express.Scene.Objects.Colliders;

namespace Physics_World_2;

public class Limit : IHalfPlaneCollider
{
    protected HalfPlane _limit;

    public Limit(HalfPlane theLimit)
    {
        _limit = theLimit;
    }

    HalfPlane HalfPlane()
    {
        return _limit;
    }

    ref HalfPlane IHalfPlaneCollider.HalfPlane => ref _limit;
}