using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Artificial_I.Artificial.Spectrum;

public class PrimitiveBatch
{
    private BlendState blendState;
    private DepthStencilState depthStencilState;
    private RasterizerState rasterizerState;
    private Effect effect;
    private Matrix transformMatrix;
    private BasicEffect basicEffect;
    private bool beginCalled;
    private VertexPositionColorArray vertexArray;
    static Matrix identity;
    static VertexPositionColorStruct vertex;

    PrimitiveBatch(GraphicsDevice theGraphicsDevice)
        : base (theGraphicsDevice)
    {
        basicEffect = new BasicEffect(theGraphicsDevice);
        this.SetProjection();
        basicEffect.TextureEnabled = true;
        basicEffect.VertexColorEnabled = true;
        vertexArray = new VertexPositionColorArray(256);
        theGraphicsDevice.DeviceReset.SubscribeDelegate(Delegate.DelegateWithTargetMethod(this, @selector (setProjection)));
    }

    static PrimitiveBatch ()
    {
        identity = Matrix.Identity();
    }
    public void SetProjection()
    {
        basicEffect.Projection = Matrix.CreateOrthographicOffCenterWithLeftRightBottomTopZNearPlaneZFarPlane(-0.5f, this.GraphicsDevice.Viewport.Width - 0.5f, this.GraphicsDevice.Viewport.Height - 0.5f, -0.5f, 0, -1);
    }

    public void Begin()
    {
        this.BeginWithBlendStateDepthStencilStateRasterizerStateEffectTransformMatrix(null, null, null, null, null);
    }

    public void BeginWithBlendState(BlendState theBlendState)
    {
        this.BeginWithBlendStateDepthStencilStateRasterizerStateEffectTransformMatrix(theBlendState, null, null, null, null);
    }

    public void BeginWithBlendStateDepthStencilStateRasterizerState(BlendState theBlendState, DepthStencilState theDepthStencilState, RasterizerState theRasterizerState)
    {
        this.BeginWithBlendStateDepthStencilStateRasterizerStateEffectTransformMatrix(theBlendState, theDepthStencilState, theRasterizerState, null, null);
    }

    public void BeginWithBlendStateDepthStencilStateRasterizerStateEffect(BlendState theBlendState, DepthStencilState theDepthStencilState, RasterizerState theRasterizerState, Effect theEffect)
    {
        this.BeginWithBlendStateDepthStencilStateRasterizerStateEffectTransformMatrix(theBlendState, theDepthStencilState, theRasterizerState, theEffect, null);
    }

    public void BeginWithBlendStateDepthStencilStateRasterizerStateEffectTransformMatrix(BlendState theBlendState, DepthStencilState theDepthStencilState, RasterizerState theRasterizerState, Effect theEffect, Matrix theTransformMatrix)
    {
        if (!theBlendState) theBlendState = BlendState.AlphaBlend();

        if (!theDepthStencilState) theDepthStencilState = DepthStencilState.None();

        if (!theRasterizerState) theRasterizerState = RasterizerState.CullCounterClockwise();

        if (!theEffect) theEffect = basicEffect;

        if (!theTransformMatrix) theTransformMatrix = Matrix.Identity();

        blendState = theBlendState;
        depthStencilState = theDepthStencilState;
        rasterizerState = theRasterizerState;
        effect = theEffect;
        if (effect.IsKindOfClass(typeof(BasicEffect)))
        {
            ((BasicEffect)effect).World = theTransformMatrix;
        }

        beginCalled = true;
    }

    public void DrawPointAtColor(Vector2 position, Color color)
    {
        this.DrawPointAtColorLayerDepth(position, color, 0);
    }

    public void DrawPointAtColorLayerDepth(Vector2 position, Color color, float layerDepth)
    {
        this.DrawLineFromToColorLayerDepth(Vector2.VectorWithXY(position.X - 0.5f, position.Y - 0.5f), Vector2.VectorWithXY(position.X + 0.5f, position.Y + 0.5f), color, layerDepth);
    }

    public void DrawLineFromToColor(Vector2 start, Vector2 end, Color color)
    {
        this.DrawLineFromToColorLayerDepth(start, end, color, 0);
    }

    public void DrawLineFromToColorLayerDepth(Vector2 start, Vector2 end, Color color, float layerDepth)
    {
        vertex.Color = color.PackedValue;
        vertex.Position.Z = layerDepth;
        vertex.Position.X = start.Data.X;
        vertex.Position.Y = start.Data.Y;
        vertexArray.AddVertex(vertex);
        vertex.Position.X = end.Data.X;
        vertex.Position.Y = end.Data.Y;
        vertexArray.AddVertex(vertex);
    }

    public void DrawCircleAtRadiusDivisionsColor(Vector2 center, float radius, int divisions, Color color)
    {
        this.DrawCircleAtRadiusDivisionsColorLayerDepth(center, radius, divisions, color, 0);
    }

    public void DrawCircleAtRadiusDivisionsColorLayerDepth(Vector2 center, float radius, int divisions, Color color, float layerDepth)
    {
        Vector2 start = Vector2.VectorWithXY(center.X + radius, center.Y);
        Vector2 end = Vector2.Zero();
        for (int i = 1; i <= divisions; i++)
        {
            float angle = (float)i / (float)divisions * Math.PI * 2;
            end.X = center.X + radius * (float)Math.Cos(angle);
            end.Y = center.Y + radius * (float)Math.Sin(angle);
            this.DrawLineFromToColorLayerDepth(start, end, color, layerDepth);
            start.Set(end);
        }

    }

    public void DrawRectangleAtWidthHeightColor(Vector2 center, float width, float height, Color color)
    {
        this.DrawRectangleAtWidthHeightColorLayerDepth(center, width, height, color, 0);
    }

    public void DrawRectangleAtWidthHeightColorLayerDepth(Vector2 center, float width, float height, Color color, float layerDepth)
    {
        vertex.Color = color.PackedValue;
        vertex.Position.Z = layerDepth;
        vertex.Position.X = center.Data.X - width / 2;
        vertex.Position.Y = center.Data.Y - height / 2;
        vertexArray.AddVertex(vertex);
        vertex.Position.X += width;
        vertexArray.AddVertex(vertex);
        vertexArray.AddVertex(vertex);
        vertex.Position.Y += height;
        vertexArray.AddVertex(vertex);
        vertexArray.AddVertex(vertex);
        vertex.Position.X -= width;
        vertexArray.AddVertex(vertex);
        vertexArray.AddVertex(vertex);
        vertex.Position.Y -= height;
        vertexArray.AddVertex(vertex);
    }

    public void End()
    {
        if (!beginCalled)
        {
            throw new Exception("InvalidOperationException", "End was called before begin.", null);
        }

        this.Apply();
        this.Draw();
        beginCalled = false;
    }

    public void Apply()
    {
        graphicsDevice.BlendState = blendState;
        graphicsDevice.DepthStencilState = depthStencilState;
        graphicsDevice.RasterizerState = rasterizerState;
        effect.CurrentTechnique.Passes.ObjectAtIndex(0).Apply();
    }

    public void Draw()
    {
        int lineCount = vertexArray.Count() / 2;
        graphicsDevice.DrawUserPrimitivesOfTypeVertexDataVertexOffsetPrimitiveCount(PrimitiveTypeLineList, vertexArray, 0, lineCount);
        vertexArray.Clear();
    }
}