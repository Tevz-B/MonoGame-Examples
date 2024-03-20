namespace Breakout.Scene.Objects.PowerUps;

public abstract class ChangeSize : PowerUp
{
    protected float _sizeChange;

    protected ChangeSize(PowerUpType theType, float theSizeChange)
        : base (theType, Constants.ChangeSizeDuration)
    {
        _sizeChange = theSizeChange * Constants.PaddleWidthChange;
    }

    public override void Activate(Paddle theParent)
    {
        base.Activate(theParent);
        if (_parent.Width > Constants.MinimumPaddleWidth && _parent.Width < Constants.MaximumPaddleWidth)
        {
            _parent.Width += _sizeChange;
        }
        else
        {
            _active = false;
        }

    }

    public override void Deactivate()
    {
        if (_active)
        {
            base.Deactivate();
            _parent.Width -= _sizeChange;
        }

    }
}