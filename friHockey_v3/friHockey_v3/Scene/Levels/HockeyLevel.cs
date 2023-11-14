using friHockey_v3.Scene.Objects.Walls;
using Microsoft.Xna.Framework;

namespace friHockey_v3.Scene.Levels;

public class HockeyLevel : Level
{
    public HockeyLevel(Game game)
        : base(game)
    {
        // Position
        _topMallet.Position = new Vector2(200, 100);
        _bottomMallet.Position = new Vector2(300, 700);
        _puck.Position = new Vector2(400, 400);
        // Spawn
        _topMalletSpawn = new Vector2(160, 60);
        _bottomMalletSpawn = new Vector2(160, 400);
        _topPuckSpawn = new Vector2(160, 150);
        _bottomPuckSpawn = new Vector2(160, 310);
        
        // AI Help
        _defenseSpots.Add(new Vector2(160, 0));
        _offenseSpots.Add(new Vector2(160, 460));
        _offenseSpots.Add(new Vector2(130, 460));
        _offenseSpots.Add(new Vector2(190, 460));
        
        // Bounds
        var wall = new RectangleWall {Position = new Vector2(-5, 230), Width = 30, Height = 480};
        _scene.Add(wall);
        wall = new RectangleWall {Position = new Vector2(325, 230), Width = 30, Height = 480};
        _scene.Add(wall);
        wall = new RectangleWall {Position = new Vector2(40, -5), Width = 100, Height = 30};
        _scene.Add(wall);
        wall = new RectangleWall {Position = new Vector2(280, -5), Width = 100, Height = 30};
        _scene.Add(wall);
        wall = new RectangleWall {Position = new Vector2(40, 465), Width = 100, Height = 30};
        _scene.Add(wall);
        wall = new RectangleWall {Position = new Vector2(280, 465), Width = 100, Height = 30};
        _scene.Add(wall);
    }
    
}
