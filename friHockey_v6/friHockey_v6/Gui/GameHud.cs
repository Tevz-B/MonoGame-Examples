using Artificial_I.Artificial.Mirage;
using Express.Scene;
using friHockey_v6.Players;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace friHockey_v6.Gui;

public class GameHud : GameComponent
{
    protected SimpleScene _scene;
    protected Label[] _playerScore = new Label[2];
    protected Color[] _playerColor = new Color[2];
    protected float[] _playerOpacity = new float[2];
    protected PlayerPosition _lastScore;
    
    public IScene Scene => _scene;


    public GameHud(Game theGame)
        : base (theGame)
    {
        _scene = new SimpleScene(Game);
        Game.Components.Add(_scene);
    }

    public override void Initialize()
    {
        // FontTextureProcessor fontProcessor = new FontTextureProcessor();
        // SpriteFont font = Game.Content.Load<SpriteFont>("Retrotype"/*, fontProcessor*/);
        SpriteFont font = Game.Content.Load<SpriteFont>("Retrotype");
        _playerScore[(int)PlayerPosition.Top] = new Label(font, "0", new Vector2(50, 80));
        _playerScore[(int)PlayerPosition.Bottom] = new Label(font, "0", new Vector2(50, 380));
        for (int i = 0; i < 2; i++)
        {
            _playerColor[i] = Color.Transparent;
            _playerScore[i].Color = _playerColor[i];
            _playerScore[i].HorizontalAlign = HorizontalAlign.Center;
            _playerScore[i].VerticalAlign = VerticalAlign.Middle;
            _scene.Add(_playerScore[i]);
        }
    }

    public override void Update(GameTime gameTime)
    {
        float change = gameTime.ElapsedGameTime.Milliseconds / 5f;
        float sizeChange = gameTime.ElapsedGameTime.Milliseconds * 2f;
        for (int i = 0; i < 2; i++)
        {
            if (_playerOpacity[i] > 0)
            {
                _playerOpacity[i] -= change;
                _playerScore[i].Color = Color.Multiply(_playerColor[i], _playerOpacity[i]);
            }

        }

        if (_playerScore[(int)_lastScore].Scale.X > 0)
        {
            _playerScore[(int)_lastScore].Scale.X -= sizeChange;
            _playerScore[(int)_lastScore].Scale.Y -= sizeChange;
        }
    }

    public void ChangePlayerScoreForTo(PlayerPosition position, int value)
    {
        PlayerPosition other = (PlayerPosition)(((int)position + 1) % 2);
        _playerScore[(int)position].Text = $"{value}";
        _playerOpacity[(int)position] = 1;
        _playerColor[(int)position] = Color.Green;
        _playerScore[(int)position].SetScaleUniform(4);
        _playerOpacity[(int)other] = 1;
        _playerColor[(int)other] = Color.Red;
        _playerScore[(int)other].SetScaleUniform(2);
        _lastScore = position;
    }
}