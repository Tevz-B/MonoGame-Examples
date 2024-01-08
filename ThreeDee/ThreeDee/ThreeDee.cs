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
    protected Matrix view;
    protected Matrix projection;
    // Model
    protected Model model;
    // Basic effects
    protected BasicEffect colorEffect;
    protected BasicEffect effect;
    // User primitives
    protected VertexPositionColor[] vertexArray;
    // Indexed user primitives
    protected VertexPositionNormalTexture[] texturedVertexArray;
    protected short[] indexArray;
    // Buffers
    protected VertexBuffer vertexBuffer;
    protected IndexBuffer indexBuffer;

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
        view = Matrix.CreateLookAt(new Vector3(0, 13, 13), Vector3.Zero, Vector3.Up);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        // _spriteBatch = new SpriteBatch(GraphicsDevice);

        // 1. Model
            model = this.Content.Load<Model>("square");
            // 2. User primitives
            vertexArray = new VertexPositionColor[5];
            VertexPositionColor colorVertex = new();
            colorVertex.Position.X = -1;
            colorVertex.Position.Y = 0;
            colorVertex.Position.Z = -1;
            colorVertex.Color = Color.Blue;
            vertexArray[0] = colorVertex;
            colorVertex.Position.X = -1;
            colorVertex.Position.Y = 0;
            colorVertex.Position.Z = 1;
            colorVertex.Color = Color.Yellow;
            vertexArray[1] = colorVertex;
            colorVertex.Position.X = 1;
            colorVertex.Position.Y = 0;
            colorVertex.Position.Z = 1;
            colorVertex.Color = Color.Red;
            vertexArray[2] = colorVertex;
            colorVertex.Position.X = 1;
            colorVertex.Position.Y = 0;
            colorVertex.Position.Z = -1;
            colorVertex.Color = Color.Lime;
            vertexArray[3] = colorVertex;
            colorVertex.Position.X = -1;
            colorVertex.Position.Y = 0;
            colorVertex.Position.Z = -1;
            colorVertex.Color = Color.Blue;
            vertexArray[4] = colorVertex;
            colorEffect = new BasicEffect(this.GraphicsDevice);
            colorEffect.World = Matrix.CreateTranslation(-3, 0, 0);
            colorEffect.View = view;
            colorEffect.Projection = projection;
            colorEffect.VertexColorEnabled = true;
            // 3. Textured indexed user primitives
            // Vertex array
            texturedVertexArray = new VertexPositionNormalTexture[4];
            VertexPositionNormalTexture vertex = new();
            vertex.Position.X = -1;
            vertex.Position.Y = 0;
            vertex.Position.Z = -1;
            vertex.Normal.X = 0;
            vertex.Normal.Y = 1;
            vertex.Normal.Z = 0;
            vertex.TextureCoordinate = new Vector2(0, 0);
            texturedVertexArray[0] = vertex;
            
            vertex.Position.X = -1;
            vertex.Position.Y = 0;
            vertex.Position.Z = 1;
            vertex.Normal.X = 0;
            vertex.Normal.Y = 1;
            vertex.Normal.Z = 0;
            vertex.TextureCoordinate = new Vector2(0, 1);
            texturedVertexArray[1] = vertex;
            
            vertex.Position.X = 1;
            vertex.Position.Y = 0;
            vertex.Position.Z = 1;
            vertex.Normal.X = 0;
            vertex.Normal.Y = 1;
            vertex.Normal.Z = 0;
            vertex.TextureCoordinate = new Vector2(1, 1);
            texturedVertexArray[2] = vertex;
            
            vertex.Position.X = 1;
            vertex.Position.Y = 0;
            vertex.Position.Z = -1;
            vertex.Normal.X = 0;
            vertex.Normal.Y = 1;
            vertex.Normal.Z = 0;
            vertex.TextureCoordinate = new Vector2(1, 0);
            texturedVertexArray[3] = vertex;
            
            // Index array
            indexArray = new short[] {0,2,1,2,0,3};
            // Effect
            effect = new BasicEffect(this.GraphicsDevice);
            effect.World = Matrix.CreateTranslation(3, 0, 0);
            effect.View = view;
            effect.Projection = projection;
            effect.TextureEnabled = true;
            effect.VertexColorEnabled = false;
            effect.LightingEnabled = true;
            // Material
            effect.Texture = this.Content.Load<Texture2D>("road");
            effect.DiffuseColor = Vector3.One;
            // Lighting
            effect.AmbientLightColor = new Vector3(0.7f, 0.6f, 0.8f);
            effect.DirectionalLight0.Enabled = true;
            effect.DirectionalLight0.Direction = Vector3.Down;
            effect.DirectionalLight0.DiffuseColor = Vector3.One;
            // 4. Buffers
            // Vertex buffer                                           // texturedVertexArray
            vertexBuffer = new VertexBuffer(this.GraphicsDevice, typeof(VertexPositionNormalTexture), 4, BufferUsage.WriteOnly);
            vertexBuffer.SetData(texturedVertexArray);
            // Index buffer
            indexBuffer = new IndexBuffer(this.GraphicsDevice, IndexElementSize.SixteenBits, 6, BufferUsage.WriteOnly);
            indexBuffer.SetData(indexArray);
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
        model.Draw(world, view, projection);
        // 2. User primitives
        colorEffect.World = Matrix.Multiply(Matrix.CreateRotationY((float)gameTime.TotalGameTime.TotalSeconds), Matrix.CreateTranslation(-3, 0, 0));
        colorEffect.CurrentTechnique.Passes[0].Apply();
        this.GraphicsDevice.DrawUserPrimitives(PrimitiveType.LineStrip, vertexArray, 0, 4);
        // 3. Textured indexed user primitives
        effect.DirectionalLight0.Direction = new Vector3( 0, (float)Math.Sin((float)gameTime.TotalGameTime.TotalSeconds), (float)Math.Cos((float)gameTime.TotalGameTime.TotalSeconds));
        effect.CurrentTechnique.Passes[0].Apply();
        /*

           [self.graphicsDevice drawUserIndexedPrimitivesOfType:PrimitiveTypeTriangleList

           vertexData:texturedVertexArray

           vertexOffset:0

           numVertices:4

           indexData:indexArray

           indexOffset:0

           primitiveCount:2];

           */
        // 4. Buffers
        this.GraphicsDevice.SetVertexBuffer(vertexBuffer);
        this.GraphicsDevice.Indices = indexBuffer;
        this.GraphicsDevice.DrawIndexedPrimitives(PrimitiveType.TriangleList, 0, 0, 2);

        base.Draw(gameTime);
    }
    
    public void ScreenChanged(object caller = null, EventArgs e = null)
    {
        projection = Matrix.CreatePerspective(MathF.PI / 20f, this.GraphicsDevice.Viewport.AspectRatio / 10f, 0.1f, 100);
        if (effect is not null)
        {
            effect.Projection = projection;
        }

        if (colorEffect is not null)
        {
            colorEffect.Projection = projection;
        }
    }
}