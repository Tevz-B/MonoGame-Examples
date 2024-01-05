using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision;

public static  class ParticleAaRectangleCollision
{
    public static void CollisionBetween(IParticleCollider particle, IAARectangleCollider iaaRectangle)
    {
        if (DetectCollision(particle, iaaRectangle) && Collision.ShouldResolveCollision(particle, iaaRectangle))
        {
            ResolveCollision(particle, iaaRectangle);
            Collision.ReportCollision(particle, iaaRectangle);
        }
    }
    
    public static bool DetectCollision(IParticleCollider particle, IAARectangleCollider iaaRectangle)
    {
        Vector2 relaxDistance = CalculateRelaxDistance(particle, iaaRectangle);
        return relaxDistance.LengthSquared() > 0;
    }

    public static void ResolveCollision(IParticleCollider particle, IAARectangleCollider iaaRectangle)
    {
        Vector2 relaxDistance = CalculateRelaxDistance(particle, iaaRectangle);
        Collision.RelaxCollision(particle, iaaRectangle, relaxDistance);
        Vector2 collisionNormal = Vector2.Normalize(relaxDistance);
        Collision.ExchangeEnergy(particle, iaaRectangle, collisionNormal);
    }

    public static Vector2 CalculateRelaxDistance(IParticleCollider particle, IAARectangleCollider iaaRectangle)
    {
        Vector2 relaxDistance = Vector2.Zero;
        Vector2 nearestVertex = iaaRectangle.Position;
        float halfWidth = iaaRectangle.Width / 2;
        float halfHeight = iaaRectangle.Height / 2;
        float leftDifference = (iaaRectangle.Position.X - halfWidth) - (particle.Position.X + particle.Radius);
        if (leftDifference > 0) return relaxDistance;

        float rightDifference = (particle.Position.X - particle.Radius) - (iaaRectangle.Position.X + halfWidth);
        if (rightDifference > 0) return relaxDistance;

        float topDifference = (iaaRectangle.Position.Y - halfHeight) - (particle.Position.Y + particle.Radius);
        if (topDifference > 0) return relaxDistance;

        float bottomDifference = (particle.Position.Y - particle.Radius) - (iaaRectangle.Position.Y + halfHeight);
        if (bottomDifference > 0) return relaxDistance;

        bool horizontallyInside = false;
        bool verticallyInside = false;
        if (particle.Position.X < iaaRectangle.Position.X - halfWidth)
        {
            nearestVertex.X -= halfWidth;
        }
        else if (particle.Position.X > iaaRectangle.Position.X + halfWidth)
        {
            nearestVertex.X += halfWidth;
        }
        else
        {
            horizontallyInside = true;
        }

        if (particle.Position.Y < iaaRectangle.Position.Y - halfHeight)
        {
            nearestVertex.Y -= halfHeight;
        }
        else if (particle.Position.Y > iaaRectangle.Position.Y + halfHeight)
        {
            nearestVertex.Y += halfHeight;
        }
        else
        {
            verticallyInside = true;
        }

        if (!horizontallyInside && !verticallyInside)
        {
            Vector2 particleVertex = nearestVertex - particle.Position;
            float vertexDistance = particleVertex.Length();
            if (vertexDistance > particle.Radius)
            {
                return relaxDistance;
            }
            else
            {
                return Vector2.Normalize( particleVertex ) * (particle.Radius - vertexDistance);
            }

        }

        if (leftDifference > rightDifference)
        {
            relaxDistance.X = -leftDifference;
        }
        else
        {
            relaxDistance.X = rightDifference;
        }

        if (topDifference > bottomDifference)
        {
            relaxDistance.Y = -topDifference;
        }
        else
        {
            relaxDistance.Y = bottomDifference;
        }
        
        if (System.Math.Abs(relaxDistance.X) < System.Math.Abs(relaxDistance.Y))
        {
            relaxDistance.Y = 0;
        }
        else
        {
            relaxDistance.X = 0;
        }

        return relaxDistance;
    }
}