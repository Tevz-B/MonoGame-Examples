using Express.Math;
using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace Express.Physics.Collision.AxisAligned;

public class AARectangleAAHalfPlaneCollision : CollisionAlgorithm<IAARectangleCollider, IAAHalfPlaneCollider>
{
    private AARectangleAAHalfPlaneCollision() {}

    private static AARectangleAAHalfPlaneCollision _instance;
    
    public static AARectangleAAHalfPlaneCollision Instance()
    {
        if (_instance is null)
        {
            _instance = new AARectangleAAHalfPlaneCollision();
        }

        return _instance;
    }
    
    public override void CollisionBetween(IAARectangleCollider aaRectangle, IAAHalfPlaneCollider aaHalfPlane)
    {
        if (DetectCollision(aaRectangle, aaHalfPlane) && ShouldResolveCollision(aaRectangle, aaHalfPlane))
        {
            ResolveCollision(aaRectangle, aaHalfPlane);
            ReportCollision(aaRectangle, aaHalfPlane);
        }
    }
    
    protected override bool DetectCollision(IAARectangleCollider aaRectangle, IAAHalfPlaneCollider aaHalfPlane)
    {
        switch (aaHalfPlane.AAHalfPlane.Direction)
        {
        default :
        case AxisDirection.PositiveX :
            return aaRectangle.Position.X - aaRectangle.Width / 2 < aaHalfPlane.AAHalfPlane.Distance;
        case AxisDirection.NegativeX :
            return aaRectangle.Position.X + aaRectangle.Width / 2 > -aaHalfPlane.AAHalfPlane.Distance;
        case AxisDirection.PositiveY :
            return aaRectangle.Position.Y - aaRectangle.Height / 2 < aaHalfPlane.AAHalfPlane.Distance;
        case AxisDirection.NegativeY :
            return aaRectangle.Position.Y + aaRectangle.Height / 2 > -aaHalfPlane.AAHalfPlane.Distance;
        }
        }

    protected override void ResolveCollision(IAARectangleCollider aaRectangle, IAAHalfPlaneCollider aaHalfPlane)
    {
        // RELAXATION STEP
        // First we relax the collision, so the two objects don't collide any more.
        Vector2 relaxDistance;
        switch (aaHalfPlane.AAHalfPlane.Direction)
        {
        case AxisDirection.PositiveX :
            relaxDistance = new Vector2(aaRectangle.Position.X - aaRectangle.Width / 2 - aaHalfPlane.AAHalfPlane.Distance, 0);
            break;
        case AxisDirection.NegativeX :
            relaxDistance = new Vector2(aaRectangle.Position.X + aaRectangle.Width / 2 + aaHalfPlane.AAHalfPlane.Distance, 0);
            break;
        case AxisDirection.PositiveY :
            relaxDistance = new Vector2(0, aaRectangle.Position.Y - aaRectangle.Height / 2 - aaHalfPlane.AAHalfPlane.Distance);
            break;
        case AxisDirection.NegativeY :
            relaxDistance = new Vector2(0, aaRectangle.Position.Y + aaRectangle.Height / 2 + aaHalfPlane.AAHalfPlane.Distance);
            break;
        default: relaxDistance = Vector2.Zero;
            break;
        }

        RelaxCollision(aaRectangle, aaHalfPlane, relaxDistance);
        // ENERGY EXCHANGE STEP
        // In a collision, energy is exchanged only along the collision normal.
        // For particles this is simply the line between both centers.
        Vector2 collisionNormal = Vector2.Normalize(relaxDistance);
        ExchangeEnergy(aaRectangle, aaHalfPlane, collisionNormal);
    }
}