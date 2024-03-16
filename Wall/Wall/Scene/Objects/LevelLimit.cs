using Express.Math;
using Express.Scene.Objects.Colliders;

namespace Wall.Scene.Objects;

public class LevelLimit : IAAHalfPlaneCollider
{
    protected AAHalfPlane _limit;

    public ref AAHalfPlane AAHalfPlane => ref _limit;
    public HalfPlane HalfPlane => _limit;
    
    public LevelLimit(AAHalfPlane limit)
    {
        _limit = limit;
    }


}