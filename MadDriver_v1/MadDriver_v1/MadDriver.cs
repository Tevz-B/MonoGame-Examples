using System;
using System.Collections;
using System.Collections.Generic;
using MadDriver_v1.Graphics;
using MadDriver_v1.Scene;
using MadDriver_v1.Scene.Levels;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MadDriver_v1;

public class MadDriver : Game
{
    private GraphicsDeviceManager _graphics;
    
    protected List<LevelType> _levelTypes;
    protected Level _currentLevel;
    protected GameRenderer _currentRenderer;
    protected Director _currentDirector;

    public MadDriver()
    {
        _graphics = new GraphicsDeviceManager(this);
        _graphics.PreferredBackBufferHeight = 920;
        _graphics.PreferredBackBufferWidth = 640;
        
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        _levelTypes = new List<LevelType>();
        _levelTypes.Add(LevelType._Intro);
        _levelTypes.Add(LevelType.Suburbs);
        _levelTypes.Add(LevelType.City);
        this.LoadLevel(_levelTypes[0]);
        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();


        
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
    }
    
    
    protected void LoadLevel(LevelType levelType)
    {
        if (_currentLevel is not null)
        {
            this.Components.Remove(_currentLevel);
        }

        _currentLevel = LevelCreator.Create(levelType, this);
        this.Components.Add(_currentLevel);
        if (_currentDirector is not null)
        {
            this.Components.Remove(_currentDirector);
        }

        _currentDirector = new Director(this);
        this.Components.Add(_currentDirector);
        if (_currentRenderer is not null)
        {
            this.Components.Remove(_currentRenderer);
        }

        _currentRenderer = new GameRenderer(this, _currentLevel, _currentDirector.Camera);
        this.Components.Add(_currentRenderer);
    }
}
