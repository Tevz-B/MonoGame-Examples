using System;
using Express.Physics.Collision.Arbitrary;
using Express.Physics.Collision.AxisAligned;
using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Movement;
using Express.Scene.Objects.Physical_Properties;
using Express.Scene.Objects.Rotation;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision;

public static class Collision
{
    
    public static void CollisionBetween(object item1, object item2)
    {
        CollisionBetween(item1, item2, true);
    }

    private static void CollisionBetween(object item1, object item2, bool recurse)
    {
        IParticleCollider item1Particle = item1 as IParticleCollider;
        IAARectangleCollider item1AARectangle = item1 as IAARectangleCollider;
        IAAHalfPlaneCollider item2AAHalfPlane = item2 as IAAHalfPlaneCollider;
        IAARectangleCollider item2AARectangle = item2 as IAARectangleCollider;

        if (item1Particle is not null && item2 is IParticleCollider item2Particle)
        {
            ParticleParticleCollision.Instance().CollisionBetween(item1Particle, item2Particle);
            return;
        }
        else if (item1Particle is not null && item2AAHalfPlane is not null)
        {
            ParticleAAHalfPlaneCollision.Instance().CollisionBetween(item1Particle, item2AAHalfPlane);
            return;
        }
        else if (item1Particle is not null && item2 is IHalfPlaneCollider item2HalfPlane)
        {
            ParticleHalfPlaneCollision.Instance().CollisionBetween(item1Particle, item2HalfPlane);
            return;
        }
        else if (item1Particle is not null && item2AARectangle is not null)
        {
            ParticleAARectangleCollision.Instance().CollisionBetween(item1Particle, item2AARectangle);
            return;
        }
        else if (item1AARectangle is not null && item2AAHalfPlane is not null)
        {
            AARectangleAAHalfPlaneCollision.Instance().CollisionBetween(item1AARectangle, item2AAHalfPlane);
            return;
        }
        else if (item1AARectangle is not null && item2AARectangle is not null)
        {
            AARectangleAARectangleCollision.Instance().CollisionBetween(item1AARectangle, item2AARectangle);
            return;
        }
        else if (item1Particle is not null && item2 is IConvexCollider item2Convex)
        {
            ParticleConvexCollision.Instance().CollisionBetween(item1Particle, item2Convex);
            return;
        }

        if (recurse)
        {
            CollisionBetween(item2, item1, false);
        }
    }
}