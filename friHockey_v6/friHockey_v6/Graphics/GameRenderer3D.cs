using System;
using Express.Scene.Objects.Movement;
using friHockey_v6.Level;
using friHockey_v6.SceneObjects;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace friHockey_v6.Graphics;

public class GameRenderer3D : DrawableGameComponent, IProjector
{
    // Resources
    protected ContentManager _content;
    protected Model _malletModel;
    protected Model _puckModel;
    protected Model _levelModel;
    protected Vector3 _lightPosition;
    // Graphics objects
    protected Matrix _view, _projection;
    // Level
    protected LevelBase _level;

    public GameRenderer3D(Game game, LevelBase theLevel)
        : base(game)
    {
        _level = theLevel;
        // _content = new ContentManager(Game.Services);
        _content = Game.Content;
        _lightPosition = new Vector3(4.6f, -5f, -3.2f);
        
        if (Game.Services.GetService<IProjector>() is null)
        {
            Game.Services.AddService<IProjector>(this);    
        }
        
        GraphicsDevice.DeviceReset += CalculateCamera;
    }

    public Vector2 ProjectToWorld(Vector2 screenCoordinate)
    {
        Vector3 rayStart = GraphicsDevice.Viewport.Unproject(new Vector3(screenCoordinate.X, screenCoordinate.Y, 0), _projection, _view, Matrix.Identity);
        Vector3 rayEnd = GraphicsDevice.Viewport.Unproject(new Vector3(screenCoordinate.X, screenCoordinate.Y, 1), _projection, _view, Matrix.Identity);
        Vector3 ray = rayEnd - rayStart;
        float percentage = rayStart.Y / MathF.Abs(rayEnd.Y - rayStart.Y);
        Vector3 ground = rayStart + (ray * percentage);
        return new Vector2(-ground.Z / 0.02f, ground.X / 0.02f);
    }

    public Vector3 ProjectToRenderFromWorld(Vector2 worldCoordinate)
    {
        return new Vector3(worldCoordinate.Y * 0.02f, 0, -worldCoordinate.X * 0.02f);
    }

    protected override void LoadContent()
    {
        _malletModel = _content.Load<Model>("mallet");
        _puckModel = _content.Load<Model>("puck");
        _levelModel = _content.Load<Model>("hockey");
        ApplyLightingOnModel(_malletModel);
        ApplyLightingOnModel(_puckModel);
        ApplyLightingOnModel(_levelModel);
        this.CalculateCamera();
        GraphicsDevice.DeviceReset += CalculateCamera;
    }

    public void CalculateCamera(object sender, EventArgs e)
    {
        CalculateCamera();
    }

    public void CalculateCamera()
    {
        _projection = Matrix.CreatePerspectiveFieldOfView(MathF.PI * 0.13f, GraphicsDevice.Viewport.AspectRatio, 1, 100);
        if (GraphicsDevice.Viewport.Width < GraphicsDevice.Viewport.Height) // right one
        // if (GraphicsDevice.Viewport.Width > GraphicsDevice.Viewport.Height)
        {
            _view = Matrix.CreateLookAt(new Vector3(4.6f, 22f, -3.2f), new Vector3(4.6f, 0f, -3.2f), Vector3.Left);
        }
        else
        {
            _view = Matrix.CreateLookAt(new Vector3(16f, 6f, -3.2f), new Vector3(5.6f, 0, -3.2f), Vector3.Left);
        }
    }

    public void ApplyLightingOnModel(Model model)
    {
        BasicEffect effect = (BasicEffect)model.Meshes[0].Effects[0];
        effect.LightingEnabled = true;
        effect.AmbientLightColor = new Vector3( 0.55f, 0.5f, 0.45f );
        // ?
        // effect.AmbientColor.Set(effect.DiffuseColor);
        
        effect.DirectionalLight0.Enabled = true;

        effect.DirectionalLight0.DiffuseColor = new Vector3(1f, 0.9f, 0.8f);
        effect.DirectionalLight0.SpecularColor = new Vector3(1f, 0.9f, 0.8f);
        effect.DirectionalLight0.Direction = new Vector3(0, -1, 0);
        
        effect.EnableDefaultLighting();
    }

    public void UpdateLightDirection(Model model, Vector2 position)
    {
        BasicEffect effect = (BasicEffect)model.Meshes[0].Effects[0];
        Vector3 direction = ProjectToRenderFromWorld(position) - _lightPosition;
        direction.Normalize();
        effect.DirectionalLight0.Direction = direction;
    }

    public override void Draw(GameTime gameTime)
    {
        // glEnable(GL_NORMALIZE);
        GraphicsDevice.SamplerStates[0] = SamplerState.LinearWrap;
        GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
        GraphicsDevice.DepthStencilState = DepthStencilState.Default;
        _levelModel.Draw(Matrix.CreateTranslation(4.6f, 0f, -3.2f), _view, _projection);
        foreach (object item in _level.Scene)
        {
            IPosition itemWithPosition = item as IPosition;
            Model model = null;
            if (item is Mallet)
            {
                model = _malletModel;
            }
            else if (item is Puck)
            {
                model = _puckModel;
            }

            if (itemWithPosition is not null && model is not null)
            {
                UpdateLightDirection(model, itemWithPosition.Position);
                model.Draw(
                    Matrix.CreateTranslation(ProjectToRenderFromWorld(itemWithPosition.Position)), _view,
                    _projection);
            }
        }
    }
}