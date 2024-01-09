using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace ThreeDee;

public class ThreeDee : Game
{
    private GraphicsDeviceManager _graphics;
    // private SpriteBatch _spriteBatch;
    
    // Camera
    protected Matrix _view;
    protected Matrix _projection;
    // Model
    protected Model _model;
    // Basic effects
    protected BasicEffect _colorEffect;
    protected BasicEffect _effect;
    // User primitives
    protected VertexPositionColor[] _vertexArray;
    // Indexed user primitives
    protected VertexPositionNormalTexture[] _texturedVertexArray;
    protected short[] _indexArray;
    // Buffers
    protected VertexBuffer _vertexBuffer;
    protected IndexBuffer _indexBuffer;

    public ThreeDee()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        this.GraphicsDevice.DeviceReset += ScreenChanged;
        this.ScreenChanged();
        _view = Matrix.CreateLookAt(new Vector3(0, 13, 13), Vector3.Zero, Vector3.Up);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // _spriteBatch = new SpriteBatch(GraphicsDevice);

        // 1. Model
            _model = this.Content.Load<Model>("square");
            // 2. User primitives
            _vertexArray = new VertexPositionColor[5];
            VertexPositionColor colorVertex = new();
            colorVertex.Position.X = -1;
            colorVertex.Position.Y = 0;
            colorVertex.Position.Z = -1;
            colorVertex.Color = Color.Blue;
            _vertexArray[0] = colorVertex;
            colorVertex.Position.X = -1;
            colorVertex.Position.Y = 0;
            colorVertex.Position.Z = 1;
            colorVertex.Color = Color.Yellow;
            _vertexArray[1] = colorVertex;
            colorVertex.Position.X = 1;
            colorVertex.Position.Y = 0;
            colorVertex.Position.Z = 1;
            colorVertex.Color = Color.Red;
            _vertexArray[2] = colorVertex;
            colorVertex.Position.X = 1;
            colorVertex.Position.Y = 0;
            colorVertex.Position.Z = -1;
            colorVertex.Color = Color.Lime;
            _vertexArray[3] = colorVertex;
            colorVertex.Position.X = -1;
            colorVertex.Position.Y = 0;
            colorVertex.Position.Z = -1;
            colorVertex.Color = Color.Blue;
            _vertexArray[4] = colorVertex;
            _colorEffect = new BasicEffect(this.GraphicsDevice);
            _colorEffect.World = Matrix.CreateTranslation(-3, 0, 0);
            _colorEffect.View = _view;
            _colorEffect.Projection = _projection;
            _colorEffect.VertexColorEnabled = true;
            // 3. Textured indexed user primitives
            // Vertex array
            _texturedVertexArray = new VertexPositionNormalTexture[4];
            VertexPositionNormalTexture vertex = new();
            vertex.Position.X = -1;
            vertex.Position.Y = 0;
            vertex.Position.Z = -1;
            vertex.Normal.X = 0;
            vertex.Normal.Y = 1;
            vertex.Normal.Z = 0;
            vertex.TextureCoordinate = new Vector2(0, 0);
            _texturedVertexArray[0] = vertex;
            
            vertex.Position.X = -1;
            vertex.Position.Y = 0;
            vertex.Position.Z = 1;
            vertex.Normal.X = 0;
            vertex.Normal.Y = 1;
            vertex.Normal.Z = 0;
            vertex.TextureCoordinate = new Vector2(0, 1);
            _texturedVertexArray[1] = vertex;
            
            vertex.Position.X = 1;
            vertex.Position.Y = 0;
            vertex.Position.Z = 1;
            vertex.Normal.X = 0;
            vertex.Normal.Y = 1;
            vertex.Normal.Z = 0;
            vertex.TextureCoordinate = new Vector2(1, 1);
            _texturedVertexArray[2] = vertex;
            
            vertex.Position.X = 1;
            vertex.Position.Y = 0;
            vertex.Position.Z = -1;
            vertex.Normal.X = 0;
            vertex.Normal.Y = 1;
            vertex.Normal.Z = 0;
            vertex.TextureCoordinate = new Vector2(1, 0);
            _texturedVertexArray[3] = vertex;
            
            // Index array
            _indexArray = new short[] {0,2,1,2,0,3};
            // Effect
            _effect = new BasicEffect(this.GraphicsDevice);
            _effect.World = Matrix.CreateTranslation(3, 0, 0);
            _effect.View = _view;
            _effect.Projection = _projection;
            _effect.TextureEnabled = true;
            _effect.VertexColorEnabled = false;
            _effect.LightingEnabled = true;
            // Material
            _effect.Texture = this.Content.Load<Texture2D>("road");
            _effect.DiffuseColor = Vector3.One;
            // Lighting
            _effect.AmbientLightColor = new Vector3(0.7f, 0.6f, 0.8f);
            _effect.DirectionalLight0.Enabled = true;
            _effect.DirectionalLight0.Direction = Vector3.Down;
            _effect.DirectionalLight0.DiffuseColor = Vector3.One;
            // 4. Buffers
            // Vertex buffer                                           // texturedVertexArray
            _vertexBuffer = new VertexBuffer(this.GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.WriteOnly);
            _vertexBuffer.SetData(_texturedVertexArray);
            // Index buffer
            _indexBuffer = new IndexBuffer(this.GraphicsDevice, IndexElementSize.SixteenBits, 6, BufferUsage.WriteOnly);
            _indexBuffer.SetData(_indexArray);
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed ||
            Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        // TODO: Add your update logic here

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.Gray);
        this.GraphicsDevice.RasterizerState = RasterizerState.CullNone;
        // 1. Model
        Matrix rotation = Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds);
        Matrix translation = Matrix.CreateTranslation(-8, 0, 0);
        Matrix world = Matrix.Multiply(rotation, translation);
        _model.Draw(world, _view, _projection);
        // 2. User primitives
        _colorEffect.World = Matrix.Multiply(Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds), Matrix.CreateTranslation(-3, 0, 0));
        _colorEffect.CurrentTechnique.Passes[0].Apply();
        this.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, _vertexArray, 0, 4);
        // 3. Textured indexed user primitives
        _effect.DirectionalLight0.Direction = new Vector3( 0, (float)Math.Sin((float)gameTime.TotalGameTime.TotalSeconds), (float)Math.Cos((float)gameTime.TotalGameTime.TotalSeconds));
        _effect.CurrentTechnique.Passes[0].Apply();
        // this.GraphicsDevice.DrawUserIndexedPrimitives(PrimitiveType.TriangleList, _texturedVertexArray, 0, 4, _indexArray, 0, 2);
        
        // 4. Buffers
        this.GraphicsDevice.SetVertexBuffer(_vertexBuffer);
        this.GraphicsDevice.Indices = _indexBuffer;
        this.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 2);

        base.Draw(gameTime);
    }
    
    public void ScreenChanged(object caller = null, EventArgs e = null)
    {
        _projection = Matrix.CreatePerspectiveFieldOfView(MathF.PI / 4f, this.GraphicsDevice.Viewport.AspectRatio, 0.1f, 100);
        if (_effect is not null)
        {
            _effect.Projection = _projection;
        }

        if (_colorEffect is not null)
        {
            _colorEffect.Projection = _projection;
        }
    }
}