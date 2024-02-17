using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpriteDrawDemo;

public class SpriteDrawDemo : Game
{
    private Texture2D _background;
    private Texture2D _gameSprites;
    private Rectangle _playerBounds;
    private Vector2 _center;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public SpriteDrawDemo()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Add your initialization logic here
        _playerBounds = new Rectangle(0, 0, 256, 256);
        _center = new Vector2(_graphics.GraphicsDevice.Viewport.Width / 2f, _graphics.GraphicsDevice.Viewport.Height / 2f);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // use this.Content to load your game content here
        _background = Content.Load<Texture2D>("background");
        _gameSprites = Content.Load<Texture2D>("gameSprites");
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
        
        // _spriteBatch.Draw( _background, new Rectangle(0, 0, _graphics.GraphicsDevice.Viewport.Width, _graphics.GraphicsDevice.Viewport.Height), null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 0 );
        //     demonstration of sprite draw (uncomment to view)
        //     player (whole texture)
        // _spriteBatch.Draw(_gameSprites, Vector2.Zero, Color.White);
        
        //     player (whole texture in the centre - corner)
        // _spriteBatch.Draw(_gameSprites, _center, Color.White);
        
        //     player (whole texture in the centre)
        // _spriteBatch.Draw(_gameSprites, _center, null, Color.White, 0, new Vector2(256, 128), 1, SpriteEffects.None, 0);
        
        //     only the player sprite
        // _spriteBatch.Draw(_gameSprites, _center, _playerBounds, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);

        //     player sprite rotated
        // _spriteBatch.Draw(_gameSprites, _center, _playerBounds, Color.White, (float)Math.PI / 2, Vector2.Zero, 1, SpriteEffects.None, 0);

        //     player sprite rotated with origin in the centre
        _spriteBatch.Draw(_gameSprites, _center, _playerBounds, Color.White, (float)Math.PI / 2, new Vector2(_playerBounds.Width / 2f, _playerBounds.Height / 2f), 1, SpriteEffects.None, 0);

        //     player sprite mirrored
        // _spriteBatch.Draw( _gameSprites, _center, _playerBounds, Color.White, 0, new Vector2(_playerBounds.Width / 2f, _playerBounds.Height / 2f), 1, SpriteEffects.FlipHorizontally, 0);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}