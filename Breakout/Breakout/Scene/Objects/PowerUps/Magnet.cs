namespace Breakout.Scene.Objects.PowerUps;

public class Magnet : PowerUp
{
    public Magnet()
        : base(PowerUpType.Magnet)
    {
    }

    public override void Activate(Paddle parent)
    {
        base.Activate(parent);
        parent.MagnetPower += Constants.MagnetPower;
    }
}