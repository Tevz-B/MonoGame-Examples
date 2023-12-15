using Express.Scene.Objects.Colliders;
using Microsoft.Xna.Framework;

namespace friHockey_v4.Scene.Objects.Walls;

public class RectangleWall : IAARectangleCollider
{
    private float _width;
    private float _height;
    private Vector2 _position;

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