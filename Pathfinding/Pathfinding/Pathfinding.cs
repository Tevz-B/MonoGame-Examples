using System;
using System.Collections;
using Artificial_I.Artificial.Spectrum;
using Express.Graphics;
using Express.Physics;
using Express.Physics.Collision;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Pathfinding;

public class Pathfinding : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private PrimitiveBatch _primitiveBatch;
    private Texture2D _neoTexture;
    private Texture2D _boulderTexture;
    private AnimatedSprite _walkUp, _walkDown, _walkSide;
    private BasicEffect _gridEffect;
    private Matrix _view;
    private Matrix _projection;
    private TimeInterval _holdDuration;
    private GridScene _scene;
    private Agent _agent;

    public Pathfinding()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
        

    }

    protected override void Initialize()
    {
        _view = Matrix.CreateLookAt(new Vector3(0, 5, 10), Vector3.Zero, Vector3.Up);
        _projection = Matrix.CreatePerspectiveFieldOfView(MathF.PI/4f, this.GraphicsDevice.Viewport.AspectRatio, 1, 100);
        _scene = new GridScene(this);
        this.Components.Add(_scene);
        _agent = new PathfindingAgent();
        _scene.AddItem(_agent);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _primitiveBatch = new PrimitiveBatch(this.GraphicsDevice);
            _spriteBatch = new SpriteBatch(this.GraphicsDevice);
            _neoTexture = this.Content.Load<Texture2D>("Neo");
            _boulderTexture = this.Content.Load<Texture2D>("Boulder");
            float walkCycleDuration = 0.5f;
            _walkUp = new AnimatedSprite(walkCycleDuration);
            _walkUp.Looping = true;
            _walkDown = new AnimatedSprite(walkCycleDuration);
            _walkDown.Looping = true;
            _walkSide = new AnimatedSprite(walkCycleDuration);
            _walkSide.Looping = true;
            int humanWidth = 60;
            int humanHeight = 90;
            Vector2 humanOrigin = new Vector2(humanWidth / 2f, humanHeight - 20);
            for (int i = 0; i < 4; i++)
            {
                Sprite sprite = new Sprite();
                sprite.Texture = _neoTexture;
                sprite.SourceRectangle = new Rectangle(0, i * humanHeight, humanWidth, humanHeight);
                sprite.Origin = humanOrigin;
                _walkSide.AddFrame(AnimatedSpriteFrame.Frame(sprite, (float)i / 4 * walkCycleDuration));
                sprite = new Sprite();
                sprite.Texture = _neoTexture;
                sprite.SourceRectangle = new Rectangle(humanWidth, i * humanHeight, humanWidth, humanHeight);
                sprite.Origin = humanOrigin;
                _walkDown.AddFrame(AnimatedSpriteFrame.Frame(sprite, (float)i / 4 * walkCycleDuration));
                sprite = new Sprite();
                sprite.Texture = _neoTexture;
                sprite.SourceRectangle = new Rectangle(2 * humanWidth, i * humanHeight, humanWidth, humanHeight);
                sprite.Origin = humanOrigin;
                _walkUp.AddFrame(AnimatedSpriteFrame.Frame(sprite, (float)i / 4 * walkCycleDuration));
            }

            _gridEffect = new BasicEffect(this.GraphicsDevice);
            _gridEffect.World = Matrix.CreateRotationX(MathF.PI/2f);
            _gridEffect.View = _view;
            _gridEffect.Projection = _projection;
            DebugRenderer debugRenderer = new DebugRenderer(this, _scene);
            debugRenderer.Effect = _gridEffect;
            debugRenderer.TransformMatrix = _gridEffect.World;
            debugRenderer.ItemColor = Color.Transparent;
            this.Components.Add(debugRenderer);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        var mouseState = Mouse.GetState();
        if (mouseState.LeftButton == ButtonState.Pressed)
        {
            _holdDuration += gameTime.ElapsedGameTime.TotalSeconds;
            if (_holdDuration > 0.25f)
            {
                Vector3 pointOnGround = this.ProjectTouchToGround(mouseState);
                Obstacle obstacle = new Obstacle();
                obstacle.Position.X = (float)MathF.Floor(pointOnGround.X) + 0.5f;
                obstacle.Position.Y = (float)MathF.Floor(pointOnGround.Z) + 0.5f;
                XniPoint gridCoordinate = _scene.CalculateGridCoordinateForItem(obstacle);
                ArrayList existingItems = _scene.GetItemsAt(gridCoordinate);
                if (existingItems.Count == 0 || (existingItems.Count == 1 && existingItems[0] == _agent))
                {
                    _scene.AddItem(obstacle);
                }

            }
            else
            {
                if (mouseState.LeftButton == ButtonState.Released)
                {
                    Vector3 pointOnGround = this.ProjectTouchToGround(mouseState);
                    _agent.GoTo(new Vector2(pointOnGround.X, pointOnGround.Z));
                }

            }
        }
            else
        {
            _holdDuration = 0;
        }

        MovementPhysics.SimulateMovement(_agent, gameTime.ElapsedGameTime);
        ArrayList itemsNearAgent = _scene.GetItemsAroundNeighbourDistance(_scene.CalculateGridCoordinateForItem(_agent), 1);
        foreach (object item in itemsNearAgent)
        {
            if (item != _agent)
            {
                Collision.CollisionBetween(_agent, item);
            }

        }
        _agent.Update(gameTime);

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

            _view.M41 = -_agent.Position.X;
            _primitiveBatch.Begin(null, null, null, _gridEffect);
            for (int i = (int)-_view.Translation.X - 15; i < -_view.Translation.X + 15; i++)
            {
                _primitiveBatch.DrawLine(new Vector2(i, -15), new Vector2(i, 10), Color.Gray);
            }

            for (int i = -15; i < 10; i++)
            {
                _primitiveBatch.DrawLine(new Vector2(-_view.Translation.X - 15, i), new Vector2(-_view.Translation.X + 15, i), Color.Gray);
            }

            foreach (object item in _scene)
            {
                Obstacle obstacle = item is Obstacle ? item as Obstacle : null;
                if (obstacle is not null)
                {
                    _primitiveBatch.DrawLine(new Vector2((float)MathF.Floor(obstacle.Position.X), (float)MathF.Floor(obstacle.Position.Y)), new Vector2((float)MathF.Floor(obstacle.Position.X + 1), (float)MathF.Floor(obstacle.Position.Y + 1)), Color.Black);
                    _primitiveBatch.DrawLine(new Vector2((float)MathF.Floor(obstacle.Position.X + 1), (float)MathF.Floor(obstacle.Position.Y)), new Vector2((float)MathF.Floor(obstacle.Position.X), (float)MathF.Floor(obstacle.Position.Y + 1)), Color.Black);
                }

            }
            Vector2 previous = _agent.Position;
            PathfindingAgent pathfindingAgent = _agent is PathfindingAgent ? _agent as PathfindingAgent : null;
            if (pathfindingAgent is not null)
            {
                int n = pathfindingAgent.Waypoints.Count();
                for (int i = 0; i < n; i++)
                {
                    Vector2 *new = [pathfindingAgent.waypoints objectAtIndex:n-i-1];
                    [primitiveBatch drawLineFrom:previous to:new color:[Color orange]];
                    previous = new;
                }

            }

            _primitiveBatch.End();
            _spriteBatch.Begin();
            Vector3 agentPosition = this.GraphicsDevice.Viewport.Project(new Vector3(_agent.Position.X, 0, _agent.Position.Y), _projection, _view, Matrix.Identity());
            Vector3 agentHead = this.GraphicsDevice.Viewport.Project(new Vector3(_agent.Position.X, 1, _agent.Position.Y), _projection, _view, Matrix.Identity());
            float agentScale = (agentPosition.Y - agentHead.Y) / 90;
            Sprite agentSprite;
            SpriteEffects agentEffects = SpriteEffects.None;
            if (_agent.Velocity.Equals(Vector2.Zero))
            {
                agentSprite = _walkDown.SpriteAtTime(0);
            }
            else if ((float)MathF.Abs(_agent.Velocity.X) > (float)MathF.Abs(_agent.Velocity.Y))
            {
                agentSprite = _walkSide.SpriteAtTime(gameTime.TotalGameTime.TotalSeconds);
                if (_agent.Velocity.X > 0)
                {
                    agentEffects = SpriteEffects.FlipHorizontally;
                }

            }
            else
            {
                if (_agent.Velocity.Y < 0)
                {
                    agentSprite = _walkUp.SpriteAtTime(gameTime.TotalGameTime.TotalSeconds);
                }
                else
                {
                    agentSprite = _walkDown.SpriteAtTime(gameTime.TotalGameTime.TotalSeconds);
                }

            }

            _spriteBatch.Draw(agentSprite.Texture, new Vector2(agentPosition.X, agentPosition.Y), agentSprite.SourceRectangle, Color.White, 0, agentSprite.Origin, agentScale, agentEffects, 0);
            _spriteBatch.End();
        base.Draw(gameTime);
    }
    
    /////////////


        Vector3 ProjectTouchToGround(MouseState mouseState)
        {
            Vector3 rayStart = this.GraphicsDevice.Viewport.Unproject(new Vector3(mouseState.Position.X, mouseState.Position.Y, 0), _projection, _view, Matrix.Identity);
            Vector3 rayEnd = this.GraphicsDevice.Viewport.Unproject(new Vector3(mouseState.Position.X, mouseState.Position.Y, 1), _projection, _view, Matrix.Identity);
            Vector3 ray = rayEnd - rayStart;
            float percentage = rayStart.Y / (float)MathF.Abs(rayEnd.Y - rayStart.Y);
            return rayStart + (ray * percentage);
        }

}