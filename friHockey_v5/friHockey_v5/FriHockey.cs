using System;
using System.Collections.Generic;
using Artificial_I.Artificial.Utils;
using friHockey_v5.Audio;
using friHockey_v5.GameStates;
using friHockey_v5.GameStates.Menus;
using friHockey_v5.Level;
using friHockey_v5.Level.Levels;
using friHockey_v5.Players.AI.Opponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace friHockey_v5;

public class FriHockey : Game
{
    private GraphicsDeviceManager _graphics;
    private Type[] _levelClasses;
    private Type[] _opponentClasses;
    private Stack<GameState> _stateStack;
    private GameProgress _progress;

    public GameProgress Progress => _progress;

    public FriHockey()
    {
        _graphics = new GraphicsDeviceManager(this);
        
        Content.RootDirectory = "Content";
        IsMouseVisible = true;

        Components.Add(new FpsComponent(this));

        // Init singletons
        SoundEngine.Init(this);

        _stateStack = new Stack<GameState>();
        _progress = GameProgress.LoadProgress();
    }

    public Type GetLevelType(LevelType type) => _levelClasses[(int)type];
    public Type GetOpponentType(OpponentType type) => _opponentClasses[(int)type];

    protected override void Initialize()
    {
        // Set resolution
        _graphics.PreferredBackBufferWidth = 640;
        _graphics.PreferredBackBufferHeight = 960;
        _graphics.ApplyChanges();

        // Add all level classes
        _levelClasses = new Type[(int)LevelType.LastType];

        _levelClasses[(int)LevelType.Hockey] = typeof(HockeyLevel);
        _levelClasses[(int)LevelType.Bullfrog] = typeof(BullfrogLevel);
        
        
        // Add all opponent classes
        _opponentClasses = new Type[(int)OpponentType.LastType];
        _opponentClasses[(int)OpponentType.Iceman] = typeof(Iceman);
        _opponentClasses[(int)OpponentType.Shaman] = typeof(Shaman);
        
        // Start in main menu
        PushState(new MainMenu(this));
        
        // Debug start in gameplay
        // PushState(new Gameplay(this, typeof(HockeyLevel), typeof(Iceman)));
        
        base.Initialize();
    }

    public void PushState(GameState gameState)
    {
        // Deactivate Current
        if (_stateStack.Count > 0)
        {
            GameState currentActiveState = _stateStack.Peek();
            currentActiveState.Deactivate();
            Components.Remove(currentActiveState);    
        }
        
        // Push new
        _stateStack.Push(gameState);
        Components.Add(gameState);
        gameState.Activate();
    }

    public void PopState()
    {
        // Pop top state
        GameState currentActiveState = _stateStack.Pop();
        currentActiveState.Deactivate();
        Components.Remove(currentActiveState);
        // Activate previous state
        currentActiveState = _stateStack.Peek();
        Components.Add(currentActiveState);
        currentActiveState.Activate();
    }

    protected override void Update(GameTime gameTime)
    {
        if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        base.Update(gameTime);
    }
    
    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.Black);
        base.Draw(gameTime);
    }
}