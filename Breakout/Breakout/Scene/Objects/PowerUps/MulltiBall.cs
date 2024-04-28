using System;

namespace Breakout.Scene.Objects.PowerUps;

public class MultiBall : PowerUp
{
    public MultiBall()
        : base (PowerUpType.MultiBall)
    {
    }

    public override void Activate(Paddle theParent)
    {
        base.Activate(theParent);
        foreach (object item in _scene)
        {
            if (item is Ball ball)
            {
                Ball copy = new Ball
                {
                    Position = ball.Position,
                    BreakthroughPower = ball.BreakthroughPower
                };
                copy.Velocity.X = -ball.Velocity.X;
                copy.Velocity.Y = ball.Velocity.Y;
                _scene.Add(copy);
                
                copy = new Ball
                {
                    Position = ball.Position,
                    BreakthroughPower = ball.BreakthroughPower
                };
                copy.Velocity.X = ball.Velocity.X;
                copy.Velocity.Y = -ball.Velocity.Y;
                _scene.Add(copy);
            }

        }
    }

}