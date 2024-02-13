using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpriteBatchDemo.Classes;

public class DrawPrimitives : SpriteBatchDemoComponent
{
    private bool _oneCall = false;
    private VertexPositionColorTexture[] _vertexArray;
    private BasicEffect _effect;
    private MouseState _mouseState;

    public DrawPrimitives(Game game) 
        : base(game)
    {
    }

    protected override void LoadContent()
    {
        base.LoadContent();
        _vertexArray = new VertexPositionColorTexture[9000];
        for (int i = 0; i < 3000; i++)
        {
            VertexPositionColorTexture vertex;
            vertex.Color = new Color(SRandom.Int(255),SRandom.Int(255),SRandom.Int(255));
            Console.WriteLine($"Color = {vertex.Color.ToString()}");
            vertex.Position.X = SRandom.Float(-1f, .8f);
            vertex.Position.Y = SRandom.Float(-1f, .8f);
            vertex.Position.Z = 0;
            Console.WriteLine($"Position = {vertex.Position.ToString()}");
            vertex.TextureCoordinate = SRandom.Vector2(.5f, .5f);
            _vertexArray[3*i] = vertex;
            vertex.Position.Y += 0.2f;
            _vertexArray[3*i+1] = vertex;
            vertex.Position.X += 0.2f;
            _vertexArray[3*i+2] = vertex;
            Console.WriteLine($"TextureCoordinate = {vertex.TextureCoordinate.ToString()}");
        }

        _effect = new BasicEffect(this.GraphicsDevice);
        _effect.TextureEnabled = true;
        _effect.Texture = _sprites1024[0];
        _effect.VertexColorEnabled = true;
    }

    public override void Update(GameTime gameTime)
    {
        var prevMouseState = _mouseState;
        _mouseState = Mouse.GetState();
        if (_mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
        {
            if (_oneCall)
            {
                _oneCall = false;
                Console.WriteLine("Switching to multiple calls.");
            }
            else
            {
                _oneCall = true;
                Console.WriteLine("Switching to one call.");
            }
        }
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        _effect.CurrentTechnique.Passes[0].Apply();
        if (_oneCall)
        {
            this.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertexArray, 0, 3000);
        }
        else
        {
            for (int i = 0; i < 3000; i++)
            {
                this.GraphicsDevice.DrawUserPrimitives(PrimitiveType.TriangleList, _vertexArray, 3*i, 1);
            }

        }
    }
}