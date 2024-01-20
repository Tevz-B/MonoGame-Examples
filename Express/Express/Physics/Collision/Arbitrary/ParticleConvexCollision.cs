using System.Collections.Generic;
using Express.Math;
using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Movement;
using Express.Scene.Objects.Rotation;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision.Arbitrary;

public static class ParticleConvexCollision
{
     public static void CollisionBetween(IParticleCollider particle, IConvexCollider convex)
        {
            if (DetectCollision(particle, convex) && Collision.ShouldResolveCollision(particle, convex))
            {
                ResolveCollision(particle, convex);
                Collision.ReportCollision(particle, convex);
            }
        }

        public static bool DetectCollision(IParticleCollider particle, IConvexCollider convex)
        {
            Vector2 pointOfImpact = new();
            Vector2 relaxDistance = ParticleConvexCollision.CalculateRelaxDistance(particle, convex, ref pointOfImpact);
            return relaxDistance.LengthSquared() > 0;
        }

        public static void ResolveCollision(IParticleCollider particle, IConvexCollider convex)
        {
            Vector2 pointOfImpact = new();
            Vector2 relaxDistance = ParticleConvexCollision.CalculateRelaxDistance(particle, convex, ref pointOfImpact);
            Collision.RelaxCollision(particle, convex, relaxDistance);
            Vector2 collisionNormal = Vector2.Normalize(relaxDistance);
            Collision.ExchangeEnergy(particle, convex, collisionNormal, pointOfImpact);
        }
        
        public static Vector2 CalculateRelaxDistance(IParticleCollider particle, IConvexCollider convex, ref Vector2 pointOfImpact )
        {
            // First move particle in coordinate space of the convex.
            Vector2 offset = convex is IPosition ? ((IPosition)convex).Position : Vector2.Zero;
            float angle = convex is IRotation ? ((IRotation)convex).RotationAngle : 0;
            Matrix transform = Matrix.CreateRotationZ(angle) * (Matrix.CreateTranslation(offset.X, offset.Y, 0));
            Vector2 relativeParticlePosition = Vector2.Transform(particle.Position, Matrix.Invert(transform));
            List<Vector2> vertices = convex.Bounds.Vertices;
            List<HalfPlane> halfPlanes = convex.Bounds.HalfPlanes;
            bool voronoiNearEdge = false;
            float smallestDifference = 0;
            int smallestDifferenceIndex = 0;
            float smallestDistance = 0;
            int smallestDistanceIndex = 0;
            // Calculate overlap with all sides.
            int timesCenterUnderEdge = 0;
            for (int i = 0; i < vertices.Count; i++)
            {
                // Relax distance from the plane
                HalfPlane halfPlane = halfPlanes[i];
                float nearPoint = Vector2.Dot(relativeParticlePosition, halfPlane.Normal) - particle.Radius;
                float relaxDifference = nearPoint - halfPlane.Distance;
                if (relaxDifference > 0) return Vector2.Zero;

                if (i == 0 || relaxDifference > smallestDifference)
                {
                    smallestDifference = relaxDifference;
                    smallestDifferenceIndex = i;
                }

                // Distance to vertex
                float distance = (vertices[i] - relativeParticlePosition).Length();
                if (i == 0 || distance < smallestDistance)
                {
                    smallestDistance = distance;
                    smallestDistanceIndex = i;
                }

                // Are we in the voronoi region of this edge?
                float centerDifference = Vector2.Dot(relativeParticlePosition, halfPlane.Normal) - halfPlane.Distance;
                if (centerDifference > 0)
                {
                    // Center is above edge so see if we're between start and end.
                    Vector2 edge = convex.Bounds.Edges[i];
                    Vector2 edgeNormal = Vector2.Normalize(edge);
                    float start = Vector2.Dot(vertices[i], edgeNormal);
                    float end = Vector2.Dot(edge + vertices[i], edgeNormal);
                    float center = Vector2.Dot(relativeParticlePosition, edgeNormal);
                    if (start < center && center < end)
                    {
                        voronoiNearEdge = true;
                        if (smallestDifferenceIndex == i)
                        {
                            pointOfImpact = vertices[i] + (edge *((center-start) / (end-start)));
                        }
                    }
                }
                else
                {
                    timesCenterUnderEdge++;
                }

            }

            Vector2 relaxDistance;
            // Particle is under all sides.	
            if (voronoiNearEdge || timesCenterUnderEdge == vertices.Count)
            {
                // The edge is closer than the nearest vertex, so just relax in the direction of edge normal
                HalfPlane nearestPlane = halfPlanes[smallestDifferenceIndex];
                relaxDistance = nearestPlane.Normal * smallestDifference;
            }
            else
            {
                // We are in the voronoi region next to nearest vertex.
                Vector2 voronoiVertex = vertices[smallestDistanceIndex];
                Vector2 voronoiNormal = Vector2.Normalize(relativeParticlePosition - voronoiVertex);
                float nearPoint = Vector2.Dot(relativeParticlePosition, voronoiNormal) - particle.Radius;
                float distance = Vector2.Dot(voronoiVertex, voronoiNormal);
                float relaxDifference = nearPoint - distance;
                if (relaxDifference > 0) return Vector2.Zero;

                // Relax in the direction of voronoi vertex
                relaxDistance = voronoiNormal * relaxDifference;
                pointOfImpact = voronoiVertex;
            }

            // Transform result vector back into absolute space.
            pointOfImpact = Vector2.Transform(pointOfImpact, transform);
            return Vector2.TransformNormal(relaxDistance, transform);
        }
}