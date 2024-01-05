using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision;

public static class AaRectangleAaRectangleCollision
{
    public static void CollisionBetween(IAARectangleCollider iaaRectangle1, IAARectangleCollider iaaRectangle2)
    {
        if (DetectCollision(iaaRectangle1, iaaRectangle2) && Collision.ShouldResolveCollision(iaaRectangle1, iaaRectangle2))
        {
            ResolveCollision(iaaRectangle1, iaaRectangle2);
            Collision.ReportCollision(iaaRectangle1, iaaRectangle2);
        }
    }
    
    public static bool DetectCollision(IAARectangleCollider iaaRectangle1, IAARectangleCollider iaaRectangle2)
    {
        float horizontalDistance = System.Math.Abs(iaaRectangle1.Position.X - iaaRectangle2.Position.X);
        float verticalDistance = System.Math.Abs(iaaRectangle1.Position.Y - iaaRectangle2.Position.Y);
        return horizontalDistance < iaaRectangle1.Width / 2 + iaaRectangle2.Width / 2 && verticalDistance < iaaRectangle1.Height / 2 + iaaRectangle2.Height / 2;
    }

    public static void ResolveCollision(IAARectangleCollider iaaRectangle1, IAARectangleCollider iaaRectangle2)
    {
        float horizontalDifference = iaaRectangle1.Position.X - iaaRectangle2.Position.X;
        float horizontalCollidedDistance = System.Math.Abs(horizontalDifference);
        float horizontalMinimumDistance = iaaRectangle1.Width / 2 + iaaRectangle2.Width / 2;
        float horizontalRelaxDistance = horizontalMinimumDistance - horizontalCollidedDistance;
        float verticalDifference = iaaRectangle1.Position.Y - iaaRectangle2.Position.Y;
        float verticalCollidedDistance = System.Math.Abs(verticalDifference);
        float verticalMinimumDistance = iaaRectangle1.Height / 2 + iaaRectangle2.Height / 2;
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
        Collision.RelaxCollision(iaaRectangle1, iaaRectangle2, relaxDistanceVector);
        Collision.ExchangeEnergy(iaaRectangle1, iaaRectangle2, collisionNormal);
    }
}