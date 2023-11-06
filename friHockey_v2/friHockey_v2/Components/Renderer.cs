using Express.Scene.Objects;
using friHockey_v2.Graphics;
using friHockey_v2.Scene;
using friHockey_v2.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace friHockey_v2.Components;

public class Renderer : DrawableGameComponent
{

    private Sprite _malletSprite;
    private Sprite _puckSprite;
    private SpriteBatch _spriteBatch;
    private Level _level;

    public Renderer(Game game, Level level)
        : base(game)
    {
        _level = level;
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        
        _malletSprite = new Sprite();
        _malletSprite.Texture = Game.Content.Load<Texture2D>("SceneItems");
        _malletSprite.SourceRectangle = new Rectangle(0, 0, 60, 60);
        _malletSprite.Origin = new Vector2(30, 30);
        
        _puckSprite = new Sprite();
        _puckSprite.Texture = Game.Content.Load<Texture2D>("SceneItems");
        _puckSprite.SourceRectangle = new Rectangle(80, 0, 40, 40);
        _puckSprite.Origin = new Vector2(20, 20);
    }

    public override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        _spriteBatch.Begin();
        foreach (object item in _level.Scene)
        {
            var itemWithPosition = item as IPosition; 
            Sprite sprite = null;
            if (item is Mallet)
            {
                sprite = _malletSprite;
            }
            else if (item is Puck)
            {
                sprite = _puckSprite;
            }

            if (item is IPosition && sprite is not null)
            {
                _spriteBatch.Draw(sprite.Texture, itemWithPosition.Position, sprite.SourceRectangle, Color.White, 0, sprite.Origin, 2, SpriteEffects.None, 0);
            }
        }
        _spriteBatch.End();
    }
}