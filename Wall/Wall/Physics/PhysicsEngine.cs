using Express.Physics;
using Express.Physics.Collision;
using Microsoft.Xna.Framework;
using Wall.Scene;

namespace Wall.Physics;

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
        MovementPhysics.SimulateMovement(_level.Ball, gameTime.ElapsedGameTime);
        foreach (object item in _level.Scene)
        {
            if (item != _level.Ball)
            {
                Collision.CollisionBetween(_level.Ball, item);
            }
        }
    }

}