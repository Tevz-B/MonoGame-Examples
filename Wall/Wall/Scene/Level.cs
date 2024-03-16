using System;
using System.Collections;
using Artificial_I.Artificial.Utils;
using Express.Math;
using Express.Scene;
using Microsoft.Xna.Framework;
using Wall.Scene.Objects;

namespace Wall.Scene;

public class Level : GameComponent
{
    protected SimpleScene _scene;
    protected Paddle _paddle;
    protected Ball _ball;
    protected int _bricksCount;
    protected Rectangle _bounds;

    public Level(Game theGame)
        : base(theGame)
    {
        _scene = new SimpleScene(Game);
        Game.Components.Add(_scene);
        _ball = new Ball();
        _paddle = new Paddle();
    }

    public IScene Scene => _scene;

    public Ball Ball => _ball;

    public Paddle Paddle => _paddle;

    public Rectangle Bounds => _bounds;

    public int BricksCount => _bricksCount;
    

    public override void Initialize()
    {
        float aspectRatio = (float)Game.Window.ClientBounds.Width /
                                   Game.Window.ClientBounds.Height;
        
        _bounds = new Rectangle(0, 0, 1000, (int)(1000 / aspectRatio));
        
        _paddle.Position.X = _bounds.Width / 2f;
        _paddle.Position.Y = _bounds.Height - 50;
    }

    public void ResetLevel(float speed)
    {
        _scene.Clear();
        
        _bricksCount = 0;
        _scene.Add(_ball);
        _scene.Add(_paddle);
        
        _scene.Add(new LevelLimit(new AAHalfPlane(AxisDirection.PositiveX, 0)));
        _scene.Add(new LevelLimit(new AAHalfPlane(AxisDirection.NegativeX, -1000)));
        _scene.Add(new LevelLimit(new AAHalfPlane(AxisDirection.PositiveY, 0)));
        for (int i = 0; i < (int)BrickStyle.Last; i++)
        {
            for (int x = i % 2 == 1 ? 0 : 25; x <= _bounds.Width; x += 50)
            {
                Brick brick = new Brick
                {
                    Style = (BrickStyle)i,
                    Position = new Vector2(x, 100 + i * 25)
                };
                
                _scene.Add(brick);
                _bricksCount++;
            }
        }

        ResetBall(speed);
    }

    public void ResetBall(float speed)
    {
        _ball.Position.X = _bounds.Width / 2f;
        _ball.Position.Y = _bounds.Height / 2f;
        _ball.Velocity.X = SRandom.Float(-5, 5);
        _ball.Velocity.Y = speed;
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
}