using System;
using Microsoft.Xna.Framework;

namespace SpriteBatchDemo.Classes;
public class Item
{
    public Vector2 Position;
    public float Angle;
    public float Speed;
    public Vector2 Velocity;
    public Color Color;
    public int SpriteIndex;
    public int RectangleIndex;

    public Item()
    {
        Position = SRandom.Vector2(320, 480);
        Angle = SRandom.Float(MathF.PI);
        Speed = SRandom.Float(200, 700);
        Velocity = new Vector2((float)Math.Cos(Angle), (float)Math.Sin(Angle)) * Speed;
        Color = Color.Multiply(Color.White, SRandom.Float(1));
        SpriteIndex = SRandom.Int();
        RectangleIndex = SRandom.Int();
    }
}
