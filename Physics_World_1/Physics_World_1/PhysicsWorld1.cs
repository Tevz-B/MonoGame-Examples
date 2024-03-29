﻿using System;
using Artificial_I.Artificial.Utils;
using Express.Graphics;
using Express.Math;
using Express.Physics;
using Express.Physics.Collision;
using Express.Scene;
using Express.Scene.Objects.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Physics_World_1;

public class PhysicsWorld1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SimpleScene _scene;
    
    private const int SpawnsPerSecond = 10;
    private const double TimePerSpawn = 1.0 / SpawnsPerSecond;
    
    private ButtonState _prevState = ButtonState.Released;
    private double _timeSinceLastSpawn = 0;

    public PhysicsWorld1()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _graphics = new GraphicsDeviceManager(this);
        // Set resolution
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.ApplyChanges();
        _graphics.SynchronizeWithVerticalRetrace = false; // Uncap framerate from refresh rate

        // _graphics.IsFullScreen = true;
        this.IsFixedTimeStep = false; // Unlock framerate
        _scene = new SimpleScene(this);
        
        this.Components.Add(_scene);
        DebugRenderer renderer = new DebugRenderer(this, _scene);
        renderer.MovementColor = Color.Cyan;
        //		renderer.movementColor = Color.Transparent;
        this.Components.Add(renderer);
        this.Components.Add(new FpsComponent(this));

    }

    protected override void Initialize()
    {
        AALimit floor = new AALimit(new AAHalfPlane(AxisDirection.NegativeY, -1000));
        _scene.Add(floor);
        AALimit leftWall = new AALimit(new AAHalfPlane(AxisDirection.PositiveX, 50));
        _scene.Add(leftWall);
        AALimit rightWall = new AALimit(new AAHalfPlane(AxisDirection.NegativeX, -1500));
        _scene.Add(rightWall);
        AALimit ceiling = new AALimit(new AAHalfPlane(AxisDirection.PositiveY, 100));
        _scene.Add(ceiling);
        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();
        
        // Input
        var mousePositionOnScreen = Mouse.GetState().Position.ToVector2();
        if (Mouse.GetState().LeftButton == ButtonState.Pressed)
        {
            if (_prevState == ButtonState.Released)
            {
                // Spawn ball
                Ball ball = new Ball();
                ball.Radius = 10 + SRandom.Float() * 50;
                ball.Mass = ball.Radius * ball.Radius * MathF.PI;
                ball.CoefficientOfRestitution = 0.85f;
                ball.Position = mousePositionOnScreen;
                _scene.Add(ball);
                
                _timeSinceLastSpawn = 0f;
            }
            else
            {
                if (_timeSinceLastSpawn > TimePerSpawn)
                {
                    // Spawn ball
                    Ball ball = new Ball();
                    ball.Radius = 10 + SRandom.Float() * 50;
                    ball.Mass = ball.Radius * ball.Radius * MathF.PI;
                    ball.CoefficientOfRestitution = 0.85f;
                    ball.Position = mousePositionOnScreen;
                    _scene.Add(ball);

                    _timeSinceLastSpawn = 0f;
                }
                
                // Increment spawn timer
                _timeSinceLastSpawn += gameTime.ElapsedGameTime.TotalSeconds;
                
            }
        }

        _prevState = Mouse.GetState().LeftButton;

        // Physics
        Vector2 gravity = new Vector2(0, 200 * (float)gameTime.ElapsedGameTime.TotalSeconds);
        foreach (object item in _scene)
        {
            MovementPhysics.SimulateMovement(item, gameTime.ElapsedGameTime);
            if (item is IVelocity itemWithVelocity)
            {
                // Simulate gravity.
                itemWithVelocity.Velocity += gravity;
            }

        }
        foreach (object item1 in _scene)
        {
            foreach (object item2 in _scene)
            {
                if (item1 != item2)
                {
                    Collision.CollisionBetween(item1, item2);
                }
            }
        }

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Gray);
        base.Draw(gameTime);
    }
}