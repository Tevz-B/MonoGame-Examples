using System;
using Artificial_I.Artificial.Utils;
using friHockey_v6.Audio;
using friHockey_v6.Graphics;
using friHockey_v6.Gui;
using friHockey_v6.Level;
using friHockey_v6.Physics;
using friHockey_v6.Players;
using friHockey_v6.Players.AI;
using friHockey_v6.Players.AI.Opponents;
using friHockey_v6.Players.Human;
using Microsoft.Xna.Framework;

namespace friHockey_v6.GameStates.Gameplay;

public class Gameplay : GameState
{
    private LevelBase _level;
    private Player[] _players = new Player[2];
    private int[] _score = new int[2]; 
    private GameHud _hud;
    // private GameRenderer _renderer;
    private GameRenderer3D _renderer;
    private GuiRenderer _hudRenderer;
    private PhysicsEngine _physics;

    public int[] Score => _score;
    
    // debug 
    private FpsComponent _fpsComponent;
        
    
    private void _startInit(Type levelClass)
    {
        _level = Activator.CreateInstance(levelClass, Game) as LevelBase;
    }

    public Gameplay(Game theGame, Type levelClass)
        : base (theGame)
    {
        _startInit(levelClass);
        
        // Create two human players
        _players[(int)PlayerPosition.Top] = new HumanPlayerKb(Game, _level.TopMallet, PlayerPosition.Top);
        _players[(int)PlayerPosition.Bottom] = new HumanPlayer(Game, _level.BottomMallet,  PlayerPosition.Bottom);
        
        _finishInit();
    }

    public Gameplay(Game theGame, Type levelClass, Type aiClass)
        : base (theGame)
    {
        _startInit(levelClass);
        
        // Crate human and AI
        _players[(int)PlayerPosition.Top] = Activator.CreateInstance(aiClass, Game, _level.TopMallet, _level, PlayerPosition.Top) as Player;
        _players[(int)PlayerPosition.Bottom] = new HumanPlayer(Game, _level.BottomMallet, PlayerPosition.Bottom);
        
        AIRenderer aiRenderer = new AIRenderer(Game, (AIPlayer)_players[(int)PlayerPosition.Top]);
        aiRenderer.DrawOrder = 1;
        _finishInit();
    }


    private void _finishInit()
    {
        _physics = new PhysicsEngine(Game, _level);
        _physics.UpdateOrder = 20;
        
        _renderer = new GameRenderer3D(Game, _level);
        
        // DebugRenderer debugRenderer = new DebugRenderer(Game, _level.Scene);
        // debugRenderer.ItemColor = Color.Red;
        // debugRenderer.MovementColor = Color.Gray;
        // debugRenderer.DrawOrder = 2;
        // // Game.Components.Add(debugRenderer);

        _fpsComponent = new FpsComponent(Game);
        
        _hud = new GameHud(Game);
        _hudRenderer = new GuiRenderer(Game, _hud.Scene);
        _hudRenderer.DrawOrder = 1;
        
        _players[(int)PlayerPosition.Bottom].UpdateOrder = 0;
        _players[(int)PlayerPosition.Top].UpdateOrder = 1;
        _physics.UpdateOrder = 2;
        _level.UpdateOrder = 3;
        _level.Scene.UpdateOrder = 4;
        UpdateOrder = 5;
    }
    
    
    public override void Activate()
    {
        Game.Components.Add(_level);
        Game.Components.Add(_hud);
        Game.Components.Add(_hudRenderer);
        Game.Components.Add(_renderer);
        Game.Components.Add(_physics);
        Game.Components.Add(_players[(int)PlayerPosition.Top]);
        Game.Components.Add(_players[(int)PlayerPosition.Bottom]);
        
        // Debug 
        Game.Components.Add(_fpsComponent);
        
        // Change resolution to landscape
        _friHockey.Graphics.PreferredBackBufferWidth = 960;
        _friHockey.Graphics.PreferredBackBufferHeight = 640;
        _friHockey.Graphics.ApplyChanges();
    }
    
    public override void Deactivate()
    {
        Game.Components.Remove(_hud);
        Game.Components.Remove(_hudRenderer);
        Game.Components.Remove(_level);
        Game.Components.Remove(_renderer);
        Game.Components.Remove(_physics);
        Game.Components.Remove(_players[(int)PlayerPosition.Top]);
        Game.Components.Remove(_players[(int)PlayerPosition.Bottom]);
        
        // Debug 
        Game.Components.Remove(_fpsComponent);
        
        // Change resolution back to portrait
        _friHockey.Graphics.PreferredBackBufferWidth = 640;
        _friHockey.Graphics.PreferredBackBufferHeight = 960;
        _friHockey.Graphics.ApplyChanges();
    }

    public override void Initialize()
    {
        base.Initialize();

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
                PlayerScores(PlayerPosition.Bottom);
                break;
            case > 510:
                PlayerScores(PlayerPosition.Top);
                break;
        }
    }

    public void PlayerScores(PlayerPosition position)
    {
        if (position == PlayerPosition.Top)
        {
            SoundEngine.Play(SoundEffectType.Lose);
            _level.ResetToBottom();
        }
        else
        {
            SoundEngine.Play(SoundEffectType.Win);
            _level.ResetToTop();
        }

        _players[(int)PlayerPosition.Top].Reset();
        _players[(int)PlayerPosition.Bottom].Reset();
        _score[(int)position]++;
        _hud.ChangePlayerScoreForTo(position, _score[(int)position]);
        if (_score[(int)position] >= Constants.WinScore)
        {
            if (position == PlayerPosition.Bottom && _players[(int)PlayerPosition.Top] is AIPlayer opponent)
            {
                LevelType levelType = opponent.GetLevelType();
                _friHockey.Progress.UnlockLevel(levelType);
                OpponentType opponentType = opponent.OpponentType + 1;
                if (opponentType < OpponentType.LastType)
                {
                    _friHockey.Progress.UnlockOpponent(opponentType);
                }
            }
            _friHockey.PopState();
        }

    }


    
}