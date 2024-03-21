using Breakout.Scene;
using Breakout.Scene.Objects;
using Express.Physics;
using Express.Physics.Collision;
using Microsoft.Xna.Framework;

namespace Breakout.Physics;

public class PhysicsEngine : GameComponent
{
    protected Level _level;

    public PhysicsEngine(Game game, Level level)
        : base(game)
    {
        _level = level;
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var item in _level.Scene)
        {
            MovementPhysics.SimulateMovement(item, gameTime.ElapsedGameTime);
        }

        foreach (object item in _level.Scene)
        {
            if (item is Ball || item is Paddle)
            {
                foreach (var other in _level.Scene)
                {
                    if (item != other)
                    {
                        Collision.CollisionBetween(item, other);
                    }
                }
            }
        }
    }
}