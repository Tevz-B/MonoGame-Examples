using Express.Math;
using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision;

public static class ParticleAaHalfPlaneCollision
{
    public static void CollisionBetween(IParticleCollider particle, IAaHalfPlaneCollider aaHalfPlane)
    {
        if (DetectCollision(particle, aaHalfPlane) && Collision.ShouldResolveCollision(particle, aaHalfPlane))
        {
            ResolveCollision(particle, aaHalfPlane);
            Collision.ReportCollision(particle, aaHalfPlane);
        }
    }
    
    static bool DetectCollision(IParticleCollider particle, IAaHalfPlaneCollider aaHalfPlane)
        {
            switch (aaHalfPlane.AaHalfPlane.Direction)
            {
            default :
            case AxisDirection.PositiveX :
                return particle.Position.X - particle.Radius < aaHalfPlane.AaHalfPlane.Distance;
            case AxisDirection.NegativeX :
                return particle.Position.X + particle.Radius > -aaHalfPlane.AaHalfPlane.Distance;
            case AxisDirection.PositiveY :
                return particle.Position.Y - particle.Radius < aaHalfPlane.AaHalfPlane.Distance;
            case AxisDirection.NegativeY :
                return particle.Position.Y + particle.Radius > -aaHalfPlane.AaHalfPlane.Distance;
            }

        }

        static void ResolveCollision(IParticleCollider particle, IAaHalfPlaneCollider aaHalfPlane)
        {
            // RELAXATION STEP
            // First we relax the collision, so the two objects don't collide any more.
            Vector2 relaxDistance = Vector2.Zero;
            Vector2 pointOfImpact = Vector2.Zero;
            switch (aaHalfPlane.AaHalfPlane.Direction)
            {
            case AxisDirection.PositiveX :
                relaxDistance = new Vector2(particle.Position.X - particle.Radius - aaHalfPlane.AaHalfPlane.Distance, 0);
                pointOfImpact = new Vector2(aaHalfPlane.AaHalfPlane.Distance, particle.Position.Y);
                break;
            case AxisDirection.NegativeX :
                relaxDistance = new Vector2(particle.Position.X + particle.Radius + aaHalfPlane.AaHalfPlane.Distance, 0);
                pointOfImpact = new Vector2(-aaHalfPlane.AaHalfPlane.Distance, particle.Position.Y);
                break;
            case AxisDirection.PositiveY :
                relaxDistance = new Vector2(0, particle.Position.Y - particle.Radius - aaHalfPlane.AaHalfPlane.Distance);
                pointOfImpact = new Vector2(particle.Position.X, aaHalfPlane.AaHalfPlane.Distance);
                break;
            case AxisDirection.NegativeY :
                relaxDistance = new Vector2(0, particle.Position.Y + particle.Radius + aaHalfPlane.AaHalfPlane.Distance);
                pointOfImpact = new Vector2(particle.Position.X, -aaHalfPlane.AaHalfPlane.Distance);
                break;
            }

            Collision.RelaxCollision(particle, aaHalfPlane, relaxDistance);
            // ENERGY EXCHANGE STEP
            // In a collision, energy is exchanged only along the collision normal.
            // For particles this is simply the line between both centers.
            Vector2 collisionNormal = Vector2.Normalize(relaxDistance);
            Collision.ExchangeEnergy(particle, aaHalfPlane, collisionNormal, pointOfImpact);
        }
}