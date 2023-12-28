using System;
using System.Collections.Generic;
using Artificial_I.Artificial.Utils;
using friHockey_v5.Audio;
using friHockey_v5.GameStates.Gameplay;
using friHockey_v5.Players.AI.Opponents;
using friHockey_v5.Scene.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace friHockey_v5;

public class FriHockey : Game
{
    protected GraphicsDeviceManager _graphics;
    protected Gameplay _currentGameplay;
    protected List<Type> _levelClasses;
    protected List<Type> _opponentClasses;

    public FriHockey()
    {
        _graphics = new GraphicsDeviceManager(this);
        
        Components.Add(new FPSComponent(this));
        
        // Init singletons
        SoundEngine.Init(this);
        
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        // Set resolution
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.PreferredBackBufferHeight = 920;
        _graphics.ApplyChanges();

        _levelClasses = new List<Type> { typeof(HockeyLevel) };
        _opponentClasses = new List<Type>{ typeof(Iceman) };
        // this.LoadMultiplayerLevel(_levelClasses[0]);
        LoadSinglePlayerLevel(_levelClasses[0], _opponentClasses[0]);
        base.Initialize();
    }

    public void LoadMultiplayerLevel(Type levelClass)
    {
        if (_currentGameplay is not null)
        {
            Components.Remove(_currentGameplay);
        }

        _currentGameplay = new Gameplay(this, levelClass);
        Components.Add(_currentGameplay);
    }
    
    public void LoadSinglePlayerLevel(Type levelClass, Type opponentClass)
    {
        if (_currentGameplay is not null)
        {
            Components.Remove(_currentGameplay);
        }

        _currentGameplay = new Gameplay(this, levelClass, opponentClass);
        Components.Add(_currentGameplay);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }
}