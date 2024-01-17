using Artificial_I.Artificial.Spectrum;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace PrimitiveBatchExample;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private PrimitiveBatch _spriteBatch;

    private Line[] _lines;

    private Line _line1;
    private Line _line2;
    private Line _line3;

    private Line _movementLine;
    private bool _isMovingRight = true;
    private const float MovementSpeed = 10f; 
    
    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here
        _line1 = new Line(new Vector2(50, 50), new Vector2(500, 500));
        _line2 = new Line(new Vector2(500, 500), new Vector2(100, 500));
        _line3 = new Line(new Vector2(100, 500), new Vector2(50, 50));

        _lines = new[] { _line1, _line2, _line3 };

        _movementLine = new Line(new Vector2(20, 0), new Vector2(1000, 1000));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new PrimitiveBatch(GraphicsDevice);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        foreach (var line in _lines)
        {
            if (_isMovingRight)
            {
                if (line.EndPoint.X < _movementLine.EndPoint.X)
                {
                    line.StartPoint += Vector2.UnitX * MovementSpeed;
                    line.EndPoint += Vector2.UnitX * MovementSpeed;      
                }
                else
                {
                    _isMovingRight = false;
                    line.StartPoint -= Vector2.UnitX * MovementSpeed;
                    line.EndPoint -= Vector2.UnitX * MovementSpeed;  
                }
            }
            else
            {
                if (line.StartPoint.X > _movementLine.StartPoint.X)
                {
                    line.StartPoint -= Vector2.UnitX * MovementSpeed;
                    line.EndPoint -= Vector2.UnitX * MovementSpeed;      
                }
                else
                {
                    _isMovingRight = true;
                    line.StartPoint += Vector2.UnitX * MovementSpeed;
                    line.EndPoint += Vector2.UnitX * MovementSpeed;  
                }
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();
        
        foreach (var line in _lines)
        {
            _spriteBatch.DrawLine( line.StartPoint, line.EndPoint, Color.OrangeRed );    
        }
        
        _spriteBatch.End();
        base.Draw(gameTime);
    }
}