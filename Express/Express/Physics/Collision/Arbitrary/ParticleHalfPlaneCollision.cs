using System.Diagnostics.Metrics;
using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision.Arbitrary;

public class ParticleHalfPlaneCollision : CollisionAlgorithm<IParticleCollider, IHalfPlaneCollider>
{
    private ParticleHalfPlaneCollision() {}

    protected static ParticleHalfPlaneCollision _instance;

    public static ParticleHalfPlaneCollision Instance()
    {
        if (_instance is null)
        {
            _instance = new ParticleHalfPlaneCollision();
        }
        return _instance;
    }
    
    public override void CollisionBetween(IParticleCollider particle, IHalfPlaneCollider halfPlane)
    {
        if (DetectCollision(particle, halfPlane) && ShouldResolveCollision(particle, halfPlane))
        {
            ResolveCollision(particle, halfPlane);
            ReportCollision(particle, halfPlane);
        }
    }
    
    protected override bool DetectCollision(IParticleCollider particle, IHalfPlaneCollider halfPlane)
    {
        float nearPoint = Vector2.Dot(particle.Position, halfPlane.HalfPlane.Normal) - particle.Radius;
        return nearPoint < halfPlane.HalfPlane.Distance;
    }

    protected override void ResolveCollision(IParticleCollider particle, IHalfPlaneCollider halfPlane)
    {
        float nearPoint = Vector2.Dot(particle.Position, halfPlane.HalfPlane.Normal) - particle.Radius;
        float relaxDistance = nearPoint - halfPlane.HalfPlane.Distance;
        
        Vector2 relaxDistanceVector = halfPlane.HalfPlane.Normal * relaxDistance;
        RelaxCollision(particle, halfPlane, relaxDistanceVector);
        
        Vector2 collisionNormal = Vector2.Normalize(relaxDistanceVector);
        Vector2 pointOfImpact = (particle.Position + (collisionNormal * (relaxDistance + particle.Radius)));
        ExchangeEnergy(particle, halfPlane, collisionNormal, pointOfImpact);
    }

}