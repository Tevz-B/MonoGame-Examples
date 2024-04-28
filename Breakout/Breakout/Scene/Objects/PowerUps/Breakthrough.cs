using Express.Scene;

namespace Breakout.Scene.Objects.PowerUps;

public class Breakthrough : PowerUp
{
    protected IScene _savedScene;

    public Breakthrough()
        : base (PowerUpType.Breakthrough, Constants.BreakthroughDuration)
    {
    }

    public override void Activate(Paddle theParent)
    {
        base.Activate(theParent);
        foreach (object item in _scene)
        {
            if (item is Ball ball)
            {
                ball.BreakthroughPower = true;
            }

        }
        _savedScene = _scene;
    }

    public override void Deactivate()
    {
        base.Deactivate();
        foreach (object item in _savedScene)
        {
            if (item is Ball ball)
            {
                ball.BreakthroughPower = false;
            }

        }
    }

}