using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Artificial_I.Utils
{
    public class FPSComponent : DrawableGameComponent
    {
        private SpriteBatch _spriteBatch;
        private SpriteFont _spriteFont;

        private int _frameRate = 0;
        private int _frameCounter = 0;
        private TimeSpan _elapsedTime = TimeSpan.Zero;
        private bool _writeToConsole = false;


        public FPSComponent(Game game, SpriteBatch batch, SpriteFont font)
            : base(game)
        {
            _spriteFont = font;
            _spriteBatch = batch;
        }

        public FPSComponent(Game game)
            : base(game)
        {
            _spriteFont = null;
            _spriteBatch = null;
        }


        public override void Update(GameTime gameTime)
        {
            _writeToConsole = false;
            _elapsedTime += gameTime.ElapsedGameTime;

            if (_elapsedTime > TimeSpan.FromSeconds(1))
            {
                _elapsedTime -= TimeSpan.FromSeconds(1);
                _frameRate = _frameCounter;
                _frameCounter = 0;
                _writeToConsole = true;
            }
        }


        public override void Draw(GameTime gameTime)
        {
            _frameCounter++;

            string fps = $"fps: {_frameRate} mem : {GC.GetTotalMemory(false)}";
            if (_spriteBatch != null && _spriteFont != null)
            {
                _spriteBatch.DrawString(_spriteFont, fps, new Vector2(1, 1), Color.Black);
                _spriteBatch.DrawString(_spriteFont, fps, new Vector2(0, 0), Color.White);
            }
            else if (_writeToConsole)
            {
                Console.WriteLine(fps);
            }
        }
    }
}