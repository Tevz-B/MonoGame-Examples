using System;
using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace SpriteBatchDemo.Classes;

public class SpriteBatchDemoComponent : DrawableGameComponent
{
    protected SpriteBatch SpriteBatch;
    protected Texture2D[] Sprites256 = new Texture2D[16];
    protected Texture2D[] Sprites1024 = new Texture2D[4];
    // protected Texture2D _sprite2048;
    protected Rectangle[] Rectangle16 = new Rectangle[12];
    protected Rectangle[] Rectangle256 = new Rectangle[16];
    protected ArrayList Scene;
    protected int MaxItems;

    protected SpriteBatchDemoComponent(Game game)
        : base(game)
    {
        MaxItems = 1000;
        Scene = new ArrayList();
    }

    public override void Initialize()
    {
        for (int i = 0; i < MaxItems; i++)
            Scene.Add(new Item());

        for (int i = 0; i < 12; i++)
            Rectangle16[i] = new Rectangle(i % 4 * 16, i / 4 * 16, 16, 16);

        for (int i = 0; i < 16; i++)
            Rectangle256[i] = new Rectangle(i % 4 * 256, i / 4 * 256, 256, 256);

        base.Initialize();
    }

    protected override void LoadContent()
    {
        SpriteBatch = new SpriteBatch(GraphicsDevice);
        for (var i = 0; i < 16; i++)
            Sprites256[i] = this.Game.Content.Load<Texture2D>($"sprites-256-{i}");
        
        for (var i = 0; i < 4; i++)
            Sprites1024[i] = this.Game.Content.Load<Texture2D>($"sprites-1024-{i}");
    }

    public override void Update(GameTime gameTime)
    {
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds / 10f;
        foreach (Item item in Scene)
        {
            item.Position += Vector2.Multiply(item.Velocity, dt);
            if (item.Position.X < 0) 
                item.Velocity.X = (float)Math.Abs(item.Velocity.X);
            else if (item.Position.X > this.GraphicsDevice.Viewport.Width) 
                item.Velocity.X = -(float)Math.Abs(item.Velocity.X);
            

            if (item.Position.Y < 0)
                item.Velocity.Y = (float)Math.Abs(item.Velocity.Y);
            else if (item.Position.Y > this.GraphicsDevice.Viewport.Height) 
                item.Velocity.Y = -(float)Math.Abs(item.Velocity.Y);
        }
    }

    public override void Draw(GameTime gameTime)
    {
        this.GraphicsDevice.Clear(Color.Black);
        base.Draw(gameTime);
    }
}