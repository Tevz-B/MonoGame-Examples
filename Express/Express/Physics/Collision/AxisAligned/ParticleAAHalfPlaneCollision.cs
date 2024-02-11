using Express.Math;
using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision.AxisAligned;

public class ParticleAAHalfPlaneCollision : CollisionAlgorithm<IParticleCollider, IAAHalfPlaneCollider>
{
    private ParticleAAHalfPlaneCollision() {}

    protected static ParticleAAHalfPlaneCollision _instance;

    public static ParticleAAHalfPlaneCollision Instance()
    {
        if (_instance is null)
        {
            _instance = new ParticleAAHalfPlaneCollision();
        }

        return _instance;
    }
    
    public override void CollisionBetween(IParticleCollider particle, IAAHalfPlaneCollider aaHalfPlane)
    {
        if (DetectCollision(particle, aaHalfPlane) && ShouldResolveCollision(particle, aaHalfPlane))
        {
            ResolveCollision(particle, aaHalfPlane);
            ReportCollision(particle, aaHalfPlane);
        }
    }

    protected override bool DetectCollision(IParticleCollider particle, IAAHalfPlaneCollider aaHalfPlane)
    {
        switch (aaHalfPlane.AAHalfPlane.Direction)
        {
            default:
            case AxisDirection.PositiveX:
                return particle.Position.X - particle.Radius < aaHalfPlane.AAHalfPlane.Distance;
            case AxisDirection.NegativeX:
                return particle.Position.X + particle.Radius > -aaHalfPlane.AAHalfPlane.Distance;
            case AxisDirection.PositiveY:
                return particle.Position.Y - particle.Radius < aaHalfPlane.AAHalfPlane.Distance;
            case AxisDirection.NegativeY:
                return particle.Position.Y + particle.Radius > -aaHalfPlane.AAHalfPlane.Distance;
        }
    }

    protected override void ResolveCollision(IParticleCollider particle, IAAHalfPlaneCollider aaHalfPlane)
    {
        // RELAXATION STEP
        // First we relax the collision, so the two objects don't collide any more.
        Vector2 relaxDistance = Vector2.Zero;
        Vector2 pointOfImpact = Vector2.Zero;
        switch (aaHalfPlane.AAHalfPlane.Direction)
        {
            case AxisDirection.PositiveX:
                relaxDistance = new Vector2(particle.Position.X - particle.Radius - aaHalfPlane.AAHalfPlane.Distance,
                    0);
                pointOfImpact = new Vector2(aaHalfPlane.AAHalfPlane.Distance, particle.Position.Y);
                break;
            case AxisDirection.NegativeX:
                relaxDistance = new Vector2(particle.Position.X + particle.Radius + aaHalfPlane.AAHalfPlane.Distance,
                    0);
                pointOfImpact = new Vector2(-aaHalfPlane.AAHalfPlane.Distance, particle.Position.Y);
                break;
            case AxisDirection.PositiveY:
                relaxDistance = new Vector2(0,
                    particle.Position.Y - particle.Radius - aaHalfPlane.AAHalfPlane.Distance);
                pointOfImpact = new Vector2(particle.Position.X, aaHalfPlane.AAHalfPlane.Distance);
                break;
            case AxisDirection.NegativeY:
                relaxDistance = new Vector2(0,
                    particle.Position.Y + particle.Radius + aaHalfPlane.AAHalfPlane.Distance);
                pointOfImpact = new Vector2(particle.Position.X, -aaHalfPlane.AAHalfPlane.Distance);
                break;
        }

        RelaxCollision(particle, aaHalfPlane, relaxDistance);
        // ENERGY EXCHANGE STEP
        // In a collision, energy is exchanged only along the collision normal.
        // For particles this is simply the line between both centers.
        Vector2 collisionNormal = Vector2.Normalize(relaxDistance);
        ExchangeEnergy(particle, aaHalfPlane, collisionNormal, pointOfImpact);
    }
}