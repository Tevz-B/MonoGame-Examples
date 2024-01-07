using System;
using System.Collections;
using friHockey_v2.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace friHockey_v2.Players.Human;

public class HumanPlayer : Player
{
    private Rectangle _inputArea;
    private bool _grabbed;

    public HumanPlayer(Mallet mallet, ArrayList scene, PlayerPosition position, Game game)
        : base (mallet, scene, position)
    {
        _inputArea = game.GraphicsDevice.Viewport.Bounds;
        _inputArea.Height = 300;
        if (position == PlayerPosition.Bottom)
        {
            _inputArea.Y = game.Window.ClientBounds.Height - _inputArea.Height;
        }
    }

    public override void Update(GameTime gameTime)
    {
        Vector2 oldPosition = _mallet.Position;
        
        var mousePosition = Mouse.GetState().Position.ToVector2();;
        bool mouseInInputArea = false;
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            if (_inputArea.Contains(mousePosition))
            {
                mouseInInputArea = true;
                if (!_grabbed)
                {
                    float distanceToMallet = (mousePosition - _mallet.Position).Length();
                    if (distanceToMallet < 100)
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

        Console.WriteLine("{0}", _mallet.Velocity);
    }
}