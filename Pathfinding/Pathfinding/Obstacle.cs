using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Movement;
using Microsoft.Xna.Framework;

namespace Pathfinding;

public class Obstacle : IAARectangleCollider
{
    private Vector2 _position = new();
    private float _width = 1;
    private float _height = 1;

    public ref Vector2 Position => ref _position;

    public float Width
    {
        get => _width;
        set => _width = value;
    }

    public float Height
    {
        get => _height;
        set => _height = value;
    }
}