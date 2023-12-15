using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision;

public static class AARectangleAARectangleCollision /*: CollisionAlgorithm*/
{
    public static void CollisionBetween(IAARectangleCollider aaRectangle1, IAARectangleCollider aaRectangle2)
    {
        if (DetectCollision(aaRectangle1, aaRectangle2) && Collision.ShouldResolveCollision(aaRectangle1, aaRectangle2))
        {
            ResolveCollision(aaRectangle1, aaRectangle2);
            Collision.ReportCollision(aaRectangle1, aaRectangle2);
        }
    }
    
    public static bool DetectCollision(IAARectangleCollider aaRectangle1, IAARectangleCollider aaRectangle2)
    {
        float horizontalDistance = System.Math.Abs(aaRectangle1.Position.X - aaRectangle2.Position.X);
        float verticalDistance = System.Math.Abs(aaRectangle1.Position.Y - aaRectangle2.Position.Y);
        return horizontalDistance < aaRectangle1.Width / 2 + aaRectangle2.Width / 2 && verticalDistance < aaRectangle1.Height / 2 + aaRectangle2.Height / 2;
    }

    public static void ResolveCollision(IAARectangleCollider aaRectangle1, IAARectangleCollider aaRectangle2)
    {
        float horizontalDifference = aaRectangle1.Position.X - aaRectangle2.Position.X;
        float horizontalCollidedDistance = System.Math.Abs(horizontalDifference);
        float horizontalMinimumDistance = aaRectangle1.Width / 2 + aaRectangle2.Width / 2;
        float horizontalRelaxDistance = horizontalMinimumDistance - horizontalCollidedDistance;
        float verticalDifference = aaRectangle1.Position.Y - aaRectangle2.Position.Y;
        float verticalCollidedDistance = System.Math.Abs(verticalDifference);
        float verticalMinimumDistance = aaRectangle1.Height / 2 + aaRectangle2.Height / 2;
        float verticalRelaxDistance = verticalMinimumDistance - verticalCollidedDistance;
        Vector2 collisionNormal;
        float relaxDistance;
        if (horizontalRelaxDistance < verticalRelaxDistance)
        {
            relaxDistance = horizontalRelaxDistance;
            collisionNormal = new Vector2(horizontalDifference < 0 ? 1 : -1, 0);
        }
        else
        {
            relaxDistance = verticalRelaxDistance;
            collisionNormal = new Vector2(0, verticalDifference < 0 ? 1 : -1);
        }

        Vector2 relaxDistanceVector = collisionNormal * relaxDistance;
        Collision.RelaxCollision(aaRectangle1, aaRectangle2, relaxDistanceVector);
        Collision.ExchangeEnergy(aaRectangle1, aaRectangle2, collisionNormal);
    }
}