using Artificial_I.Artificial.Mirage;
using friHockey_v6.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace friHockey_v6.GameStates.Menus;

public class MainMenu : Menu
{
    protected Image _table, _duke;
    protected Label _title, _subtitle, _copyright;
    protected Button _singleplayer, _multiplayer, _options;

    public MainMenu(Game game)
        : base(game)
    {
    }


    public override void Initialize()
    {
        base.Initialize();
        
        // Background
        Texture2D tableTexture = Game.Content.Load<Texture2D>("TablePlain");
        _table = new Image(tableTexture, new Vector2(-60, 0));
        _table.SetScaleUniform(2);
        _scene.Add(_table);
        
        Texture2D dukeTexture = Game.Content.Load<Texture2D>("TheDuke");
        _duke = new Image(dukeTexture, new Vector2(0, 0));
        _duke.SetScaleUniform(2);
        _scene.Add(_duke);
        
        
        // Title
        _title = new Label(_retrotype, "friHockey", new Vector2(160, 10));
        _title.HorizontalAlign = HorizontalAlign.Center;
        _scene.Add(_title);
        
        _subtitle = new Label(_fivexfive, "by Matej Jan", new Vector2(320, 50));
        _subtitle.HorizontalAlign = HorizontalAlign.Right;
        _scene.Add(_subtitle);
        
        _copyright = new Label(_fivexfive,  "3D modeling by\nMatjaz Lamut\nPublished by GameTeam, Fri\nCopyright 2011 Razum d.o.o.\nAll Rights Reserved v0.5", new Vector2(4, 462));
        _copyright.VerticalAlign = VerticalAlign.Bottom;
        _scene.Add(_copyright);
        
        // Singleplayer
        _singleplayer = new Button(new Rectangle(180, 150, 140, 32), _buttonBackground, _retrotype, "Faculty");
        _singleplayer.BackgroundImage.SetScaleUniform(2);
        _scene.Add(_singleplayer);
        // Multiplayer
        _multiplayer = new Button(new Rectangle(180, 200, 140, 32), _buttonBackground, _retrotype, "Versus");
        _multiplayer.BackgroundImage.SetScaleUniform(2);
        _scene.Add(_multiplayer);
        // Options
        _options = new Button(new Rectangle(180, 250, 140, 32), _buttonBackground, _retrotype, "Restroom");
        _options.BackgroundImage.SetScaleUniform(2);
        _scene.Add(_options);
    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        GameState newState = null;
        if (_singleplayer.WasReleased)
        {
            newState = new OpponentSelection(Game);
        }
        else if (_multiplayer.WasReleased)
        {
            newState = new LevelSelection(Game);
        }
        else if (_options.WasReleased)
        {
            newState = new Options(Game);
        }

        if (newState is not null)
        {
            _friHockey.PushState(newState);
        }
    }
}