using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpriteBatchDemo.Classes;

public class TextureSorting : SpriteBatchDemoComponent
{
    private SpriteSortMode _sortMode;
    private MouseState _mouseState;

    public TextureSorting(Game game) 
        : base(game)
    {
    }

    public override void Update(GameTime gameTime)
    {
        var prevMouseState = _mouseState;
        _mouseState = Mouse.GetState();
        if (_mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released)
        {
            switch (_sortMode)
            {
                case SpriteSortMode.Deferred:
                    _sortMode = SpriteSortMode.Texture;
                    Console.WriteLine("Switching to texture mode.");
                    break;
                case SpriteSortMode.Texture:
                    _sortMode = SpriteSortMode.Deferred;
                    Console.WriteLine("Switching to deferred mode.");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        base.Update(gameTime);
    }

    public override void Draw(GameTime gameTime)
    {
        base.Draw(gameTime);
        SpriteBatch.Begin(_sortMode, null);
        foreach (Item item in Scene)
        {
            SpriteBatch.Draw(Sprites256[item.RectangleIndex % 16], item.Position, null, item.Color, 0, Vector2.Zero, 0.1f, SpriteEffects.None, 0);
        }
        SpriteBatch.End();
    }
}