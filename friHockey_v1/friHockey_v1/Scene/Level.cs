using System.Collections;
using friHockey_v1.Scene.Objects;

namespace friHockey_v1.Scene;

public class Level
{
    
    protected ArrayList _scene;
    protected Mallet _player1Mallet;
    protected Mallet _player2Mallet;
    protected Puck _puck;

    protected Level()
    {
        _player1Mallet = new Mallet();
        _player2Mallet = new Mallet();
        _puck = new Puck();
        _scene = new ArrayList();
        _scene.Add(_player1Mallet);
        _scene.Add(_player2Mallet);
        _scene.Add(_puck);
    }

    public ArrayList Scene
    {
        get => _scene;
        set => _scene = value;
    }

}