namespace Express.Graphics;

public class AnimatedSpriteFrame
{
    protected Sprite _sprite;
    protected double _start;

    public AnimatedSpriteFrame(Sprite sprite, double start)
    {
        _sprite = sprite;
        _start = start;
    }

    public static AnimatedSpriteFrame Frame(Sprite sprite, double start)
    {
        return new AnimatedSpriteFrame(sprite, start);
    }

    public Sprite Sprite => _sprite;

    public double Start => _start;
}