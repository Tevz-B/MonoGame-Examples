using System;

namespace Express.Physics.Collision;
//
// public abstract class CollisionAlgorithm : ICollisionAlgorithm
// {
//     public void CollisionBetween(object item1, object item2)
//     {
//         // Collision.CollisionBetween(item1, item2, this);
//         Console.WriteLine($"CollisionAlgorithm.CollisionBetween: {item1}, {item2}");
//         if (DetectCollision(item1, item2) && Collision.ShouldResolveCollision(item1, item2))
//         {
//             ResolveCollision(item1, item2);
//             Collision.ReportCollision(item1, item2);
//         }
//     }
//
//     public virtual bool DetectCollision(object item1, object item2)
//     {
//         Console.WriteLine($"CollisionAlgorithm.DetectCollision: No Collision Detection between {item1} and {item2}");
//         return false;
//     }
//
//     public virtual void ResolveCollision(object item1, object item2)
//     {
//         Console.WriteLine($"CollisionAlgorithm.ResolveCollision: No Collision Resolve between {item1} and {item2}");
//     }
// }