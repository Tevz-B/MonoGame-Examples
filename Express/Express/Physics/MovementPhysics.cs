using System;
using Express.Scene.Objects;

namespace Express.Physics;

public class MovementPhysics
{
    public static void SimulateMovement(object item, TimeSpan elapsed)
    {
        if (item is IMovable movable)
        {
            movable.Position += movable.Velocity * (float)elapsed.TotalMilliseconds;
        }
    }
}