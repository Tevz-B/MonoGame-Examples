using System;
using Artificial_I.Artificial.Mirage;
using friHockey_v5.Gui;
using friHockey_v5.Players.AI.Opponents;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace friHockey_v5.GameStates.Menus;

public class OpponentSelection : Menu
{
    protected Label _title;
    protected Button[] _opponentButton = new Button[(int)OpponentType.LastType];

    public OpponentSelection(Game game)
        : base(game)
    {
    }
    public override void Initialize()
    {
        base.Initialize();
        _title = new Label(_retrotype, "Select Opponent", new Vector2(160, 10));
        _title.HorizontalAlign = HorizontalAlign.Center;
        _scene.Add(_title);
        for (int i = 0; i < (int)OpponentType.LastType; i++)
        {
            _opponentButton[i] = new Button(new Rectangle(0, 60 + i * 80, 320, 80), null, _retrotype, "");
            _opponentButton[i].BackgroundHoverColor = Color.White;
            _opponentButton[i].LabelHoverColor = Color.Gray;
            _opponentButton[i].Label.Position.X = 90;
            _scene.Add(_opponentButton[i]);
        }

        _scene.Add(_back);
    }

    void Activate()
    {
        base.Activate();
        for (int i = 0; i < (int)OpponentType.LastType; i++)
        {
            Type opponentClass = FriHockey.GetOpponentClass(i);
            string portraitPath;
            if (FriHockey.Progress.IsOpponentUnlocked(i))
            {
                portraitPath = opponentClass.PortraitPath;
                _opponentButton[i].Label.Text = opponentClass.Name;
                _opponentButton[i].LabelColor = Color.White;
                _opponentButton[i].Enabled = true;
            }
            else
            {
                portraitPath = opponentClass.HiddenPortraitPath;
                _opponentButton[i].Label.Text = "???";
                _opponentButton[i].LabelColor = Color.DimGray;
                _opponentButton[i].Enabled = false;
            }

            Texture2D portrait = this.Game.Content.Load<Texture2D>(portraitPath);
            _opponentButton[i].BackgroundImage.Texture = portrait;
        }

    }

    public override void Update(GameTime gameTime)
    {
        base.Update(gameTime);
        for (int i = 0; i < (int)OpponentType.LastType; i++)
        {
            if (_opponentButton[i].WasReleased)
            {
                Type opponentClass = FriHockey.GetOpponentClass(i);
                Type levelClass = FriHockey.GetLevelClass(opponentClass.LevelType);
                Gameplay.Gameplay gameplay = new Gameplay.Gameplay(this.Game, levelClass, opponentClass);
                FriHockey.PushState(gameplay);
            }

        }

    }
}