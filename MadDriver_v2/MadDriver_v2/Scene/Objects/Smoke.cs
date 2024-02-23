using Express.Scene.Objects.Movement;
using Microsoft.Xna.Framework;

namespace MadDriver_v2.Scene.Objects;

public class Smoke : IPosition
{
    protected Vector2 _position = new();

    public ref Vector2 Position => ref _position;
}