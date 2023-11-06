using System;
using System.Collections.Generic;
using friHockey_v2.Components;
using friHockey_v2.Scene;
using friHockey_v2.Scene.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace friHockey_v2;

public class FriHockey : Game
{
    private GraphicsDeviceManager _graphics;
    private Renderer _renderer;
    private List<Level> _levels;
    private Level _currentLevel;

    public FriHockey()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Set resolution
        _graphics.PreferredBackBufferWidth = 720;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();
        
        _levels = new List<Level> { new HockeyLevel() };
        var levelClasses = new List<Type> { typeof(HockeyLevel) }; // TODO
        _currentLevel = _levels[0];
        LoadLevel(_currentLevel);

        base.Initialize();
    }
    


    // protected override void LoadContent()
    // {
    //     // _spriteBatch = new SpriteBatch(GraphicsDevice);
    //
    //     // TODO: use this.Content to load your game content here
    // }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }

    // protected override void Draw(GameTime gameTime)
    // {
    //     GraphicsDevice.Clear(Color.CornflowerBlue);
    //
    //     // TODO: Add your drawing code here
    //
    //     base.Draw(gameTime);
    // }
    
    public void LoadLevel(Level level)
    {
        if (_renderer != null)
        {
            Components.Remove(_renderer);
        }

        _renderer = new Renderer(this, level);
        Components.Add(_renderer);
    }
}