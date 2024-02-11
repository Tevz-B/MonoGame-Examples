using System.ComponentModel.Design.Serialization;
using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision;

public class ParticleParticleCollision : CollisionAlgorithm<IParticleCollider, IParticleCollider>
{
    private ParticleParticleCollision()
    {
    }

    private static ParticleParticleCollision _instance;

    public static ParticleParticleCollision Instance()
    {
        if (_instance is null)
        {
            _instance = new ParticleParticleCollision();
        }

        return _instance;
    }

    public override void CollisionBetween(IParticleCollider particle1, IParticleCollider particle2)
    {
        if (DetectCollision(particle1, particle2) && ShouldResolveCollision(particle1, particle2))
        {
            ResolveCollision(particle1, particle2);

            ReportCollision(particle1, particle2);
        }
    }

    protected override bool DetectCollision(IParticleCollider particle1, IParticleCollider particle2)
    {
        float distanceBetweenParticles = (particle1.Position - particle2.Position).Length();
        return distanceBetweenParticles < particle1.Radius + particle2.Radius;
    }

    protected override void ResolveCollision(IParticleCollider particle1, IParticleCollider particle2)
    {
        // RELAXATION STEP

        // First we relax the collision, so the two objects don't collide any more.
        // We need to calculate by how much to move them apart. We will move them in the shortest direction
        // possible which is simply the difference between both centers.

        Vector2 positionDifference = particle2.Position - particle1.Position;

        float collidedDistance = positionDifference.Length();
        float minimumDistance = particle1.Radius + particle2.Radius;
        float relaxDistance = minimumDistance - collidedDistance;

        Vector2 collisionNormal = collidedDistance != 0f ? Vector2.Normalize(positionDifference) : Vector2.UnitX;
        Vector2 relaxDistanceVector = collisionNormal * relaxDistance;

        RelaxCollision(particle1, particle2, relaxDistanceVector);

        // ENERGY EXCHANGE STEP

        // In a collision, energy is exchanged only along the collision normal.
        // For particles this is simply the line between both centers.
        ExchangeEnergy(particle1, particle2, collisionNormal);
    }
}