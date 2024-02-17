using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteClassDemo.Classes;

namespace SpriteClassDemo;

public class SpriteClassDemo : Game
{
    private Texture2D _background;
    private Sprite _playerSprite;
    private Rectangle _playerBounds;
    private Vector2 _center;
    
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public SpriteClassDemo()
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
        _playerSprite = new Sprite();
        const string assetName = "gameSprites";
        Console.WriteLine("%@", assetName);
        _playerSprite.Texture = Content.Load<Texture2D>(assetName);
        _playerSprite.Origin = new Vector2(_playerSprite.Texture.Width / 4f, _playerSprite.Texture.Height / 2f);
        _playerSprite.SourceRectangle = new Rectangle(0, 0, 256, 256);    }

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
        
        _spriteBatch.Draw( _background, new Rectangle(0, 0, (int)_graphics.GraphicsDevice.Viewport.Width, (int)_graphics.GraphicsDevice.Viewport.Height), null, Color.Blue, 0, Vector2.Zero, SpriteEffects.None, 0 );
        //     demonstration of sprite draw (uncomment to view)
        // //     player (whole texture)
        // _spriteBatch.Draw(_playerSprite.Texture, Vector2.Zero, Color.White);
        //
        // //     player (whole texture in the centre - corner)
        // _spriteBatch.Draw(_playerSprite.Texture, _center, Color.White);
        //
        // //     player (whole texture in the centre)
        // _spriteBatch.Draw(_playerSprite.Texture, _center, null, Color.White, 0, new Vector2(256, 128), 1, SpriteEffects.None, 0);
        //
        // //     only the player sprite
        // _spriteBatch.Draw(_playerSprite.Texture, _center, _playerBounds, Color.White, 0, new Vector2(0, 0), 1, SpriteEffects.None, 0);
        //
        // //     player sprite rotated
        // _spriteBatch.Draw(_playerSprite.Texture, _center, _playerBounds, Color.White, (float)Math.PI / 2, Vector2.Zero, 1, SpriteEffects.None, 0);
        //
        // //     player sprite rotated with origin in the centre
        // _spriteBatch.Draw(_playerSprite.Texture, _center, _playerBounds, Color.White, (float)Math.PI / 2, new Vector2(_playerBounds.Width / 2f, _playerBounds.Height / 2f), 1, SpriteEffects.None, 0);
        //
        // //     player sprite mirrored
        // _spriteBatch.Draw( _playerSprite.Texture, _center, _playerBounds, Color.White, 0, new Vector2(_playerBounds.Width / 2f, _playerBounds.Height / 2f), 1, SpriteEffects.FlipHorizontally, 0);
        _spriteBatch.Draw(_playerSprite.Texture, _center, _playerSprite.SourceRectangle, Color.White, 0, _playerSprite.Origin, 1, SpriteEffects.FlipHorizontally, 0);
        
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}