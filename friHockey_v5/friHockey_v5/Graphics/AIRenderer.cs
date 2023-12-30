using Artificial_I.Artificial.Spectrum;
using friHockey_v5.Players.AI;
using Microsoft.Xna.Framework;

namespace friHockey_v5.Graphics;

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
        float scaleX = (float)Game.Window.ClientBounds.Width / 320;
        float scaleY = (float)Game.Window.ClientBounds.Height / 480;
        _camera = Matrix.CreateScale(new Vector3(scaleX, scaleY, 1));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _primitiveBatch = new PrimitiveBatch(GraphicsDevice);
    }

    public override void Draw(GameTime gameTime)
    {
        _primitiveBatch.Begin(null, null, null, null, _camera);
        // glLineWidth(3.0f); no command in XNA
        var defenseDangers = _aiPlayer.GetDefenseDangers();
        for (int i = 0; i < defenseDangers.Count; i++)
        {
            float danger = defenseDangers[i];
            Vector2 position = _aiPlayer.LevelBase.DefenseSpots[i];
            _primitiveBatch.DrawCircle(position, 30, 24, new Color(danger, danger - 256, danger - 512));
        }

        var offenseWeaknesses = _aiPlayer.GetOffenseWeaknesses();
        for (int i = 0; i < offenseWeaknesses.Count; i++)
        {
            float weakness = offenseWeaknesses[i] * 1000f;
            Vector2 position = _aiPlayer.LevelBase.OffenseSpots[i];
            _primitiveBatch.DrawCircle(position, 30, 24, new Color(weakness - 512, weakness - 256, weakness));
        }

        _primitiveBatch.DrawLine(_aiPlayer.LevelBase.TopMallet.Position, _aiPlayer.Target, Color.Black);

        _primitiveBatch.End();
    }
}