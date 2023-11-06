using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace friHockey_v2.Graphics;

public class Sprite
{
    private Texture2D _texture;
    private Rectangle _sourceRectangle;
    private Vector2 _origin;
    
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