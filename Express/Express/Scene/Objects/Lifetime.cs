using Microsoft.Xna.Framework;

namespace Express.Scene.Objects;

public class Lifetime
{
    private double _start;
    private double _duration;
    private double _progress;

    public double Progress => _progress;

    public Lifetime(double start, double duration)
    {
        _start = start;
        _duration = duration;
    }

    public void Update(GameTime gameTime)
    {
        if (IsAlive)
        {
            _progress += gameTime.ElapsedGameTime.TotalSeconds;
            if (!IsAlive)
            {
                _progress = _duration;
            }
        }
    }

    public bool IsAlive => _progress < _duration;
    public float Percentage => (float) (_progress / _duration);
}