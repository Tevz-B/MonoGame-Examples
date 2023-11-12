using Express.Scene.Objects;
using friHockey_v3.Scene;
using friHockey_v3.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace friHockey_v3.Graphics;

public class GameRenderer : DrawableGameComponent
{

    private SpriteBatch _spriteBatch;
    private Sprite _malletSprite, _malletShadow;
    private Sprite _puckSprite, _puckShadow;
    private Texture2D _background;
    private Vector2 _lightPosition = new Vector2(320, 460);
    private Level _level;

    public GameRenderer(Game game, Level level)
        : base(game)
    {
        _level = level;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        var textureAtlas = Game.Content.Load<Texture2D>("SceneItems");
        _malletSprite = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(0, 0, 60, 60),
            Origin = new Vector2(30, 30)
        };
        _malletShadow = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(1, 61, 78, 78),
            Origin = new Vector2(39, 39)
        };

        _puckSprite = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(80, 0, 40, 40),
            Origin = new Vector2(20, 20)
        };
        
        _puckShadow = new Sprite
        {
            Texture = textureAtlas,
            SourceRectangle = new Rectangle(81, 41, 48, 48),
            Origin = new Vector2(24, 24)
        };
        _background = Game.Content.Load<Texture2D>("Hockey");
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Yellow);
        _spriteBatch.Begin( SpriteSortMode.BackToFront );

        _spriteBatch.Draw(_background, new Vector2(0, -40), null, Color.White, 0f, Vector2.Zero, 2f, SpriteEffects.None, 0.9f );
        foreach (object item in _level.Scene)
        {
            var itemWithPosition = item as IPosition; 
            Sprite sprite = null;
            Sprite shadowSprite = null;
            var effect = SpriteEffects.None;
            if (item is Mallet)
            {
                sprite = _malletSprite;
                shadowSprite = _malletShadow;
                if(((Mallet)item).Position.Y > 460)
                {
                    effect = SpriteEffects.FlipVertically;
                }
            }
            else if (item is Puck)
            {
                sprite = _puckSprite;
                shadowSprite = _puckShadow;
            }

            if (itemWithPosition is not null)
            {
                if (shadowSprite is not null)
                {
                    Vector2 shadowPosition = ((_lightPosition - itemWithPosition.Position) * -0.04f) + itemWithPosition.Position;
                    _spriteBatch.Draw(shadowSprite.Texture, shadowPosition, shadowSprite.SourceRectangle, Color.White, 0, shadowSprite.Origin, 2, SpriteEffects.None, 0.5f);
                }

                if (sprite is not null)
                {
                    _spriteBatch.Draw(sprite.Texture, itemWithPosition.Position, sprite.SourceRectangle, Color.White, 0, sprite.Origin, 2, effect, 0.1f);
                }
            }
        }
        
        _spriteBatch.End();
    }
}