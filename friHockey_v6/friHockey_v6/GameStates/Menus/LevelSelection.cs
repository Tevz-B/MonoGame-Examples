using Artificial_I.Artificial.Mirage;
using Microsoft.Xna.Framework;

namespace friHockey_v6.GameStates.Menus;

public class LevelSelection : Menu
{
    protected Label _title;

    public LevelSelection(Game game)
        : base(game)
    {
    }
    public override void Initialize()
    {
        base.Initialize();
        _title = new Label(_retrotype, "Select Level", new Vector2(160, 10));
        _title.HorizontalAlign = HorizontalAlign.Center;
        _scene.Add(_title);
        _scene.Add(_back);
    }

}