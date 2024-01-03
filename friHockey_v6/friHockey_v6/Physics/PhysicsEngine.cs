using Express.Physics;
using Express.Physics.Collision;
using friHockey_v6.Level;
using Microsoft.Xna.Framework;

namespace friHockey_v6.Physics;

public class PhysicsEngine : GameComponent
{
    protected LevelBase _levelBase;

    public PhysicsEngine(Game game, LevelBase levelBase) 
        : base(game)
    {
        _levelBase = levelBase;
    }

    public override void Update(GameTime gameTime)
    {
        float puckSpeed = _levelBase.Puck.Velocity.Length();
        if (puckSpeed != 0)
        {
            float newSpeed = puckSpeed * (1 - Constants.PuckFriction);
            float maxSpeed = Constants.PuckMaximumSpeed;
            if (newSpeed > maxSpeed)
            {
                newSpeed = maxSpeed;
            }

            _levelBase.Puck.Velocity.Normalize();
            _levelBase.Puck.Velocity *= newSpeed;
        }
        MovementPhysics.SimulateMovement(_levelBase.Puck, gameTime.ElapsedGameTime);
        foreach (object item1 in _levelBase.Scene)
        {
            foreach (object item2 in _levelBase.Scene)
            {
                if (item1 != item2)
                {
                    Collision.CollisionBetween(item1, item2);
                }
            }
        }
    }
}