using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Artificial_I.Artificial.Spectrum;

public class PrimitiveBatch
{
    private BlendState _blendState;
    private DepthStencilState _depthStencilState;
    private RasterizerState _rasterizerState;
    private Effect _effect;
    private BasicEffect _basicEffect;
    private bool _beginCalled;
    private List<VertexPositionColor> _vertexArray = new List<VertexPositionColor>(256);
    private readonly GraphicsDevice _graphicsDevice;

    public PrimitiveBatch(GraphicsDevice graphicsDevice)
    {
        _graphicsDevice = graphicsDevice;
        _basicEffect = new BasicEffect(graphicsDevice);
        SetProjection();
        _basicEffect.TextureEnabled = true;
        _basicEffect.VertexColorEnabled = true;
        graphicsDevice.DeviceReset += SetProjection;
    }

    public void SetProjection(object o = null, EventArgs args = null)
    {
        _basicEffect.Projection = Matrix.CreateOrthographicOffCenter(-0.5f, _graphicsDevice.Viewport.Width - 0.5f, _graphicsDevice.Viewport.Height - 0.5f, -0.5f, 0, -1);
    }

    public void Begin()
    {
        Begin(null, null, null, null, Matrix.Identity);
    }

    public void Begine(BlendState theBlendState)
    {
        Begin(theBlendState, null, null, null, Matrix.Identity);
    }

    public void Begin(BlendState theBlendState, DepthStencilState theDepthStencilState, RasterizerState theRasterizerState)
    {
        Begin(theBlendState, theDepthStencilState, theRasterizerState, null, Matrix.Identity);
    }

    public void Begin(BlendState theBlendState, DepthStencilState theDepthStencilState, RasterizerState theRasterizerState, Effect theEffect)
    {
        Begin(theBlendState, theDepthStencilState, theRasterizerState, theEffect, Matrix.Identity);
    }

    public void Begin(BlendState theBlendState, DepthStencilState theDepthStencilState, RasterizerState theRasterizerState, Effect theEffect, Matrix theTransformMatrix)
    {
        theBlendState ??= BlendState.AlphaBlend;
        theDepthStencilState ??= DepthStencilState.None;
        theRasterizerState ??= RasterizerState.CullCounterClockwise;
        theEffect ??= _basicEffect;


         _blendState = theBlendState;
        _depthStencilState = theDepthStencilState;
        _rasterizerState = theRasterizerState;
        _effect = theEffect;
        if (_effect is BasicEffect effect1)
        {
            effect1.World = theTransformMatrix;
        }

        _beginCalled = true;
    }

    public void DrawPointAtColor(Vector2 position, Color color)
    {
        DrawPoint(position, color);
    }

    public void DrawPoint(Vector2 position, Color color, float layerDepth = 0f)
    {
        DrawLine(new Vector2(position.X - 0.5f, position.Y - 0.5f), new Vector2(position.X + 0.5f, position.Y + 0.5f), color, layerDepth);
    }

    public void DrawLine(Vector2 start, Vector2 end, Color color, float layerDepth = 0f)
    {
        VertexPositionColor vertex = new VertexPositionColor(new Vector3(start, layerDepth), color);
        _vertexArray.Add(vertex);
        vertex.Position.X = end.X;
        vertex.Position.Y = end.Y;
        _vertexArray.Add(vertex);
    }

    public void DrawCircle(Vector2 center, float radius, int divisions, Color color, float layerDepth = 0)
    {
        Vector2 start = new Vector2(center.X + radius, center.Y);
        Vector2 end = Vector2.Zero;
        for (int i = 1; i <= divisions; i++)
        {
            float angle = (float)i / (float)divisions * (float)Math.PI * 2;
            end.X = center.X + radius * (float)Math.Cos(angle);
            end.Y = center.Y + radius * (float)Math.Sin(angle);
            DrawLine(start, end, color, layerDepth);
            start = end;
        }

    }

    public void DrawRectangle(Vector2 center, float width, float height, Color color, float layerDepth = 0)
    {
        VertexPositionColor vertex = new VertexPositionColor(
            new Vector3( center.X - width / 2, center.Y - height / 2, layerDepth), color
            );
        _vertexArray.Add(vertex);
        vertex.Position.X += width;
        _vertexArray.Add(vertex);
        _vertexArray.Add(vertex);
        vertex.Position.Y += height;
        _vertexArray.Add(vertex);
        _vertexArray.Add(vertex);
        vertex.Position.X -= width;
        _vertexArray.Add(vertex);
        _vertexArray.Add(vertex);
        vertex.Position.Y -= height;
        _vertexArray.Add(vertex);
    }

    public void End()
    {
        if (!_beginCalled)
        {
            throw new Exception("InvalidOperationException End was called before begin.");
        }

        Apply();
        Draw();
        _beginCalled = false;
    }

    public void Apply()
    {
        _graphicsDevice.BlendState = _blendState;
        _graphicsDevice.DepthStencilState = _depthStencilState;
        _graphicsDevice.RasterizerState = _rasterizerState;
        _effect.CurrentTechnique.Passes[0].Apply();
    }

    public void Draw()
    {
        int lineCount = _vertexArray.Count() / 2;
        _graphicsDevice.DrawUserPrimitives(PrimitiveType.LineList, _vertexArray.ToArray(), 0, lineCount);
    }
}