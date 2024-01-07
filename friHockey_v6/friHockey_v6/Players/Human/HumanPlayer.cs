using System;
using friHockey_v6.Graphics;
using friHockey_v6.SceneObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace friHockey_v6.Players.Human;

public class HumanPlayer : Player
{
    private Rectangle _inputArea;
    private bool _grabbed;
    private IProjector _projector;

    public HumanPlayer(Game game, Mallet mallet, PlayerPosition position)
        : base (game, mallet, position)
    {
        _inputArea = new Rectangle( 0, 0, Game.Window.ClientBounds.Width,150);
        if (position == PlayerPosition.Bottom)
        {
            _inputArea.Y = 460 - _inputArea.Height;
        }
    }

    public override void Initialize()
    {
        _projector = Game.Services.GetService<IProjector>();
    }

    public override void Update(GameTime gameTime)
    {
        Vector2 oldPosition = _mallet.Position;
        
        var mousePositionOnScreen = Mouse.GetState().Position.ToVector2();
        var mousePosition = _projector.ProjectToWorld(mousePositionOnScreen);
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