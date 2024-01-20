using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteClass.Classes;

public class Sprite
{
    protected Texture2D _texture;
    protected Rectangle _sourceRectangle;
    protected Vector2 _origin;
    
    public Texture2D Texture
    {
        get => _texture;
        set => _texture = value;
    }

    public Rectangle SourceRectangle
    {
        get => _sourceRectangle;
        set => _sourceRectangle = value;
    }

    public Vector2 Origin
    {
        get => _origin;
        set => _origin = value;
    }

}