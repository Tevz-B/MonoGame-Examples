﻿using System;
using System.Collections.Generic;
using friHockey_v3.Players.AI;
using friHockey_v3.Scene.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace friHockey_v3;

public class FriHockey : Game
{
    protected GraphicsDeviceManager _graphics;
    protected Gameplay _currentGameplay;
    protected List<Type> _levelClasses;
    protected List<Type> _opponentClasses;

    public FriHockey()
    {
        _graphics = new GraphicsDeviceManager(this);
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
        _opponentClasses = new List<Type>{typeof(AIPlayer)};
        // this.LoadMultiplayerLevel(_levelClasses[0]);
        LoadSinglePlayerLevel(_levelClasses[0], _opponentClasses[0]);
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
    
    public void LoadSinglePlayerLevel(Type levelClass, Type opponentClass)
    {
        if (_currentGameplay is not null)
        {
            this.Components.Remove(_currentGameplay);
        }

        _currentGameplay = new Gameplay(this, levelClass, opponentClass);
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