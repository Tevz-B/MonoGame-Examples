using Artificial_I.Artificial.Mirage;
using Express.Graphics;
using Express.Scene;
using friHockey_v5.Graphics;
using friHockey_v5.Gui;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace friHockey_v5.GameStates.Menus;

public class Menu : GameState
{
    protected SimpleScene _scene;
    protected GuiRenderer _renderer;
    protected SpriteFont _retrotype, _fivexfive;
    protected Texture2D _buttonBackground;
    protected Button _back;

    protected Menu(Game theGame)
        : base (theGame)
    {
        _scene = new SimpleScene(Game);
        _renderer = new GuiRenderer(Game, _scene);
    }

    public override void Activate()
    {
        Game.Components.Add(_scene);
        Game.Components.Add(_renderer);
    }

    public override void Deactivate()
    {
        Game.Components.Remove(_scene);
        Game.Components.Remove(_renderer);
    }

    public override void Initialize()
    {
        _retrotype = Game.Content.Load<SpriteFont>("Retrotype");
        _fivexfive = Game.Content.Load<SpriteFont>("5x5");
        _fivexfive.LineSpacing = 14;
        _buttonBackground = Game.Content.Load<Texture2D>("Button");
        _back = new Button(new Rectangle(0, 428, 320, 32), null, _retrotype, "Back");
        _back.LabelColor = Color.White;
        _back.LabelHoverColor = Color.Gray;
        _back.Label.Position.X = 160;
        _back.Label.HorizontalAlign = HorizontalAlign.Center;
        base.Initialize();
    }

    public override void Update(GameTime gameTime)
    {
        Matrix inverseView = Matrix.Invert(_renderer.Camera);
        foreach (object item in _scene)
        {
            if (item is Button button)
            {
                button.UpdateWithInverseView(inverseView);
            }

        }
        if (_back.WasReleased)
        {
            _friHockey.PopState();
        }

    }

}