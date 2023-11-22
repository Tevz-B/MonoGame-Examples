using System;
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
        var args = new object[] {_level.TopMallet, _level.Scene, PlayerPosition.Top};
        _topPlayer = Activator.CreateInstance(aiClass, args) as Player; 
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
        _bottomPlayer.UpdateOrder = 0;
        _topPlayer.UpdateOrder = 1;
        _physics.UpdateOrder = 2;
        _level.UpdateOrder = 3;
        _level.Scene.UpdateOrder = 4;
        this.UpdateOrder = 5;
    }

    public override void Update(GameTime gameTime)
    {
        if (_level.Puck.Position.Y < -50)
        {
            _level.ResetToTop();
            _topPlayer.Reset();
            _bottomPlayer.Reset();
        }
        else if (_level.Puck.Position.Y > 510)
        {
            _level.ResetToBottom();
            _topPlayer.Reset();
            _bottomPlayer.Reset();
        }
    }
    
    
    
    
    ////////////////////////////
    public Gameplay(Game theGame, Class levelClass)
            : base (theGame)
        {
            this.StartInitWithLevelClass(levelClass);
            topPlayer = new HumanPlayer(this.Game, _level.TopMallet, PlayerPositionTop);
            bottomPlayer = new HumanPlayer(this.Game, _level.BottomMallet, PlayerPositionBottom);
            this.FinishInit();
        }

        public Gameplay(Game theGame, Class levelClass, Class aiClass)
            : base (theGame)
        {
            this.StartInitWithLevelClass(levelClass);
            topPlayer = new aiClass(this.Game, _level.TopMallet, _level, PlayerPositionTop);
            bottomPlayer = new HumanPlayer(this.Game, _level.BottomMallet, PlayerPositionBottom);
            AIRenderer aiRenderer = new AIRenderer(this.Game, (AIPlayer)topPlayer);
            aiRenderer.DrawOrder = 1;
            this.Game.Components.Add(aiRenderer);
            this.FinishInit();
        }

        public void StartInitWithLevelClass(Class levelClass)
        {
            _level = new levelClass(this.Game);
            this.Game.Components.Add(_level);
        }

        public void FinishInit()
        {
            this.Game.Components.Add(topPlayer);
            this.Game.Components.Add(bottomPlayer);
            _physics = new PhysicsEngine(this.Game, _level);
            _physics.UpdateOrder = 20;
            this.Game.Components.Add(_physics);
            _renderer = new GameRenderer(this.Game, _level);
            this.Game.Components.Add(_renderer);
            DebugRenderer debugRenderer = new DebugRenderer(this.Game, _level.Scene);
            debugRenderer.ItemColor = Color.Red();
            debugRenderer.MovementColor = Color.Gray();
            bottomPlayer.UpdateOrder = 0;
            topPlayer.UpdateOrder = 1;
            _physics.UpdateOrder = 2;
            _level.UpdateOrder = 3;
            _level.Scene.UpdateOrder = 4;
            this.UpdateOrder = 5;
        }

        void initialize()
        {
            base.initialize();
            if (topPlayer.IsKindOfClass(typeof(HumanPlayer)))
            {
                ((HumanPlayer)topPlayer).SetCamera(_renderer.Camera);
            }

            if (bottomPlayer.IsKindOfClass(typeof(HumanPlayer)))
            {
                ((HumanPlayer)bottomPlayer).SetCamera(_renderer.Camera);
            }

            if (Random.FloatM() < 0)
            {
                _level.ResetToTop();
            }
            else
            {
                _level.ResetToBottom();
            }

        }

        void UpdateWithGameTime(GameTime gameTime)
        {
            if (_level.Puck.Position.Y < -50)
            {
                _level.ResetToTop();
                topPlayer.Reset();
                bottomPlayer.Reset();
            }
            else if (_level.Puck.Position.Y > 510)
            {
                _level.ResetToBottom();
                topPlayer.Reset();
                bottomPlayer.Reset();
            }

        }

        void Dealloc()
        {
            this.Game.Components.RemoveComponent(_level);
            this.Game.Components.RemoveComponent(_renderer);
            this.Game.Components.RemoveComponent(_physics);
        }
}