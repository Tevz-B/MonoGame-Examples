﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SpriteBatchDemo.Classes;

namespace SpriteBatchDemo;

public class SpriteBatchDemo : Game

{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;

    public SpriteBatchDemo()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        IsFixedTimeStep = false;
        _graphics.SynchronizeWithVerticalRetrace = false;
    }

    protected override void Initialize()
    {
        this.Components.Add( new FpsComponent(this));
        
        // INSTRUCTIONS:
        // Uncomment one of the four tests.
        // Observe console output for FPS display.
        // Click on screen to toggle the mode being used.

        // Test 1: Immediate vs. deferred sprite sort mode.
        // this.Components.Add(new ImmediateVsDeferred(this));
	
        // Test 2: Separate textures vs texture atlas.
        this.Components.Add(new TextureAtlas(this));

        // Test 3: Deferred vs. texture sprite sort mode.
        // this.Components.Add(new TextureSorting(this));

        // Extra test: Test drawing triangles with one or multiple calls.
        // this.Components.Add(new DrawPrimitives(this));

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

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
        GraphicsDevice.Clear(Color.CornflowerBlue);

        base.Draw(gameTime);
    }
}