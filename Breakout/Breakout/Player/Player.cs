using Breakout.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout.Player;

public class Player : GameComponent
{
    protected Paddle _paddle;
    protected Matrix _inverseView;

    public Player(Game game, Paddle paddle)
        : base(game)
    {
        _paddle = paddle;
    }

    public void SetCamera(Matrix camera)
    {
        _inverseView = Matrix.Invert(camera);
    }

    public override void Update(GameTime gameTime)
    {
        //// Touch version 
        // TouchCollection touches = TouchPanel.GetState();
        // if (touches.Count() == 1)
        // {
        //     TouchLocation touch = touches[0];
        //     Vector2 touchInScene = Vector2.Transform(touch.Position, _inverseView);
        //     _paddle.Position.X = touchInScene.X;
        // }

        Vector2 mouseInScene = Vector2.Transform(Mouse.GetState().Position.ToVector2(), _inverseView);
        _paddle.Position.X = mouseInScene.X;
    }
}