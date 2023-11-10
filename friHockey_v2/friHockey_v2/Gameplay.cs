using System;
using friHockey_v2.Components;
using friHockey_v2.Physics;
using friHockey_v2.Players;
using friHockey_v2.Players.Human;
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
        _init(theGame, levelClass);
        UpdateOrder = 10;
        _topPlayer = new HumanPlayerKB(_level.TopMallet, _level.Scene, PlayerPosition.Top, Game);
        _bottomPlayer = new HumanPlayer(_level.BottomMallet, _level.Scene, PlayerPosition.Bottom, Game);
    }

    public Gameplay(Game theGame, Type levelClass, Type aiClass)
        : base (theGame)
    {
        _init(theGame, levelClass);
        var args = new object[] {_level.TopMallet, _level.Scene, PlayerPosition.Top};
        _topPlayer = Activator.CreateInstance(aiClass, args) as Player; 
        _bottomPlayer = new HumanPlayer(_level.BottomMallet, _level.Scene, PlayerPosition.Bottom, Game);
    }

    private void _init(Game game, Type levelClass)
    {
        var args = new object[] { game };
        _level = Activator.CreateInstance(levelClass, game) as Level;
        Game.Components.Add(_level);
        _renderer = new GameRenderer(game, _level);
        Game.Components.Add(_renderer);
        _physics = new PhysicsEngine(game, _level);
        _physics.UpdateOrder = 20;
        Game.Components.Add(_physics);
    }

    public override void Update(GameTime gameTime)
    {
        _topPlayer.Update(gameTime);
        _bottomPlayer.Update(gameTime);
    }
    //
    // void Dealloc()
    // {
    //     this.Game.Components.RemoveComponent(_level);
    //     this.Game.Components.RemoveComponent(_renderer);
    //     this.Game.Components.RemoveComponent(_physics);
    // }


}