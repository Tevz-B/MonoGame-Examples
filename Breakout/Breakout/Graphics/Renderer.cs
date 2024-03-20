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
    protected Sprite _brickSprite, _ballSprite, _paddleSprite, _lifeSprite;
    protected Color[] _brickColor = new Color[(int)BrickStyle.Last];
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
        
        _paddleSprite = new Sprite
        {
            Texture = _breakoutTexture,
            SourceRectangle = new Rectangle(1, 1, 123, 49),
            Origin = new Vector2(61, 29)
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

        _brickColor[(int)BrickStyle.Blue] = Color.Blue;
        _brickColor[(int)BrickStyle.Red] = Color.Red;
        _brickColor[(int)BrickStyle.Magenta] = Color.Magenta;
        _brickColor[(int)BrickStyle.Green] = Color.Lime;
        _brickColor[(int)BrickStyle.Yellow] = Color.Yellow;
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.WhiteSmoke);
        _spriteBatch.Begin(
            SpriteSortMode.Deferred, null, null, null, null, null, _camera);
        foreach (object item in _gameplay.Level.Scene)
        {
            Sprite sprite = null;
            Color? color = null;
            
            switch (item)
            {
                case Brick brick:
                    sprite = _brickSprite;
                    color = _brickColor[(int)brick.Style];
                    break;
                case Ball:
                    sprite = _ballSprite;
                    color = Color.Black;
                    break;
                case Paddle:
                    sprite = _paddleSprite;
                    color = Color.Black;
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