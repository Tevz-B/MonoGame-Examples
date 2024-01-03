using Artificial_I.Artificial.Mirage;
using Express.Scene;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace friHockey_v6.Graphics;

public class GuiRenderer : DrawableGameComponent
{
        protected SpriteBatch _spriteBatch;
        protected IScene _scene;
        protected Matrix _camera;
        public Matrix Camera => _camera;

        public GuiRenderer(Game theGame, IScene theScene)
            : base (theGame)
        {
            _scene = theScene;
        }

        public override void  Initialize()
        {
            float scaleX = (float)Game.Window.ClientBounds.Width / 320;
            float scaleY = (float)Game.Window.ClientBounds.Height / 480;
            _camera = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));
            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
        }

        public override void Draw(GameTime gameTime)
        {
            _spriteBatch.Begin(SpriteSortMode.Deferred, null, SamplerState.PointClamp, null, null, null, _camera);
            foreach (object item in _scene)
            {
                if (item is Label label)
                {
                    _spriteBatch.DrawString(label.Font, label.Text, label.Position, label.Color, label.Rotation, label.Origin, label.Scale, SpriteEffects.None, label.LayerDepth);
                }

                if (item is Image image && image.Texture is not null)
                {
                    _spriteBatch.Draw(image.Texture, image.Position, image.SourceRectangle, image.Color, image.Rotation, image.Origin, image.Scale, SpriteEffects.None, image.LayerDepth);
                }
            }
            _spriteBatch.End();
        }
    }