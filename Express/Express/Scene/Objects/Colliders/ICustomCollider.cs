namespace Express.Scene.Objects.Colliders;

public interface ICustomCollider
{
    public bool CollidingWith(object item, bool defaultValue = true)
    {
        return defaultValue; // override
    }

    public void CollidedWith(object item)
    {
        //override
    }
}