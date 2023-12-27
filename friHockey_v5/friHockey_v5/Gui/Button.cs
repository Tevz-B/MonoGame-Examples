using Artificial_I.Artificial.Mirage;
using Express.Scene;
using Express.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace friHockey_v5.Gui;

public class Button : ISceneUser
{

    protected IScene _scene;
    protected Image _backgroundImage;
    protected Label _label;
    protected Rectangle _inputArea;
    protected bool _enabled;
    protected bool _isDown;
    protected bool _wasPressed;
    protected bool _wasReleased;
    protected int _pressedId;
    protected Color _labelColor, _labelHoverColor, _backgroundColor, _backgroundHoverColor;

    public Button(Rectangle theInputArea, Texture2D background, SpriteFont font, string text)
    {
        _inputArea = theInputArea;
        _enabled = true;
        _backgroundImage = new Image(background, new Vector2(_inputArea.X, _inputArea.Y));
        _label = new Label(font, text, new Vector2(_inputArea.X + 10, _inputArea.Y + _inputArea.Height / 2f));
        _label.VerticalAlign = VerticalAlign.Middle;
        this.BackgroundColor = Color.White;
        this.BackgroundHoverColor = Color.DimGray;
        this.LabelColor = Color.Black;
        this.LabelHoverColor = Color.White;
    }

    public Rectangle InputArea => _inputArea;

    public bool Enabled
    {
        get => _enabled;
        set => _enabled = value;
    }

    public bool IsDown => _isDown;

    public bool WasPressed => _wasPressed;

    public bool WasReleased => _wasReleased;

    public Image BackgroundImage => _backgroundImage;

    public Label Label => _label;

    public Color LabelColor
    {
        get => _labelColor;
        set
        {
            _labelColor = value;
            _label.Color = _labelColor;
        }
    }

    public Color LabelHoverColor
    {
        get => _labelHoverColor;
        set => _labelHoverColor = value;
    }

    public Color BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            _backgroundColor = value;
            _backgroundImage.Color = _backgroundColor;
        }
    }

    public Color BackgroundHoverColor
    {
        get => _backgroundHoverColor;
        set => _backgroundHoverColor = value;
    }

    public IScene Scene { get; set; }

    public void AddedToScene(IScene theScene)
    {
        // Add child items to scene.
        theScene.Add(_backgroundImage);
        theScene.Add(_label);
    }

    public void RemovedFromScene(IScene theScene)
    {
        // Remove child items.
        theScene.Remove(_backgroundImage);
        theScene.Remove(_label);
    }

    public void UpdateWithInverseView(Matrix inverseView)
    {
        if (!_enabled)
            return;

        if (Mouse.GetState().LeftButton != ButtonState.Pressed)
            return;

        bool wasDown = _isDown;
        _isDown = false;
        _wasPressed = false;
        _wasReleased = false;
        foreach (TouchLocation touch in touches)
        {
            Vector2 touchInScene = Vector2.TransformWith(touch.Position, inverseView);
            if (_inputArea.ContainsVector(touchInScene) && touch.State != TouchLocationStateInvalid)
            {
                if (touch.State == TouchLocationStatePressed)
                {
                    _pressedId = touch.Identifier;
                    _wasPressed = true;
                }

                // Only act to the touch that started the push.
                if (touch.Identifier == _pressedId)
                {
                    if (touch.State == TouchLocationStateReleased)
                    {
                        _wasReleased = true;
                    }
                    else
                    {
                        _isDown = true;
                    }
                }
            }
        }
        if (_isDown && !wasDown)
        {
            _backgroundImage.Color = _backgroundHoverColor;
            _label.Color = _labelHoverColor;
        }
        else if (!_isDown && wasDown)
        {
            _backgroundImage.Color = _backgroundColor;
            _label.Color = _labelColor;
        }
    }
}