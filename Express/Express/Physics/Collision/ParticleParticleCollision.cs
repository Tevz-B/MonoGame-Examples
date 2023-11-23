using Express.Scene.Objects;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision;

public static class ParticleParticleCollision
{
    public static void CollisionBetweenAnd(IParticleCollider particle1, IParticleCollider particle2)
        {
            if (ParticleParticleCollision.DetectCollisionBetweenAnd(particle1, particle2))
            {
                ParticleParticleCollision.ResolveCollisionBetweenAnd(particle1, particle2);
            }

        }

        public static bool DetectCollisionBetweenAnd(IParticleCollider particle1, IParticleCollider particle2)
        {
            float distanceBetweenParticles = (particle1.Position - particle2.Position).Length();
            return distanceBetweenParticles < particle1.Radius + particle2.Radius;
        }

        public static void ResolveCollisionBetweenAnd(IParticleCollider particle1, IParticleCollider particle2)
        {
            Vector2 collisionDifference = particle2.Position - particle1.Position;
            float collidedDistance = collisionDifference.Length();
            float minimumDistance = particle1.Radius + particle2.Radius;
            float relaxDistance = minimumDistance - collidedDistance;
            float relaxPercentage1 = 0.5f;
            float relaxPercentage2 = 0.5f;
            float mass1 = particle1 is IMass __tmp1 ? __tmp1.Mass : 0f;
            float mass2 = particle2 is IMass __tmp2 ? __tmp2.Mass : 0f;
            if (mass1 != 0 && mass2 != 0)
            {
                relaxPercentage1 = mass2 / (mass1 + mass2);
                relaxPercentage2 = mass1 / (mass1 + mass2);
            }
            else if (mass1 == 0)
            {
                relaxPercentage1 = 0;
                relaxPercentage2 = 1;
            }
            else
            {
                relaxPercentage1 = 1;
                relaxPercentage2 = 0;
            }

            Vector2 collisionNormal = Vector2.Normalize(collisionDifference);
            particle1.Position -= collisionNormal * (relaxPercentage1 * relaxDistance);
            particle2.Position += collisionNormal * (relaxPercentage2 * relaxDistance);
            IVelocity particleWithVelocity1 = particle1 as IVelocity;
            IVelocity particleWithVelocity2 = particle2 as IVelocity;
            Vector2? velocity1 = particleWithVelocity1?.Velocity;
            Vector2? velocity2 = particleWithVelocity2?.Velocity;
            float speed1 = velocity1.HasValue ? Vector2.Dot(velocity1.Value, collisionNormal) : 0;
            float speed2 = velocity2.HasValue ? Vector2.Dot(velocity2.Value, collisionNormal) : 0;
            float speedDifference = speed1 - speed2;
            
            float cor1 = particle1 is ICoefficientOfRestitution __tmp3 ? __tmp3.CoefficientOfRestitution : 1;
            float cor2 = particle2 is ICoefficientOfRestitution __tmp4 ? __tmp4.CoefficientOfRestitution : 1;
            float cor = cor1 * cor2;
            float mass1Inverse = mass1 > 0 ? 1.0f / mass1 : 0;
            float mass2Inverse = mass2 > 0 ? 1.0f / mass2 : 0;
            float impact = -(cor+1) * speedDifference / (mass1Inverse + mass2Inverse);
            if (mass1 > 0 && particleWithVelocity1 is not null)
            {
                particleWithVelocity1.Velocity += (collisionNormal * (impact / mass1));
            }

            if (mass2 > 0 && particleWithVelocity2 is not null)
            {
                particleWithVelocity2.Velocity -= (collisionNormal * (impact / mass2));
            }
            
        }

}