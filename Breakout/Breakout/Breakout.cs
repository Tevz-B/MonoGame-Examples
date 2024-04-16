﻿using Artificial_I.Artificial.Utils;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Breakout;

// TODO:
// doesnt end level (win condition),
// sometimes stuck on big paddle,
// speed is sometimes weird


public class Breakout : Game
{
    private GraphicsDeviceManager _graphics;

    public Breakout()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        Components.Add(new Gameplay(this));
        Components.Add(new FpsComponent(this));
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }
}