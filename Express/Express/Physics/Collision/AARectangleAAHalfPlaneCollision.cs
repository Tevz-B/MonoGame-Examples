using Express.Math;
using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision;

public class AARectangleAAHalfPlaneCollision
{
    public static void CollisionBetween(IAARectangleCollider aaRectangle, IAaHalfPlaneCollider aaHalfPlane)
    {
        if (DetectCollision(aaRectangle, aaHalfPlane) && Collision.ShouldResolveCollision(aaRectangle, aaHalfPlane))
        {
            ResolveCollision(aaRectangle, aaHalfPlane);
            Collision.ReportCollision(aaRectangle, aaHalfPlane);
        }
    }
    
    static bool DetectCollision(IAARectangleCollider aaRectangle, IAaHalfPlaneCollider aaHalfPlane)
    {
        switch (aaHalfPlane.AaHalfPlane.Direction)
        {
        default :
        case AxisDirection.PositiveX :
            return aaRectangle.Position.X - aaRectangle.Width / 2 < aaHalfPlane.AaHalfPlane.Distance;
        case AxisDirection.NegativeX :
            return aaRectangle.Position.X + aaRectangle.Width / 2 > -aaHalfPlane.AaHalfPlane.Distance;
        case AxisDirection.PositiveY :
            return aaRectangle.Position.Y - aaRectangle.Height / 2 < aaHalfPlane.AaHalfPlane.Distance;
        case AxisDirection.NegativeY :
            return aaRectangle.Position.Y + aaRectangle.Height / 2 > -aaHalfPlane.AaHalfPlane.Distance;
        }

        }

    static void ResolveCollision(IAARectangleCollider aaRectangle, IAaHalfPlaneCollider aaHalfPlane)
    {
        // RELAXATION STEP
        // First we relax the collision, so the two objects don't collide any more.
        Vector2 relaxDistance;
        switch (aaHalfPlane.AaHalfPlane.Direction)
        {
        case AxisDirection.PositiveX :
            relaxDistance = new Vector2(aaRectangle.Position.X - aaRectangle.Width / 2 - aaHalfPlane.AaHalfPlane.Distance, 0);
            break;
        case AxisDirection.NegativeX :
            relaxDistance = new Vector2(aaRectangle.Position.X + aaRectangle.Width / 2 + aaHalfPlane.AaHalfPlane.Distance, 0);
            break;
        case AxisDirection.PositiveY :
            relaxDistance = new Vector2(0, aaRectangle.Position.Y - aaRectangle.Height / 2 - aaHalfPlane.AaHalfPlane.Distance);
            break;
        case AxisDirection.NegativeY :
            relaxDistance = new Vector2(0, aaRectangle.Position.Y + aaRectangle.Height / 2 + aaHalfPlane.AaHalfPlane.Distance);
            break;
        default: relaxDistance = Vector2.Zero;
            break;
        }

        Collision.RelaxCollision(aaRectangle, aaHalfPlane, relaxDistance);
        // ENERGY EXCHANGE STEP
        // In a collision, energy is exchanged only along the collision normal.
        // For particles this is simply the line between both centers.
        Vector2 collisionNormal = Vector2.Normalize(relaxDistance);
        Collision.ExchangeEnergy(aaRectangle, aaHalfPlane, collisionNormal);
    }
}