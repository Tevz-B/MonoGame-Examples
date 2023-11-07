using System;
using friHockey_v2.Scene;
using Microsoft.Xna.Framework;

namespace friHockey_v2;

public class Gameplay : GameComponent
{
    private Level _level;
    private Player _topPlayer;
    private Player _bottomPlayer;
    private GameRenderer _renderer;
    private PhysicsEngine _physics;

    public Gameplay(Game theGame, Type levelClass)
        : base (theGame)
    {
        this._init(theGame, levelClass);
        this.UpdateOrder = 10;
        _topPlayer = new HumanPlayer(_level.TopMallet, _level.Scene, PlayerPosition.Top, this.Game);
        _bottomPlayer = new HumanPlayer(_level.BottomMallet, _level.Scene, PlayerPosition.Bottom, this.Game);
    }

    public Gameplay(Game theGame, Type levelClass, Type aiClass)
        : base (theGame)
    {
        this._init(theGame, levelClass);
        _topPlayer = new typeof(aiClass) (_level.TopMallet, _level.Scene, PlayerPosition.Top);
        _bottomPlayer = new HumanPlayer(_level.BottomMallet, _level.Scene, PlayerPosition.Bottom, this.Game);
    }

    private void _init(Game theGame, Type levelClass)
    {
        _level = new levelClass(this.Game);
        this.Game.Components.AddComponent(_level);
        _renderer = new GameRenderer(this.Game, _level);
        this.Game.Components.AddComponent(_renderer);
        _physics = new PhysicsEngine(this.Game, _level);
        _physics.UpdateOrder = 20;
        this.Game.Components.AddComponent(_physics);
    }

    void UpdateWithGameTime(GameTime gameTime)
    {
        _topPlayer.UpdateWithGameTime(gameTime);
        _bottomPlayer.UpdateWithGameTime(gameTime);
    }

    void Dealloc()
    {
        this.Game.Components.RemoveComponent(_level);
        this.Game.Components.RemoveComponent(_renderer);
        this.Game.Components.RemoveComponent(_physics);
    }


}