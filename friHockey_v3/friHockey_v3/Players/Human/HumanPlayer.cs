using System;
using friHockey_v3.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace friHockey_v3.Players.Human;

public class HumanPlayer : Player
{
    private Rectangle _inputArea;
    private bool _grabbed;
    protected Matrix _inverseView;

    public HumanPlayer(Game game, Mallet mallet, PlayerPosition position)
        : base (game, mallet, position)
    {
        _inputArea = new Rectangle( 0, 0, Game.Window.ClientBounds.Width,150);
        if (position == PlayerPosition.Bottom)
        {
            _inputArea.Y = 460 - _inputArea.Height;
        }
    }

    public void SetCamera(Matrix camera)
    {
        
        Console.WriteLine($"camera {camera}");
        _inverseView = Matrix.Invert(camera);
    }

    public override void Update(GameTime gameTime)
    {
        Vector2 oldPosition = _mallet.Position;
        
        var mousePositionOnScreen = Mouse.GetState().Position.ToVector2();
        var mousePosition = Vector2.Transform(mousePositionOnScreen, _inverseView);
        bool mouseInInputArea = false;
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            if (_inputArea.Contains(mousePosition))
            {
                mouseInInputArea = true;
                if (!_grabbed)
                {
                    float distanceToMallet = (mousePosition - _mallet.Position).Length();
                    if (distanceToMallet < 50)
                    {
                        _grabbed = true;
                    }
                }
        
                if (_grabbed)
                {
                    _mallet.Position = mousePosition;
                }
            }
        }
        else
        {
            _grabbed = false;
        }
        
        if (!mouseInInputArea)
        {
            _grabbed = false;
        }
        Vector2 distance = _mallet.Position - oldPosition;
        if (gameTime.ElapsedGameTime.TotalSeconds > 0f)
        {
            _mallet.Velocity = distance * (1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds);
        }
    }

    public override void Reset()
    {
        _grabbed = false;
    }
}