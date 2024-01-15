using System;
using System.Collections.Generic;
using Artificial_I.Artificial.Spectrum;
using Express.Math;
using Express.Scene;
using Express.Scene.Objects;
using Express.Scene.Objects.Colliders;
using Express.Scene.Objects.Shapes;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Express.Graphics;

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
        ItemColor = Color.OrangeRed;
        MovementColor = Color.SkyBlue;
        ColliderColor = Color.Lime;
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
        _primitiveBatch = new PrimitiveBatch(GraphicsDevice);
    }

    public override void Draw(GameTime gameTime)
    {
        Matrix transformInverse = Matrix.Invert(_transformMatrix);
        Vector2 topLeft = Vector2.Transform(Vector2.Zero, transformInverse);
        Vector2 bottomRight = Vector2.Transform(new Vector2( GraphicsDevice.Viewport.Width, GraphicsDevice.Viewport.Height), transformInverse);
        _primitiveBatch.Begin(_blendState, _depthStencilState, _rasterizerState, _effect, transformInverse);
        foreach (object item in _scene)
        {
            IPosition itemWithPosition = item as IPosition;
            IVelocity itemWithVelocity = item as IVelocity;
            IRotation itemWithRotation = item as IRotation;
            IRadius itemWithRadius = item as IRadius;
            IRectangleSize itemWithRectangleSize = item as IRectangleSize;
            IAaHalfPlaneCollider aaHalfPlaneCollider = item as IAaHalfPlaneCollider;
            IHalfPlaneCollider halfPlaneCollider = item as IHalfPlaneCollider;
            IConvexCollider convex  = item as IConvexCollider;
            
            if (itemWithPosition is not null)
            {
                _primitiveBatch.DrawPointAtColor(itemWithPosition.Position, _itemColor);
                if (itemWithRadius is not null)
                {
                    _primitiveBatch.DrawCircle(itemWithPosition.Position, itemWithRadius.Radius, 32, _itemColor);
                }

                if (itemWithRectangleSize is not null)
                {
                    _primitiveBatch.DrawRectangle(itemWithPosition.Position, itemWithRectangleSize.Width, itemWithRectangleSize.Height, _itemColor);
                }

                if (itemWithVelocity is not null)
                {
                    _primitiveBatch.DrawLine(itemWithPosition.Position, (itemWithPosition.Position + itemWithVelocity.Velocity), _movementColor);
                }
                
            }

            if (aaHalfPlaneCollider is not null)
            {
                AaHalfPlane aaHPlane = aaHalfPlaneCollider.AaHalfPlane;
                if (aaHPlane.Direction == AxisDirection.NegativeX)
                {
                    _primitiveBatch.DrawLine(new Vector2(-aaHPlane.Distance, topLeft.Y), new Vector2(-aaHPlane.Distance, bottomRight.Y), _colliderColor);
                }
                else if (aaHPlane.Direction == AxisDirection.PositiveX)
                {
                    _primitiveBatch.DrawLine(new Vector2(aaHPlane.Distance, topLeft.Y), new Vector2(aaHPlane.Distance, bottomRight.Y), _colliderColor);
                }
                else if (aaHPlane.Direction == AxisDirection.NegativeY)
                {
                    _primitiveBatch.DrawLine(new Vector2(topLeft.X, -aaHPlane.Distance), new Vector2(bottomRight.X, -aaHPlane.Distance), _colliderColor);
                }
                else
                {
                    _primitiveBatch.DrawLine(new Vector2(topLeft.X, aaHPlane.Distance), new Vector2(bottomRight.X, aaHPlane.Distance), _colliderColor);
                }

            }

            if (halfPlaneCollider is not null)
            {
                HalfPlane hPlane = halfPlaneCollider.HalfPlane;
                
                Vector2 pointOnPlane = hPlane.Normal * hPlane.Distance;
                Vector2 planeDirection = new Vector2(hPlane.Normal.Y, -hPlane.Normal.X);

                float screenDiagonalLength = MathF.Sqrt(MathF.Pow(GraphicsDevice.Viewport.Width, 2) +
                                                        MathF.Pow(GraphicsDevice.Viewport.Height, 2));

                Vector2 lineVectorStart = planeDirection * screenDiagonalLength;
                Vector2 lineVectorEnd = planeDirection * -screenDiagonalLength;

                Vector2 hPlaneStart = lineVectorStart + pointOnPlane;
                Vector2 hPlaneEnd = lineVectorEnd + pointOnPlane;

                _primitiveBatch.DrawLine(hPlaneStart, hPlaneEnd, _colliderColor);
            }

            if (convex is not null)
            {
                Vector2 offset = itemWithPosition is not null ? itemWithPosition.Position : Vector2.Zero;
                float angle = itemWithRotation is not null ? itemWithRotation.RotationAngle : 0;
                Matrix transform = Matrix.CreateRotationZ(angle) * (Matrix.CreateTranslation(offset.X, offset.Y, 0));
                List<Vector2> vertices = convex.Bounds.Vertices;
                for (int i = 0; i < vertices.Count; i++)
                {
                    int j = (i + 1) % vertices.Count;
                    Vector2 start = Vector2.Transform(vertices[i], transform);
                    Vector2 end = Vector2.Transform(vertices[j], transform);
                    _primitiveBatch.DrawLine(start, end, itemWithPosition is not null ? _itemColor : _colliderColor);
                }
            }

        }
        _primitiveBatch.End();
    }
}