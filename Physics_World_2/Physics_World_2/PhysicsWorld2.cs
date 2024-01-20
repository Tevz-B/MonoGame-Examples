using System;
using System.Collections.Generic;
using System.Linq;
using Artificial_I.Artificial.Utils;
using Express.Graphics;
using Express.Math;
using Express.Physics;
using Express.Physics.Collision;
using Express.Scene;
using Express.Scene.Objects;
using Express.Scene.Objects.Movement;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Physics_World_2.SceneObjects;

namespace Physics_World_2;

public class PhysicsWorld2 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    
    private SimpleScene _scene;
    private SpriteFont _font;
    private FpsComponent _fps;
    
    private const int SpawnsPerSecond = 10;
    private const double TimePerSpawn = 1.0 / SpawnsPerSecond;
    
    private MouseState _prevState;
    private double _timeSinceLastSpawn;

    public PhysicsWorld2()
    {
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        
        _graphics = new GraphicsDeviceManager(this);
        
        // Set resolution
        _graphics.PreferredBackBufferWidth = 1920;
        _graphics.PreferredBackBufferHeight = 1080;
        _graphics.SynchronizeWithVerticalRetrace = false; // Uncap framerate from refresh rate
        _graphics.ApplyChanges();

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
        AALimit floor = new AALimit(new AAHalfPlane(AxisDirection.NegativeY, -1000));
        _scene.Add(floor);
        AALimit leftWall = new AALimit(new AAHalfPlane(AxisDirection.PositiveX, 50));
        _scene.Add(leftWall);
        AALimit rightWall = new AALimit(new AAHalfPlane(AxisDirection.NegativeX, -1500));
        _scene.Add(rightWall);
        AALimit ceiling = new AALimit(new AAHalfPlane(AxisDirection.PositiveY, 100));
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
        var mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            if (_prevState.LeftButton != ButtonState.Pressed || _timeSinceLastSpawn > TimePerSpawn)
            {
                SpawnBall(mousePositionOnScreen);
            }
            _timeSinceLastSpawn += gameTime.ElapsedGameTime.TotalSeconds;
        }
        else if (mouseState.MiddleButton == ButtonState.Pressed)
        {
            if(_prevState.MiddleButton != ButtonState.Pressed || _timeSinceLastSpawn > TimePerSpawn)
            {
                SpawnParticle(mousePositionOnScreen);
            }
            _timeSinceLastSpawn += gameTime.ElapsedGameTime.TotalSeconds;
        }
        else if (mouseState.RightButton == ButtonState.Pressed)
        {
            if(_prevState.RightButton != ButtonState.Pressed || _timeSinceLastSpawn > TimePerSpawn)
            {
                SpawnAABox(mousePositionOnScreen);
            }
            _timeSinceLastSpawn += gameTime.ElapsedGameTime.TotalSeconds;
        }
        

        _prevState = mouseState;

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

    private void SpawnBall(Vector2 mousePositionOnScreen)
    {
        Ball ball = new Ball();
        ball.Radius = 10 + SRandom.Float() * 50;
        ball.Mass = ball.Radius * ball.Radius * MathF.PI;
        ball.CoefficientOfRestitution = 0.85f;
        ball.RotationAngle = SRandom.Float() * MathF.PI * 2;
        ball.AngularVelocity = (SRandom.Float() - 0.5f) * 5;
        ball.AngularMass = ball.Mass * MathF.Pow(ball.Radius, 2) / 2;
        ball.Position = mousePositionOnScreen;
        _scene.Add(ball);

        _timeSinceLastSpawn = 0;
    }

    private void SpawnParticle(Vector2 mousePositionOnScreen)
    {
        Particle particle = new Particle();
        particle.Radius = 10 + SRandom.Float() * 50;
        particle.Mass = MathF.PI * MathF.Pow(particle.Radius, 2);
        particle.CoefficientOfRestitution = 0.8f;
        particle.Position = mousePositionOnScreen;
        _scene.Add(particle);
        
        _timeSinceLastSpawn = 0;
    }

    private void SpawnAABox(Vector2 mousePositionOnScreen)
    {
        AABox box = new AABox();
        box.Width = 10 + SRandom.Float() * 50;
        box.Height = 10 + SRandom.Float() * 50;
        box.Mass = box.Width * box.Height;
        box.CoefficientOfRestitution = 0.2f;
        box.Position = mousePositionOnScreen;
        _scene.Add(box);
        
        _timeSinceLastSpawn = 0;
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