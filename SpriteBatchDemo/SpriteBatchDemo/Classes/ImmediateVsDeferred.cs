using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace SpriteBatchDemo.Classes;

public class ImmediateVsDeferred : SpriteBatchDemoComponent
{
    private SpriteSortMode _sortMode = SpriteSortMode.Deferred;
    private MouseState _mouseState = new MouseState();

    public ImmediateVsDeferred(Game game) 
        : base(game)
    {
        MaxItems = 4000;
    }

    public override void Update(GameTime gameTime)
    {
        var prevMouseState = _mouseState;
        _mouseState = Mouse.GetState();
        if (_mouseState.LeftButton == ButtonState.Pressed && prevMouseState.LeftButton == ButtonState.Released )
        {
            switch (_sortMode)
            {
                case SpriteSortMode.Deferred:
                    Console.WriteLine("Switching to immediate mode.");
                    _sortMode = SpriteSortMode.Immediate;
                    break;
                case SpriteSortMode.Immediate:
                    Console.WriteLine("Switching to deferred mode.");
                    _sortMode = SpriteSortMode.Deferred;
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
            SpriteBatch.Draw(Sprites256[0], item.Position, Rectangle16[item.RectangleIndex % 12], item.Color);
        }
        SpriteBatch.End();
    }
}