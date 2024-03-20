using Express.Math;
using Express.Scene;
using Express.Scene.Objects;
using Express.Scene.Objects.Colliders;

namespace Breakout.Scene.Objects;

public class LevelLimit : IAAHalfPlaneCollider, ICustomCollider, ISceneUser
{
    protected AAHalfPlane _limit;
    protected bool _deadly;
    protected IScene _scene;

    public ref AAHalfPlane AAHalfPlane => ref _limit;
    public HalfPlane HalfPlane => _limit;
    
    IScene ISceneUser.Scene
    {
        get => _scene;
        set => _scene = value;
    }
    
    public LevelLimit(AAHalfPlane limit, bool isDeadly = false)
    {
        _limit = limit;
        _deadly = isDeadly;
    }
    
    public bool CollidingWith(object item, bool defaultValue = true)
    {
        if (_deadly)
        {
            _scene.Remove(item);
        }
        return !_deadly;
    }

    public void AddedToScene(IScene scene)
    {
        // throw new System.NotImplementedException();
    }

    public void RemovedFromScene(IScene scene)
    {
        // throw new System.NotImplementedException();
    }
}