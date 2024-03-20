using System.Collections;
using Artificial_I.Artificial.Utils;
using Breakout.Scene.Objects;
using Express.Math;
using Express.Scene;
using Microsoft.Xna.Framework;

namespace Breakout.Scene;

public class Level : GameComponent
{
    protected SimpleScene _scene;
    protected Paddle _paddle;
    protected int _bricksCount;
    protected Rectangle _bounds;

    public Level(Game theGame)
        : base(theGame)
    {
        _scene = new SimpleScene(Game);
        _scene.UpdateOrder = 3;
        Game.Components.Add(_scene);

        _scene.ItemAdded += ItemAddedToScene;
        _scene.ItemRemoved += ItemRemovedFromScene;
        
        _paddle = new Paddle();
    }

    public IScene Scene => _scene;

    public Paddle Paddle => _paddle;

    public Rectangle Bounds => _bounds;

    public int BricksCount => _bricksCount;
    

    public override void Initialize()
    {
        float aspectRatio = (float)Game.Window.ClientBounds.Width /
                                   Game.Window.ClientBounds.Height;
        
        _bounds = new Rectangle(0, 0, 1000, (int)(1000 / aspectRatio));
        
        _paddle.Position.X = _bounds.Width / 2f;
        _paddle.Position.Y = _bounds.Height - 70;
    }

    public void ResetLevel(float speed)
    {
        _scene.Clear();
        
        _scene.Add(new LevelLimit(new AAHalfPlane(AxisDirection.PositiveX, _bounds.X)));
        _scene.Add(new LevelLimit(new AAHalfPlane(AxisDirection.NegativeX, -_bounds.Width)));
        _scene.Add(new LevelLimit(new AAHalfPlane(AxisDirection.PositiveY, _bounds.Y)));
        _scene.Add(new LevelLimit(new AAHalfPlane(AxisDirection.NegativeY, -_bounds.Height-50), true));
        
        // Add Paddle
        _scene.Add(_paddle);
        ResetPaddle();
        
        // Add Ball
        AddBall(speed);
        
        // Add Bricks
        for (int i = 0; i < (int)BrickStyle.Last; i++)
        {
            for (int x = 25; x <= _bounds.Width; x += 50)
            {
                Brick brick = new Brick
                {
                    Style = (BrickStyle)i,
                    Position = new Vector2(x, 100 + i * 25)
                };
                if (i == 0)
                {
                    brick.Power = 2;
                }
                
                _scene.Add(brick);
            }
        }
    }

    protected void ResetPaddle()
    {
        _paddle.RemoveAllPowerUps();
        _paddle.MagnetPower = 1;
        _paddle.Width = Constants.InitialPaddleWidth;
    }

    protected void AddBall(float speed)
    {
        Ball ball = new Ball
        {
            Position = new Vector2(
                _paddle.Position.X + SRandom.Float(-5, 5), 
                _paddle.Position.Y - _paddle.Height/2f),
        };
        ball.Velocity.Y = speed;
        _scene.Add(ball);
    }

    public override void Update(GameTime gameTime)
    {
        ArrayList removeBricks = new ArrayList();
        foreach (object item in _scene)
        {
            if (item is Brick brick && brick.Destroyed)
            {
                removeBricks.Add(brick);
            }
        }

        foreach (Brick brick in removeBricks)
        {
            _scene.Remove(brick);
            _bricksCount--;
        }
    }

    protected void ItemAddedToScene(object sender, IScene.SceneEventArgs args)
    {
        
    }
    
    protected void ItemRemovedFromScene(object sender, IScene.SceneEventArgs args)
    {
        
    }
}