using Breakout.Graphics;
using Breakout.Physics;
using Breakout.Scene;
using Express.Graphics;
using Microsoft.Xna.Framework;

namespace Breakout;

public class Gameplay : GameComponent
{
    protected Level _level;
    protected Player.Player _player;
    protected PhysicsEngine _physics;
    protected Renderer _renderer;
    protected DebugRenderer _debugRenderer;
    protected int _lives;
    protected int _difficultyLevel;

    public Gameplay(Game theGame)
        : base(theGame)
    {
        _level = new Level(Game);
        _player = new Player.Player(Game, _level.Paddle);
        _physics = new PhysicsEngine(Game, _level);
        _renderer = new Renderer(Game, this);
        _debugRenderer = new DebugRenderer(Game, _level.Scene);
        
        _player.UpdateOrder = 0;
        _physics.UpdateOrder = 1;
        _level.UpdateOrder = 2;
        UpdateOrder = 3;
        
        Game.Components.Add(_level);
        Game.Components.Add(_player);
        Game.Components.Add(_physics);
        Game.Components.Add(_renderer);
        // Game.Components.Add(_debugRenderer);
    }

    public Level Level => _level;

    public int Lives => _lives;

    public override void Initialize()
    {
        _debugRenderer.ColliderColor = Color.Black;
        _debugRenderer.MovementColor = Color.Blue;
        _debugRenderer.ItemColor = Color.Red;
        _debugRenderer.TransformMatrix = _renderer.Camera;
        _player.SetCamera(_renderer.Camera);
        Reset();
        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        if (_level.BallsCount == 0)
        {
            
        }

        if (_level.BricksCount == 0)
        {
            _difficultyLevel++;
            _lives++;
            _level.ResetLevel(CalculateCurrentBallSpeed());
        }
    }

    public void Reset()
    {
        _lives = Constants.StartLives;
        _difficultyLevel = 0;
        _level.ResetLevel(CalculateCurrentBallSpeed());
    }

    public float CalculateCurrentBallSpeed()
    {
        return Constants.InitialBallSpeed +
               _difficultyLevel * Constants.LevelUpBallSpeedIncrease;
    }
}