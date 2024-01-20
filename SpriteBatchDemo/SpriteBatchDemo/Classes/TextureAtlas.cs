
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpriteBatchDemo.Classes;

public class TextureAtlas : SpriteBatchDemoComponent
{
    private bool _useTextureAtlas = false;
    private MouseState _mouseState;

    public TextureAtlas(Game game)
        : base(game)
    {
    }

    public override void Update(GameTime gameTime)
    {
        var prevMouseState = _mouseState;
        _mouseState = Mouse.GetState();
        if (_mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
        {
            if (_useTextureAtlas)
            {
                _useTextureAtlas = false;
                Console.WriteLine("Switching to separate textures.");
            }
            else
            {
                _useTextureAtlas = true;
                Console.WriteLine("Switching to texture atlas.");
            }
        }
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        _spriteBatch.Begin();
        if (_useTextureAtlas)
        {
            foreach (Item item in _scene)
            {
                _spriteBatch.Draw(_sprites1024[0], item.Position, _rectangle256[item.RectangleIndex % 16], item.Color, 0, Vector2.Zero, 0.1f, SpriteEffects.None, 0);
            }
        }
        else
        {
            foreach (Item item in _scene)
            {
                _spriteBatch.Draw(_sprites256[item.RectangleIndex % 16], item.Position, null, item.Color, 0, Vector2.Zero, 0.1f, SpriteEffects.None, 0);
            }
        }

        _spriteBatch.End();
    }
}
        

