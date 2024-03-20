namespace Breakout.Scene.Objects;

public class CaughtBall
{
    protected Ball _ball;
    protected float _offset;

    public CaughtBall(Ball theBall, float theOffset)
    {
        _ball = theBall;
        _offset = theOffset;
    }

    public static CaughtBall CreateCaughtBall(Ball theBall, float theOffset)
    {
        return new CaughtBall(theBall, theOffset);
    }

    public Ball Ball => _ball;

    public float Offset => _offset;
}