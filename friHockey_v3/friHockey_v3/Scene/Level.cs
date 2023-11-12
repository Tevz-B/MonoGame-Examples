using System.Collections;
using friHockey_v3.Scene.Objects;
using Microsoft.Xna.Framework;

namespace friHockey_v3.Scene;

public class Level : GameComponent
{
    
    protected ArrayList _scene;
    protected Mallet _topMallet;
    protected Mallet _bottomMallet;
    protected Puck _puck;

    public Mallet TopMallet => _topMallet;

    public Mallet BottomMallet => _bottomMallet;

    public Puck Puck => _puck;

    
    public Level(Game game) 
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