using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Artificial_I.Artificial.Mirage;
public class Label
{
    protected SpriteFont _font;
    protected string _text;
    protected Color _color;
    protected Vector2 _position;
    protected Vector2 _origin;
    protected Vector2 _scale;
    protected float _rotation;
    protected float _layerDepth;
    protected HorizontalAlign _horizontalAlign;
    protected VerticalAlign _verticalAlign;

    public Label(SpriteFont theFont, string theText, Vector2 thePosition)
    {
        _font = theFont;
        _text = theText;
        _position = thePosition;
        _color = Color.White;
        _origin = Vector2.Zero;
        _scale = Vector2.One;
        UpdateOrigin();
    }

    public SpriteFont Font
    {
        get => _font;
        set
        {
            _font = value;
            UpdateOrigin();
        }
    }

    public string Text
    {
        get => _text;
        set
        {
            _text = value;
            UpdateOrigin();
        }
    }

    public Color Color
    {
        get => _color;
        set => _color = value;
    }

    public ref Vector2 Position => ref _position;

    public Vector2 Origin
    {
        get => _origin;
        set
        {
            _origin = value;
            _horizontalAlign = HorizontalAlign.Custom;
            _verticalAlign = VerticalAlign.Custom;
        }
    }

    public ref Vector2 Scale => ref _scale;

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

    public HorizontalAlign HorizontalAlign
    {
        get => _horizontalAlign;
        set
        {
            _horizontalAlign = value;
            UpdateOrigin();
        }
    }

    public VerticalAlign VerticalAlign
    {
        get => _verticalAlign;
        set
        {
            _verticalAlign = value;
            UpdateOrigin();
        }
    }

    public void SetScaleUniform(float value)
    {
        _scale.X = value;
        _scale.Y = value;
    }

    public void UpdateOrigin()
    {
        Vector2 size = _font.MeasureString(_text);
        switch (_horizontalAlign)
        {
        case HorizontalAlign.Left :
            _origin.X = 0;
            break;
        case HorizontalAlign.Center :
            _origin.X = size.X / 2;
            break;
        case HorizontalAlign.Right :
            _origin.X = size.X;
            break;
        default :
            break;
        }

        switch (_verticalAlign)
        {
        case VerticalAlign.Top :
            _origin.Y = 0;
            break;
        case VerticalAlign.Middle :
            _origin.Y = size.Y / 2;
            break;
        case VerticalAlign.Bottom :
            _origin.Y = size.Y;
            break;
        default :
            break;
        }

    }
}