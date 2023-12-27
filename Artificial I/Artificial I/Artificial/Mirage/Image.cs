using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Artificial_I.Artificial.Mirage;

public class Image
{
    protected Texture2D _texture;
    protected Rectangle _sourceRectangle;
    protected Color _color;
    protected Vector2 _position;
    protected Vector2 _origin;
    protected Vector2 _scale;
    protected float _rotation;
    protected float _layerDepth;

    public Image(Texture2D theTexture, Vector2 thePosition)
    {
        _texture = theTexture;
        _position = thePosition;
        _color = Color.White;
        _origin = Vector2.Zero;
        _scale = Vector2.One;
    }

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

    public Color Color
    {
        get => _color;
        set => _color = value;
    }

    public Vector2 Position
    {
        get => _position;
        set => _position = value;
    }

    public Vector2 Origin
    {
        get => _origin;
        set => _origin = value;
    }

    public Vector2 Scale
    {
        get => _scale;
        set => _scale = value;
    }

    public float Rotation
    {
        get => _rotation;
        set => _rotation = value;
    }

    public float LayerDepth
    {
        get => _layerDepth;
        set => _layerDepth = value;
    }

    public void SetScaleUniform(float value)
    {
        _scale.X = value;
        _scale.Y = value;
    }
}
