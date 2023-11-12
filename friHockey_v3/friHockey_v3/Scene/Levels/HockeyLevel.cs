using Microsoft.Xna.Framework;

namespace friHockey_v3.Scene.Levels;

public class HockeyLevel : Level
{
    public HockeyLevel(Game game)
        : base(game)
    {
        _topMallet.Position = new Vector2(200, 100);
        _bottomMallet.Position = new Vector2(300, 700);
        _puck.Position = new Vector2(400, 400);
    }
    
}
