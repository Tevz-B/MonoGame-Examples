using System.Collections;
using friHockey_v1.Interfaces;
using Microsoft.Xna.Framework;

namespace friHockey_v1.Scene.Objects;

public class Puck : IPosition
{
    private Vector2 _position = new();

    public ref Vector2 Position => ref _position;
}