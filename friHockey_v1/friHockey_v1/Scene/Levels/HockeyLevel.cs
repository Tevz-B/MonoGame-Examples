using Microsoft.Xna.Framework;

namespace friHockey_v1.Scene.Levels;

public class HockeyLevel : Level
{
    public HockeyLevel()
    {
        _player1Mallet.Position = new Vector2(200, 320);
        _player2Mallet.Position = new Vector2(1080, 320);
        _puck.Position = new Vector2(500, 320);
    }
    
}
