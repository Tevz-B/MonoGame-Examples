
using friHockey_v5.Level;
using friHockey_v5.Players.AI.Opponents;

namespace friHockey_v5;

public class SaveData
{
    private bool[] _levelUnlocked = new bool[(int)LevelType.LastType];
    private bool[] _opponentUnlocked = new bool[(int)OpponentType.LastType];
    
    public bool[] LevelUnlocked { get; set; }
    public bool[] OpponentUnlocked { get; set; }

    public SaveData()
    {
        _levelUnlocked[(int)LevelType.Hockey] = true;
        _opponentUnlocked[(int)OpponentType.Iceman] = true;
    }
}