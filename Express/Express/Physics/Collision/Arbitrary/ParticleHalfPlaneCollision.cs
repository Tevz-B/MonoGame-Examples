using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision.Arbitrary;

public class ParticleHalfPlaneCollision
{
    public static void CollisionBetween(IParticleCollider particle, IHalfPlaneCollider halfPlane)
    {
        if (DetectCollision(particle, halfPlane) && Collision.ShouldResolveCollision(particle, halfPlane))
        {
            ResolveCollision(particle, halfPlane);
            Collision.ReportCollision(particle, halfPlane);
        }
    }
    
    static bool DetectCollision(IParticleCollider particle, IHalfPlaneCollider halfPlane)
    {
        float nearPoint = Vector2.Dot(particle.Position, halfPlane.HalfPlane.Normal) - particle.Radius;
        return nearPoint < halfPlane.HalfPlane.Distance;
    }

    static void ResolveCollision(IParticleCollider particle, IHalfPlaneCollider halfPlane)
    {
        float nearPoint = Vector2.Dot(particle.Position, halfPlane.HalfPlane.Normal) - particle.Radius;
        float relaxDistance = nearPoint - halfPlane.HalfPlane.Distance;
        
        Vector2 relaxDistanceVector = halfPlane.HalfPlane.Normal * relaxDistance;
        Collision.RelaxCollision(particle, halfPlane, relaxDistanceVector);
        
        Vector2 collisionNormal = Vector2.Normalize(relaxDistanceVector);
        Vector2 pointOfImpact = (particle.Position + (collisionNormal * (relaxDistance + particle.Radius)));
        Collision.ExchangeEnergy(particle, halfPlane, collisionNormal, pointOfImpact);
    }

}