using Microsoft.Xna.Framework;

namespace friHockey_v2.Scene.Levels;

public class HockeyLevel : Level
{
    public HockeyLevel()
    {
        _player1Mallet.Position = new Vector2(200, 100);
        _player2Mallet.Position = new Vector2(300, 980);
        _puck.Position = new Vector2(400, 400);
    }
    
}
