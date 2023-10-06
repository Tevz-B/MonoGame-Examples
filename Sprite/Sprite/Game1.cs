using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Sprite;

public class Game1 : Game
{
    private Texture2D background;
    private Texture2D gameSprites;
    private Rectangle playerBounds;
    private Vector2 center;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Add your initialization logic here
        playerBounds = new Rectangle(0, 0, 256, 256);
        center = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2, _graphics.GraphicsDevice.Viewport.Height / 2);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // use this.Content to load your game content here
        background = Content.Load<Texture2D>("background");
        gameSprites = Content.Load<Texture2D>("gameSprites");
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        _spriteBatch.Begin();
        
        _spriteBatch.Draw( background, new Rectangle((int)0, (int)0, (int)_graphics.GraphicsDevice.Viewport.Width, (int)_graphics.GraphicsDevice.Viewport.Height), null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 0 );
        //     demonstration of sprite draw (uncomment to view)
        //     player (whole texture)
        // _spriteBatch.Draw(gameSprites, Vector2.Zero, Color.White);
        
        //     player (whole texture in the centre - corner)
        // _spriteBatch.Draw(gameSprites, center, Color.White);
        
        //     player (whole texture in the centre)
        // _spriteBatch.Draw(gameSprites, center, null, Color.White, 0, new Vector2(256, 128), 1, SpriteEffects.None, 0);
        
        //     only the player sprite
        // _spriteBatch.Draw(gameSprites, center, playerBounds, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

        //     player sprite rotated
        // _spriteBatch.Draw(gameSprites, center, playerBounds, Color.White, (float)Math.PI / 2, Vector2.Zero, 1, SpriteEffects.None, 0);

        //     player sprite rotated with origin in the centre
        // _spriteBatch.Draw(gameSprites, center, playerBounds, Color.White, (float)Math.PI / 2, new Vector2(playerBounds.Width / 2, playerBounds.Height / 2), 1, SpriteEffects.None, 0);

        //     player sprite mirrored
        _spriteBatch.Draw( gameSprites, center, playerBounds, Color.White, 0, new Vector2(playerBounds.Width / 2, playerBounds.Height / 2), 1, SpriteEffects.FlipHorizontally, 0);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}