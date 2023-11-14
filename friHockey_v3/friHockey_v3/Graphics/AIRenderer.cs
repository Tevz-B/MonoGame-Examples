using Artificial_I.Artificial.Spectrum;
using friHockey_v3.Players.AI;
using Microsoft.Xna.Framework;

namespace friHockey_v3.Graphics;

public class AIRenderer : DrawableGameComponent
{
    protected PrimitiveBatch _primitiveBatch;
    protected AIPlayer _aiPlayer;
    protected Matrix _camera;

    public AIRenderer(Game theGame, AIPlayer theAIPlayer)
        : base (theGame)
    {
        _aiPlayer = theAIPlayer;
    }

    public override void Initialize()
    {
        float scaleX = (float)this.Game.Window.ClientBounds.Width / 320;
        float scaleY = (float)this.Game.Window.ClientBounds.Height / 480;
        _camera = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _primitiveBatch = new PrimitiveBatch(this.GraphicsDevice);
    }

    public override void Draw(GameTime gameTime)
    {
        _primitiveBatch.Begin(null, null, null, null, _camera);
        // glLineWidth(3.0f); no command in MonoGame
        var defenseDangers = _aiPlayer.GetDefenseDangers();
        for (int i = 0; i < defenseDangers.Count; i++)
        {
            float danger = defenseDangers[i];
            Vector2 position = _aiPlayer.Level.DefenseSpots[i];
            _primitiveBatch.DrawCircle(position, 30, 24, new Color(danger, danger - 256, danger - 512));
        }

        var offenseWeaknesses = _aiPlayer.GetOffenseWeaknesses();
        for (int i = 0; i < offenseWeaknesses.Count; i++)
        {
            float weakness = offenseWeaknesses[i] * 1000f;
            Vector2 position = _aiPlayer.Level.OffenseSpots[i];
            _primitiveBatch.DrawCircle(position, 30, 24, new Color(weakness - 512, weakness - 256, weakness));
        }

        if (_aiPlayer.HasTarget)
        {
            _primitiveBatch.DrawLine(_aiPlayer.Level.TopMallet.Position, _aiPlayer.Target, Color.Black);
        }

        _primitiveBatch.End();
    }
}