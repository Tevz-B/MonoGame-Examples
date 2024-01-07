using System;
using Express.Physics;
using Express.Physics.Collision;
using friHockey_v4.Scene;
using Microsoft.VisualBasic;
using Microsoft.Xna.Framework;

namespace friHockey_v4.Physics;

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
        float puckSpeed = _level.Puck.Velocity.Length();
        if (puckSpeed != 0)
        {
            float newSpeed = puckSpeed * (1 - Constants.PuckFriction());
            Console.WriteLine($"Puck Speed = {newSpeed}");
            float maxSpeed = Constants.PuckMaximumSpeed();
            if (newSpeed > maxSpeed)
            {
                newSpeed = maxSpeed;
            }

            Console.WriteLine($"Puck Speed (after max clamp) = {newSpeed}");
            _level.Puck.Velocity.Normalize();
            _level.Puck.Velocity *= newSpeed;
            Console.WriteLine($"Puck Velocity (after max clamp) = {_level.Puck.Velocity}");
        }
        MovementPhysics.SimulateMovement(_level.Puck, gameTime.ElapsedGameTime);
        foreach (object item1 in _level.Scene)
        {
            foreach (object item2 in _level.Scene)
            {
                if (item1 != item2)
                {
                    Collision.CollisionBetween(item1, item2);
                }
            }
        }
    }
}