using System;
using System.Collections.Generic;
using friHockey_v2.Scene.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace friHockey_v2;

public class FriHockey : Game
{

    protected GraphicsDeviceManager _graphics;
    protected Gameplay _currentGameplay;
    protected List<Type> _levelClasses;

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

        _levelClasses = new List<Type> { typeof(HockeyLevel) };
        this.LoadMultiplayerLevel(_levelClasses[0]);
        base.Initialize();
    }

    public void LoadMultiplayerLevel(Type levelClass)
    {
        if (_currentGameplay is not null)
        {
            this.Components.Remove(_currentGameplay);
        }

        _currentGameplay = new Gameplay(this, levelClass);
        this.Components.Add(_currentGameplay);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }
}