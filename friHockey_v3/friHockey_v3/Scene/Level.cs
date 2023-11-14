using System.Collections;
using System.Collections.Generic;
using friHockey_v3.Scene.Objects;
using Microsoft.Xna.Framework;

namespace friHockey_v3.Scene;

public class Level : GameComponent
{
    
    protected ArrayList _scene;
    protected Mallet _topMallet;
    protected Mallet _bottomMallet;
    protected Puck _puck;

    protected Vector2 _topMalletSpawn;
    protected Vector2 _bottomMalletSpawn;
    protected Vector2 _topPuckSpawn;
    protected Vector2 _bottomPuckSpawn;

    protected List<Vector2> _defenseSpots;
    protected List<Vector2> _offenseSpots;

    public List<Vector2> DefenseSpots
    {
        get => _defenseSpots;
        set => _defenseSpots = value;
    }

    public List<Vector2> OffenseSpots
    {
        get => _offenseSpots;
        set => _offenseSpots = value;
    }

    public Mallet TopMallet => _topMallet;
    public Mallet BottomMallet => _bottomMallet;
    public Puck Puck => _puck;

    
    protected Level(Game game) 
        : base(game)
    {
        _topMallet = new Mallet();
        _bottomMallet = new Mallet();
        _puck = new Puck();
        _scene = new ArrayList
        {
            _topMallet,
            _bottomMallet,
            _puck
        };
    }

    public ArrayList Scene
    {
        get => _scene;
        set => _scene = value;
    }

}