using System.Collections.Generic;
using Express.Scene;
using Express.Scene.Objects;
using friHockey_v6.SceneObjects;
using Microsoft.Xna.Framework;
using Vector2 = Microsoft.Xna.Framework.Vector2;

namespace friHockey_v6.Level;

public class LevelBase : GameComponent
{
    
    protected SimpleScene _scene;
    protected Mallet _topMallet;
    protected Mallet _bottomMallet;
    protected Puck _puck;

    protected Vector2 _topMalletSpawn;
    protected Vector2 _bottomMalletSpawn;
    protected Vector2 _topPuckSpawn;
    protected Vector2 _bottomPuckSpawn;

    protected List<Vector2> _defenseSpots = new List<Vector2>();
    protected List<Vector2> _offenseSpots = new List<Vector2>();

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

    
    protected LevelBase(Game game) 
        : base(game)
    {
        _topMallet = new Mallet();
        _bottomMallet = new Mallet();
        _puck = new Puck();
        _scene = new SimpleScene(Game);
        _scene.Add(_topMallet);
        _scene.Add(_bottomMallet);
        _scene.Add(_puck);
    }

    public SimpleScene Scene
    {
        get => _scene;
        set => _scene = value;
    }

    public override void Initialize()
    {
        base.Initialize();
        Game.Components.Add(_scene);
    }

    public override void Update(GameTime gameTime)
    {
        foreach (var item in _scene)
        {
            var updateable = item as ICustomUpdate;
            updateable?.Update(gameTime);
        }
    }

    public void ResetToTop()
    {
        Reset();
        _puck.Position = _topPuckSpawn;
    }
    
    public void ResetToBottom()
    {
        Reset();
        _puck.Position = _bottomPuckSpawn;
    }

    protected void Reset()
    {
        _topMallet.Position  = _topMalletSpawn;
        _topMallet.ResetVelocity();
        _bottomMallet.Position  = _bottomMalletSpawn;
        _bottomMallet.ResetVelocity();

        _puck.Velocity = Vector2.Zero;
    }

}