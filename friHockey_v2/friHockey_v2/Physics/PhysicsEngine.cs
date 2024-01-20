using Express.Physics;
using Express.Physics.Collision;
using Express.Scene.Objects.Colliders;
using friHockey_v2.Scene;
using Microsoft.Xna.Framework;

namespace friHockey_v2.Physics;

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
        MovementPhysics.SimulateMovement(_level.Puck, gameTime.ElapsedGameTime);
        foreach (object item1 in _level.Scene)
        {
            foreach (object item2 in _level.Scene)
            {
                if (item1 != item2)
                {
                    if (item1 is IParticleCollider particleCollider1 && item2 is IParticleCollider particleCollider2)
                    {
                        ParticleParticleCollision.CollisionBetween(particleCollider1, particleCollider2);
                    }
                }

            }
        }
    }
}