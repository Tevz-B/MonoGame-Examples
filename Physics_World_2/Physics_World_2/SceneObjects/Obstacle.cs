using Express.Math;
using Express.Scene.Objects;
using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Movement;
using Express.Scene.Objects.Physical_Properties;
using Express.Scene.Objects.Rotation;
using Microsoft.Xna.Framework;

namespace Physics_World_2.SceneObjects;

public class Obstacle : IConvexCollider, IPosition, IRotatable, IAngularMass
{
    protected ConvexPolygon _bounds;
    protected Vector2 _position;

    public Obstacle(ConvexPolygon bounds)
    {
        _bounds = bounds;
        _position = new Vector2();
    }


    public ConvexPolygon Bounds { get => _bounds; set => _bounds = value; }
    public ref Vector2 Position => ref _position;

    public float RotationAngle { get; set; }
    public float AngularVelocity { get; set; }
    public float AngularMass { get; set; }
}