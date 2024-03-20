namespace Breakout.Scene.Objects.PowerUps;

public class Death : PowerUp
{
    public Death()
        : base(PowerUpType.Death)
    {
    }

    public override void Activate(Paddle parent)
    {
        base.Activate(parent);
        foreach (var item in _scene)
        {
            if (item is Ball)
            {
                _scene.Remove(item);
            }
        }
    }
}