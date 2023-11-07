using System.Collections;
using friHockey_v2.Scene.Objects;

namespace friHockey_v2.Scene;

public class Level
{
    
    protected ArrayList _scene;
    protected Mallet _topMallet;
    protected Mallet _bottomMallet;
    protected Puck _puck;

    protected Level()
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