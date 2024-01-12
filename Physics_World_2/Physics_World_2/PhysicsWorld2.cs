﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Artificial_I.Artificial.Utils;
using Express.Graphics;
using Express.Math;
using Express.Physics;
using Express.Physics.Collision;
using Express.Scene;
using Express.Scene.Objects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Physics_World_2;

public class PhysicsWorld2 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private SimpleScene _scene;
    private SpriteFont _font;
    private FpsComponent _fps;

    public PhysicsWorld2()
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
        _fps = new FpsComponent(this);
        this.Components.Add(_fps);

    }

    protected override void Initialize()
    {
        // Bounds
        AALimit floor = new AALimit(new AaHalfPlane(AxisDirection.NegativeY, -1000));
        _scene.Add(floor);
        AALimit leftWall = new AALimit(new AaHalfPlane(AxisDirection.PositiveX, 50));
        _scene.Add(leftWall);
        AALimit rightWall = new AALimit(new AaHalfPlane(AxisDirection.NegativeX, -1500));
        _scene.Add(rightWall);
        AALimit ceiling = new AALimit(new AaHalfPlane(AxisDirection.PositiveY, 100));
        _scene.Add(ceiling);
        
        Limit ramp = new Limit(new HalfPlane(new Vector2(-1, -2), -1000));
        _scene.Add(ramp);
        List<Vector2> wallVertices = new List<Vector2>
        {
            new Vector2(50, 700),
            new Vector2(150, 650),
            new Vector2(50, 750)
        };
        Wall wall = new Wall(new ConvexPolygon(wallVertices));
        _scene.Add(wall);
        
        wallVertices.Clear();
        wallVertices.Add(new Vector2(-50, -50));
        wallVertices.Add(new Vector2(50, -50));
        wallVertices.Add(new Vector2(0, 150));
        Obstacle obstacle = new Obstacle(new ConvexPolygon(wallVertices));
        obstacle.Position = new Vector2(400, 400);
        obstacle.RotationAngle = MathF.PI / 2f;
        obstacle.AngularVelocity = MathF.PI;
        obstacle.AngularMass = 50 * 150 * (MathF.Pow(50, 2) + MathF.Pow(150, 2)) / 12;
        _scene.Add(obstacle);
        
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("Retrotype");
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
            Ball ball = new Ball();
            ball.Radius = 10 + SRandom.Float() * 50;
            ball.Mass = ball.Radius * ball.Radius * MathF.PI;
            ball.CoefficientOfRestitution = 0.85f;
            ball.Position = mousePositionOnScreen;
            _scene.Add(ball);
        }
        else if (Mouse.GetState().MiddleButton == ButtonState.Pressed)
        {
            Particle particle = new Particle();
            particle.Radius = 10 + SRandom.Float() * 50;
            particle.Mass = MathF.PI * MathF.Pow(particle.Radius, 2);
            particle.CoefficientOfRestitution = 0.8f;
            particle.Position = mousePositionOnScreen;
            _scene.Add(particle);
        }
        else if (Mouse.GetState().RightButton == ButtonState.Pressed)
        {
            AABox box = new AABox();
            box.Width = 10 + SRandom.Float() * 50;
            box.Height = 10 + SRandom.Float() * 50;
            box.Mass = box.Width * box.Height;
            box.CoefficientOfRestitution = 0.2f;
            box.Position = mousePositionOnScreen;
            _scene.Add(box);
        }

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
        _spriteBatch.Begin();
        _spriteBatch.DrawString(_font, $"Physics World      total objects: {_scene.Count()}    fps: {_fps.FrameRate}", new Vector2(50, 30), Color.Green);
        _spriteBatch.End();
    }
}