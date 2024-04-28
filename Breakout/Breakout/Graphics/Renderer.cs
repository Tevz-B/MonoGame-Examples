using Breakout.Scene.Objects;
using Express.Graphics;
using Express.Scene.Objects.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Breakout.Graphics;

public class Renderer : DrawableGameComponent
{
    protected SpriteBatch _spriteBatch;
    protected Texture2D _breakoutTexture;
    protected Texture2D _background;
    protected Sprite _brickSprite, _ballSprite, _lifeSprite;
    protected Sprite _paddleLeftSprite, _paddleMiddleSprite, _paddleRightSprite;
    protected Sprite[] _powerUpSprite = new Sprite[(int)PowerUpType.LastType];
    protected Color[,] _brickColor = new Color[(int)BrickStyle.Last, 2];
    protected Gameplay _gameplay;
    protected Matrix _camera;

    public Renderer(Game game, Gameplay theGameplay)
        : base(game)
    {
        _gameplay = theGameplay;
    }

    public Matrix Camera => _camera;

    public override void Initialize()
    {
        float scaleX = Game.Window.ClientBounds.Width / (float)_gameplay.Level.Bounds.Width;
        float scaleY = Game.Window.ClientBounds.Height / (float)_gameplay.Level.Bounds.Height;
        _camera = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        _breakoutTexture = Game.Content.Load<Texture2D>("breakout");
        _background = Game.Content.Load<Texture2D>("background");
        
        _paddleLeftSprite = new Sprite
        {
            Texture = _breakoutTexture,
            SourceRectangle = new Rectangle(125, 26, 20, 23),
        };
        _paddleMiddleSprite = new Sprite
        {
            Texture = _breakoutTexture,
            SourceRectangle = new Rectangle(145, 26, 57, 23),
        };
        _paddleRightSprite = new Sprite
        {
            Texture = _breakoutTexture,
            SourceRectangle = new Rectangle(202, 26, 20, 23),
        };

        _lifeSprite = new Sprite
        {
            Texture = _breakoutTexture,
            SourceRectangle = new Rectangle(178, 2, 46, 21)
        };

        _ballSprite = new Sprite
        {
            Texture = _breakoutTexture,
            SourceRectangle = new Rectangle(225, 0, 25, 25),
            Origin = new Vector2(12, 13)
        };

        _brickSprite = new Sprite
        {
            Texture = _breakoutTexture,
            SourceRectangle = new Rectangle(125, 0, 50, 25),
            Origin = new Vector2(25, 13)
        };

        _brickColor[(int)BrickStyle.Blue, 0] = Color.Blue;
        _brickColor[(int)BrickStyle.Blue, 1] = Color.LightBlue;
        _brickColor[(int)BrickStyle.Red, 0] = Color.Red;
        _brickColor[(int)BrickStyle.Red, 1] = Color.MistyRose;
        _brickColor[(int)BrickStyle.Magenta, 0] = Color.Purple;
        _brickColor[(int)BrickStyle.Magenta, 1] = Color.Magenta;
        _brickColor[(int)BrickStyle.Green, 0] = Color.Lime;
        _brickColor[(int)BrickStyle.Green, 1] = Color.LightGreen;
        _brickColor[(int)BrickStyle.Yellow, 0] = Color.Yellow;
        _brickColor[(int)BrickStyle.Yellow, 1] = Color.LightYellow;
        _brickColor[(int)BrickStyle.Steel, 0] = Color.DarkGray;
        _brickColor[(int)BrickStyle.Steel, 1] = Color.Gray;

        for (int i = 0; i < (int)PowerUpType.LastType; i++)
        {
            _powerUpSprite[i] = new Sprite();
            _powerUpSprite[i].Texture = _breakoutTexture;
            int x = i % 5;
            int y = i / 5;
            _powerUpSprite[i].SourceRectangle = new Rectangle(50 * x, 50 * (y + 1), 50, 50);
            _powerUpSprite[i].Origin = new Vector2(25, 25);
        }
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.WhiteSmoke);
        
        _spriteBatch.Begin(
            SpriteSortMode.Deferred, null, null, null, null, null, _camera);
        
        // Background
        _spriteBatch.Draw(_background, new Rectangle(0,0, _gameplay.Level.Bounds.Width, _gameplay.Level.Bounds.Height), Color.White);
        
        foreach (object item in _gameplay.Level.Scene)
        {
            Sprite sprite = null;
            Color? color = null;
            
            switch (item)
            {
                case Brick brick:
                    sprite = _brickSprite;
                    color = _brickColor[(int)brick.Style, brick.Power - 1];
                    break;
                case Ball:
                    sprite = _ballSprite;
                    color = Color.Black;
                    break;
                case PowerUp powerUp:
                    sprite = _powerUpSprite[(int)powerUp.Type];
                    color = Color.White;
                    break;
                case Paddle paddle:
                    Rectangle paddleLeftDestination = new Rectangle((int)(paddle.Position.X - paddle.Width / 2), (int)(paddle.Position.Y - paddle.Height / 2), _paddleLeftSprite.SourceRectangle.Width, _paddleLeftSprite.SourceRectangle.Height);
                    _spriteBatch.Draw(_paddleLeftSprite.Texture, paddleLeftDestination, _paddleLeftSprite.SourceRectangle, Color.White);
                    Rectangle paddleRightDestination = new Rectangle((int)(paddle.Position.X + paddle.Width / 2 - _paddleRightSprite.SourceRectangle.Width), (int)(paddle.Position.Y - paddle.Height / 2), _paddleRightSprite.SourceRectangle.Width, _paddleRightSprite.SourceRectangle.Height);
                    _spriteBatch.Draw(_paddleRightSprite.Texture, paddleRightDestination, _paddleRightSprite.SourceRectangle, Color.White);
                    Rectangle paddleMiddleDestination = new Rectangle((int)(paddle.Position.X - paddle.Width / 2 + _paddleLeftSprite.SourceRectangle.Width), (int)(paddle.Position.Y - paddle.Height / 2), (int)(paddle.Width - _paddleLeftSprite.SourceRectangle.Width - _paddleRightSprite.SourceRectangle.Width), _paddleMiddleSprite.SourceRectangle.Height);
                    _spriteBatch.Draw(_paddleMiddleSprite.Texture, paddleMiddleDestination, _paddleMiddleSprite.SourceRectangle, Color.White);
                    
                    break;
                
                default:
                    break;
            }

            if (item is IPosition itemWithPos && sprite is not null && color.HasValue)
            {
                _spriteBatch.Draw(sprite.Texture, itemWithPos.Position, sprite.SourceRectangle, 
                                  color.Value, 0f, sprite.Origin, 1f, SpriteEffects.None, 0);
            }
        }

        for (int i = 0; i < _gameplay.Lives; i++)
        {
            _spriteBatch.Draw(_lifeSprite.Texture,
                new Vector2(_gameplay.Level.Bounds.Width - (i + 1) * 50, 0), _lifeSprite.SourceRectangle,
                Color.Black);
        }

        _spriteBatch.End();
    }
}