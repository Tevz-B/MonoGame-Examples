using System;
using Microsoft.Xna.Framework;

namespace MadDriver_v1.Graphics;

public class Director : DrawableGameComponent
{
    protected Camera _camera;

    public Director(Game theGame)
        : base (theGame)
    {
        _camera = new Camera { Position = 300 };    
        GraphicsDevice.DeviceReset += DeviceResetEvent;
    }

    public Camera Camera => _camera;

    public override void Update(GameTime gameTime)
    {
        _camera.Position -= (float)gameTime.ElapsedGameTime.TotalSeconds * 10;
    }

    public override void Initialize()
    {
        base.Initialize();
        this.UpdateAspectRatio();
    }

    void DeviceResetEvent(object sender, EventArgs e)
    {
        this.UpdateAspectRatio();
    }

    public void UpdateAspectRatio()
    {
        _camera.AspectRatio = this.GraphicsDevice.Viewport.AspectRatio;
    }


}