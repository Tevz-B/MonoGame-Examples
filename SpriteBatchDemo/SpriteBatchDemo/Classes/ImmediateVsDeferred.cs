using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace SpriteBatchDemo.Classes;

public class ImmediateVsDeferred : SpriteBatchDemoComponent
{
    private SpriteSortMode _sortMode = SpriteSortMode.Deferred;
    private MouseState _mouseState = new MouseState();

    public ImmediateVsDeferred(Game game) 
        : base(game)
    {
        _maxItems = 4000;
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
                    _sortMode = SpriteSortMode.Immediate;
                    Console.WriteLine("Switching to immediate mode.");
                    break;
                case SpriteSortMode.Immediate:
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
        _spriteBatch.Begin(_sortMode, null);
        foreach (Item item in _scene)
        {
            _spriteBatch.Draw(_sprites256[0], item.Position, _rectangle16[item.RectangleIndex % 12], item.Color);
        }
        _spriteBatch.End();
    }
}