using Artificial_I.Artificial.Spectrum;
using Express.Scene;
using Express.Scene.Objects;
using Express.Scene.Objects.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace friHockey_v3.Graphics;

public class DebugRenderer : DrawableGameComponent
{
    protected IScene _scene;
    protected PrimitiveBatch _primitiveBatch;
    protected Color _itemColor;
    protected Color _movementColor;
    protected Color _colliderColor;
    protected BlendState _blendState;
    protected DepthStencilState _depthStencilState;
    protected RasterizerState _rasterizerState;
    protected Effect _effect;
    protected Matrix _transformMatrix;

    public DebugRenderer(Game theGame, IScene theScene)
        : base (theGame)
    {
        _scene = theScene;
        this.ItemColor = Color.White;
        this.MovementColor = Color.SkyBlue;
        this.ColliderColor = Color.Lime;
        _transformMatrix = Matrix.Identity;
    }

    public Color ItemColor
    {
        get => _itemColor;
        set => _itemColor = value;
    }

    public Color MovementColor
    {
        get => _movementColor;
        set => _movementColor = value;
    }

    public Color ColliderColor
    {
        get => _colliderColor;
        set => _colliderColor = value;
    }

    public BlendState BlendState
    {
        get => _blendState;
        set => _blendState = value;
    }

    public DepthStencilState DepthStencilState
    {
        get => _depthStencilState;
        set => _depthStencilState = value;
    }

    public RasterizerState RasterizerState
    {
        get => _rasterizerState;
        set => _rasterizerState = value;
    }

    public Effect Effect
    {
        get => _effect;
        set => _effect = value;
    }

    public Matrix TransformMatrix
    {
        get => _transformMatrix;
        set => _transformMatrix = value;
    }

    protected override void LoadContent()
    {
        _primitiveBatch = new PrimitiveBatch(this.GraphicsDevice);
    }

    public override void Draw(GameTime gameTime)
    {
        Matrix transformInverse = Matrix.Invert(_transformMatrix);
        Vector2 topLeft = Vector2.Transform(Vector2.Zero, transformInverse);
        Vector2 bottomRight = Vector2.Transform(new Vector2( GraphicsDevice.Viewport.Width, this.GraphicsDevice.Viewport.Height), transformInverse);
        _primitiveBatch.Begin(_blendState, _depthStencilState, _rasterizerState, _effect, _transformMatrix);
        foreach (object item in _scene)
        {
            IPosition itemWithPosition = item as IPosition;
            IVelocity itemWithVelocity = item as IVelocity;
            IRadius itemWithRadius = item as IRadius;
            IRectangleSize itemWithRectangleSize = item as IRectangleSize;
            IAAHalfPlaneCollider aaHalfPlaneCollider = item.ConformsToProtocol(@protocol (IAAHalfPlaneCollider)) ? item : null;
            if (itemWithPosition)
            {
                _primitiveBatch.DrawPointAtColor(itemWithPosition.Position, _itemColor);
                if (itemWithRadius)
                {
                    _primitiveBatch.DrawCircleAtRadiusDivisionsColor(itemWithPosition.Position, itemWithRadius.Radius, 32, _itemColor);
                }

                if (itemWithRectangleSize)
                {
                    _primitiveBatch.DrawRectangleAtWidthHeightColor(itemWithPosition.Position, itemWithRectangleSize.Width, itemWithRectangleSize.Height, _itemColor);
                }

            }

            if (itemWithVelocity)
            {
                _primitiveBatch.DrawLineFromToColor(itemWithPosition.Position, Vector2.AddTo(itemWithPosition.Position, itemWithVelocity.Velocity), _movementColor);
            }

            if (aaHalfPlaneCollider)
            {
                AAHalfPlane aaHPlane = aaHalfPlaneCollider.AaHalfPlane;
                if (aaHPlane.Direction == AxisDirectionNegativeX)
                {
                    _primitiveBatch.DrawLineFromToColor(Vector2.VectorWithXY(-aaHPlane.Distance, topLeft.Y), Vector2.VectorWithXY(-aaHPlane.Distance, bottomRight.Y), _colliderColor);
                }
                else if (aaHPlane.Direction == AxisDirectionPositiveX)
                {
                    _primitiveBatch.DrawLineFromToColor(Vector2.VectorWithXY(aaHPlane.Distance, topLeft.Y), Vector2.VectorWithXY(aaHPlane.Distance, bottomRight.Y), _colliderColor);
                }
                else if (aaHPlane.Direction == AxisDirectionNegativeY)
                {
                    _primitiveBatch.DrawLineFromToColor(Vector2.VectorWithXY(topLeft.X, -aaHPlane.Distance), Vector2.VectorWithXY(bottomRight.X, -aaHPlane.Distance), _colliderColor);
                }
                else
                {
                    _primitiveBatch.DrawLineFromToColor(Vector2.VectorWithXY(topLeft.X, aaHPlane.Distance), Vector2.VectorWithXY(bottomRight.X, aaHPlane.Distance), _colliderColor);
                }

            }

        }
        _primitiveBatch.End();
    }
}