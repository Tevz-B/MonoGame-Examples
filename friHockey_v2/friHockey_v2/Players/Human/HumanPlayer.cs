using System;
using System.Collections;
using friHockey_v2.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace friHockey_v2.Players.Human;

public class HumanPlayer : Player
{
    private Rectangle _inputArea;
    private bool _grabbed;
    private Vector2 _touchOffset;

    public HumanPlayer(Mallet mallet, ArrayList scene, PlayerPosition position, Game game)
        : base (mallet, scene, position)
    {
        _inputArea = game.Window.ClientBounds;
        _inputArea.Height = 300;
        if (position == PlayerPosition.Bottom)
        {
            _inputArea.Y = game.Window.ClientBounds.Height - _inputArea.Height;
        }

        _touchOffset = new Vector2(0, position == PlayerPosition.Top ? 80 : -80);
    }

    public override void Update(GameTime gameTime)
    {
        TouchCollection touches = TouchPanel.GetState();
        Vector2 oldPosition = _mallet.Position;
        _mallet.Position = Mouse.GetState().Position.ToVector2();

        Vector2 distance = _mallet.Position - oldPosition;
        if (gameTime.ElapsedGameTime.Seconds > 0)
        {
            _mallet.Velocity = distance * (1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        Console.WriteLine("{0}", _mallet.Velocity);
    }
    
    
    
    // public override void Update(GameTime gameTime)
    // {
    //     TouchCollection touches = TouchPanel.GetState();
    //     Vector2 oldPosition = _mallet.Position;
    //     bool touchesInInputArea = false;
    //     foreach (TouchLocation touch in touches)
    //     {
    //         if (_inputArea.Contains(touch.Position))
    //         {
    //             touchesInInputArea = true;
    //             if (!_grabbed)
    //             {
    //                 float distanceToMallet = (touch.Position - _mallet.Position + _touchOffset).Length();
    //                 if (distanceToMallet < 100)
    //                 {
    //                     _grabbed = true;
    //                 }
    //
    //             }
    //
    //             if (_grabbed)
    //             {
    //                 _mallet.Position = touch.Position + _touchOffset;
    //             }
    //         }
    //     }
    //     if (!touchesInInputArea)
    //     {
    //         _grabbed = false;
    //     }
    //
    //     Vector2 distance = _mallet.Position - oldPosition;
    //     if (gameTime.ElapsedGameTime.Seconds > 0)
    //     {
    //         _mallet.Velocity = distance * (1.0f / (float)gameTime.ElapsedGameTime.TotalSeconds);
    //     }
    //
    //     Console.WriteLine("{0}", _mallet.Velocity);
    // }
}