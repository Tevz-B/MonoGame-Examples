using System.Collections;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace friHockey_v3.Graphics;

public class AIRenderer : DrawableGameComponent
{
    protected PrimitiveBatch primitiveBatch;
    protected AIPlayer aiPlayer;
    protected Matrix camera;

    public AIRenderer(Game theGame, AIPlayer theAIPlayer)
        : base (theGame)
    {
        aiPlayer = theAIPlayer;
    }

    public override void Initialize()
    {
        float scaleX = (float)this.Game.GameWindow.ClientBounds.Width / 320;
        float scaleY = (float)this.Game.GameWindow.ClientBounds.Height / 480;
        camera = Matrix.CreateScale(Vector3.VectorWithXYZ(scaleX, scaleY, 1));
        base.Initialize();
    }

    protected override void LoadContent()
    {
        primitiveBatch = new PrimitiveBatch(this.GraphicsDevice);
    }

    public override void Draw(GameTime gameTime)
    {
        primitiveBatch.BeginWithBlendStateDepthStencilStateRasterizerStateEffectTransformMatrix(null, null, null, null, camera);
        glLineWidth(3.0f);
        ArrayList defenseDangers = aiPlayer.GetDefenseDangers();
        for (int i = 0; i < defenseDangers.Count; i++)
        {
            float danger = ((NSNumber)defenseDangers[i]).FloatValue();
            Vector2 position = aiPlayer.Level.DefenseSpots.ObjectAtIndex(i);
            primitiveBatch.DrawCircleAtRadiusDivisionsColor(position, 30, 24, Color.ColorWithRedGreenBlue(danger, danger - 256, danger - 512));
        }

        ArrayList offenseWeaknesses = aiPlayer.GetOffenseWeaknesses();
        for (int i = 0; i < offenseWeaknesses.Count; i++)
        {
            float weakness = ((NSNumber)offenseWeaknesses[i]).FloatValue() * 1000;
            Vector2 position = aiPlayer.Level.OffenseSpots.ObjectAtIndex(i);
            primitiveBatch.DrawCircleAtRadiusDivisionsColor(position, 30, 24, Color.ColorWithRedGreenBlue(weakness - 512, weakness - 256, weakness));
        }

        if (aiPlayer.Target)
        {
            primitiveBatch.DrawLineFromToColor(aiPlayer.Level.TopMallet.Position, aiPlayer.Target, Color.Black());
        }

        primitiveBatch.End();
    }
}