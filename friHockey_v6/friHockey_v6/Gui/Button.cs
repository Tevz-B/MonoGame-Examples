using Artificial_I.Artificial.Mirage;
using Express.Scene;
using Express.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace friHockey_v6.Gui;

public class Button : ISceneUser
{

    protected Image _backgroundImage;
    protected Label _label;
    protected Rectangle _inputArea;
    protected bool _enabled;
    protected bool _isDown;
    protected bool _wasPressed;
    protected bool _wasReleased;
    protected Color _labelColor, _labelHoverColor, _labelPressedColor, _backgroundColor, _backgroundHoverColor, _backgroundPressedColor;

    public Button(Rectangle theInputArea, Texture2D background, SpriteFont font, string text)
    {
        _inputArea = theInputArea;
        _enabled = true;
        _backgroundImage = new Image(background, new Vector2(_inputArea.X, _inputArea.Y));
        _label = new Label(font, text, new Vector2(_inputArea.X + 10, _inputArea.Y + _inputArea.Height / 2f));
        _label.VerticalAlign = VerticalAlign.Middle;
        BackgroundColor = Color.White;
        BackgroundHoverColor = Color.White;
        BackgroundPressedColor = Color.DimGray;
        LabelColor = Color.Black;
        LabelHoverColor = Color.Gray;
        LabelPressedColor = Color.White;
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

    public ref Image BackgroundImage => ref _backgroundImage;

    public ref Label Label => ref _label;

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
    public Color LabelPressedColor
    {
        get => _labelPressedColor;
        set => _labelPressedColor = value;
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
    public Color BackgroundPressedColor
    {
        get => _backgroundPressedColor;
        set => _backgroundPressedColor = value;
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

        bool wasDown = _isDown;
        _isDown = false;
        _wasPressed = false;
        _wasReleased = false;
        
        var mousePositionOnScreen = Mouse.GetState().Position.ToVector2();
        var mousePositionInScene = Vector2.Transform(mousePositionOnScreen, inverseView);

        if (_inputArea.Contains(mousePositionInScene))
        {
            if (wasDown)
            {
                // release pressed button -> trigger action
                if (Mouse.GetState().LeftButton != ButtonState.Pressed)
                {
                    _wasReleased = true;
                    _backgroundImage.Color = _backgroundColor;
                    _label.Color = _labelColor;
                }
                // holding pressed button
                else
                {
                    _isDown = true;
                    _wasPressed = true;
                    _backgroundImage.Color = _backgroundPressedColor;
                    _label.Color = _labelPressedColor;
                }
            }
            else
            {
                // click on button
                if (Mouse.GetState().LeftButton == ButtonState.Pressed)
                {
                    _isDown = true;
                    _wasPressed = true;
                    _backgroundImage.Color = _backgroundPressedColor;
                    _label.Color = _labelPressedColor;
                }
                // hover over button
                else
                {
                    _backgroundImage.Color = _backgroundHoverColor;
                    _label.Color = _labelHoverColor;
                }
            }
        }
        // mouse not over button
        else
        {
            _backgroundImage.Color = _backgroundColor;
            _label.Color = _labelColor;
        }
    }
}