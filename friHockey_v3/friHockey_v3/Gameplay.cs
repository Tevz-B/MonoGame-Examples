using System;
using Artificial_I.Artificial.Utils;
using Express.Graphics;
using friHockey_v3.Graphics;
using friHockey_v3.Physics;
using friHockey_v3.Players;
using friHockey_v3.Players.AI;
using friHockey_v3.Players.Human;
using friHockey_v3.Scene;
using Microsoft.Xna.Framework;

namespace friHockey_v3;

public class Gameplay : GameComponent
{
    private Level _level;
    private Player _topPlayer;
    private Player _bottomPlayer;
    private GameRenderer _renderer;
    private PhysicsEngine _physics;
    
    
    private void _startInit(Type levelClass)
    {
        _level = Activator.CreateInstance(levelClass, Game) as Level;
        Game.Components.Add(_level);
        _renderer = new GameRenderer(Game, _level);
        Game.Components.Add(_renderer);
        _physics = new PhysicsEngine(Game, _level);
        _physics.UpdateOrder = 20;
        Game.Components.Add(_physics);
    }

    public Gameplay(Game theGame, Type levelClass)
        : base (theGame)
    {
        _startInit(levelClass);
        _topPlayer = new HumanPlayerKB(Game, _level.TopMallet, PlayerPosition.Top);
        _bottomPlayer = new HumanPlayer(Game, _level.BottomMallet,  PlayerPosition.Bottom);
        _finishInit();
    }

    public Gameplay(Game theGame, Type levelClass, Type aiClass)
        : base (theGame)
    {
        _startInit(levelClass);
        _topPlayer = Activator.CreateInstance(aiClass, Game, _level.TopMallet, _level, PlayerPosition.Top) as Player;
        _bottomPlayer = new HumanPlayer(Game, _level.BottomMallet, PlayerPosition.Bottom);
        AIRenderer aiRenderer = new AIRenderer(Game, (AIPlayer)_topPlayer);
        aiRenderer.DrawOrder = 1;
        Game.Components.Add(aiRenderer);
        _finishInit();
    }


    private void _finishInit()
    {
        Game.Components.Add(_topPlayer);
        Game.Components.Add(_bottomPlayer);
        
        _physics = new PhysicsEngine(this.Game, _level);
        _physics.UpdateOrder = 20;
        this.Game.Components.Add(_physics);
        
        _renderer = new GameRenderer(this.Game, _level);
        this.Game.Components.Add(_renderer);
        
        DebugRenderer debugRenderer = new DebugRenderer(this.Game, _level.Scene);
        debugRenderer.ItemColor = Color.Red;
        debugRenderer.MovementColor = Color.Gray;
        Game.Components.Add(debugRenderer);

        FpsComponent fpsComponent = new FpsComponent(Game);
        Game.Components.Add(fpsComponent);
        
        _bottomPlayer.UpdateOrder = 0;
        _topPlayer.UpdateOrder = 1;
        _physics.UpdateOrder = 2;
        _level.UpdateOrder = 3;
        _level.Scene.UpdateOrder = 4;
        this.UpdateOrder = 5;
    }

    public override void Initialize()
    {
        base.Initialize();

        if (_topPlayer is HumanPlayer playerT)
        {
            playerT.SetCamera(_renderer.Camera);
        }
        if (_bottomPlayer is HumanPlayer playerB)
        {
            playerB.SetCamera(_renderer.Camera);
        }

        if (SRandom.Float() > 0.5f)
            _level.ResetToTop();
        else
            _level.ResetToBottom();

    }

    public override void Update(GameTime gameTime)
    {
        switch (_level.Puck.Position.Y)
        {
            case < -50:
                _level.ResetToTop();
                _topPlayer.Reset();
                _bottomPlayer.Reset();
                break;
            case > 510:
                _level.ResetToBottom();
                _topPlayer.Reset();
                _bottomPlayer.Reset();
                break;
        }
    }
}