using System;
using Express.Scene.Objects;
using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Physical_Properties;
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
            ParticleParticleCollision.CollisionBetween(item1Particle, item2Particle);
            return;
        }
        else if (item1Particle is not null && item2AAHalfPlane is not null)
        {
            throw new NotImplementedException();
            // ParticleAAHalfPlaneCollision.CollisionBetweenAnd(item1Particle, item2AAHalfPlane);
            // return;
        }
        else if (item1Particle is not null && item2AARectangle is not null)
        {
            ParticleAARectangleCollision.CollisionBetween(item1Particle, item2AARectangle);
            return;
        }
        else if (item1AARectangle is not null && item2AAHalfPlane is not null)
        {
            throw new NotImplementedException();
            // AARectangleAAHalfPlaneCollision.CollisionBetweenAnd(item1AARectangle, item2AAHalfPlane);
            // return;
        }
        else if (item1AARectangle is not null && item2AARectangle is not null)
        {
            AARectangleAARectangleCollision.CollisionBetween(item1AARectangle, item2AARectangle);
            return;
        }

        if (recurse)
        {
            Collision.CollisionBetween(item2, item1, false);
        }
    }
    
    public static bool ShouldResolveCollision(object item1, object item2)
    {
        ICustomCollider customCollider1 = item1 as ICustomCollider;
        ICustomCollider customCollider2 = item2 as ICustomCollider;
        bool result = true;
        if (customCollider1 is not null)
        {
            result &= customCollider1.CollidingWith(item2, result);
        }

        if (customCollider2 is not null)
        {
            result &= customCollider2.CollidingWith(item1, result);
        }

        return result;

    }

    public static void ReportCollision(object item1, object item2)
    {
        ICustomCollider customCollider1 = item1 as ICustomCollider;
        ICustomCollider customCollider2 = item2 as ICustomCollider;
        
        customCollider1?.CollidedWith(item2);
        customCollider2?.CollidedWith(item1);
    }

    public static void RelaxCollision(object item1, object item2, Vector2 relaxDistance)
    {
        float relaxPercentage1 = 0.5f;
        float relaxPercentage2 = 0.5f;
        IMass itemWithMass1 = item1 as IMass;
        IMass itemWithMass2 = item2 as IMass;
        IPosition itemWithPosition1 = item1 as IPosition;
        IPosition itemWithPosition2 = item2 as IPosition;
        if (itemWithMass1 is not null && itemWithMass2 is not null)
        {
            float mass1 = itemWithMass1.Mass;
            float mass2 = itemWithMass2.Mass;
            relaxPercentage1 = mass2 / (mass1 + mass2);
            relaxPercentage2 = mass1 / (mass1 + mass2);
        }
        else if (itemWithMass1 is not null)
        {
            relaxPercentage1 = 1;
            relaxPercentage2 = 0;
        }
        else if (itemWithMass2 is not null)
        {
            relaxPercentage1 = 0;
            relaxPercentage2 = 1;
        }
        else
        {
            if (itemWithPosition1 is not null && itemWithPosition2 is null)
            {
                relaxPercentage1 = 1;
                relaxPercentage2 = 0;
            }
            else if (itemWithPosition1 is null && itemWithPosition2 is not null)
            {
                relaxPercentage1 = 0;
                relaxPercentage2 = 1;
            }

        }

        if (itemWithPosition1 is not null)
        {
            itemWithPosition1.Position -= relaxDistance * relaxPercentage1;
        }

        if (itemWithPosition2 is not null)
        {
            itemWithPosition2.Position += relaxDistance * relaxPercentage2;
        }
    }

    public static void ExchangeEnergy(object item1, object item2, Vector2 collisionNormal)
    {
        IVelocity itemWithVelocity1 = item1 as IVelocity;
        IVelocity itemWithVelocity2 = item2 as IVelocity;
        
        float speed1 = itemWithVelocity1 is not null ? Vector2.Dot(itemWithVelocity1.Velocity, collisionNormal) : 0;
        float speed2 = itemWithVelocity2 is not null ? Vector2.Dot(itemWithVelocity2.Velocity, collisionNormal) : 0;
        
        float speedDifference = speed1 - speed2;
        if (speedDifference < 0)
        {
            return;
        }

        float cor1 = item1 is ICoefficientOfRestitution restitution1 ? restitution1.CoefficientOfRestitution : 1;
        float cor2 = item2 is ICoefficientOfRestitution restitution2 ? restitution2.CoefficientOfRestitution : 1;
        float cor = cor1 * cor2;
        float mass1inverse = item1 is IMass ? 1.0f / ((IMass)item1).Mass : 0;
        float mass2inverse = item2 is IMass ? 1.0f / ((IMass)item2).Mass : 0;
        float impact = -(cor+1) * speedDifference / (mass1inverse + mass2inverse);
        if (mass1inverse > 0 && itemWithVelocity1 is not null)
        {
            itemWithVelocity1.Velocity += collisionNormal * (impact * mass1inverse);
        }

        if (mass2inverse > 0 && itemWithVelocity2 is not null)
        {
            itemWithVelocity2.Velocity -= collisionNormal * (impact * mass2inverse);
        }


    }
}