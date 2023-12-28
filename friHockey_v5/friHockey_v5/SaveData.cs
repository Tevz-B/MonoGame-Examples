
using friHockey_v5.Players.AI.Opponents;
using friHockey_v5.Scene;

namespace friHockey_v5;

public class SaveData
{
    private bool[] _levelUnlocked = new bool[(int)LevelType.LastType];
    private bool[] _opponentUnlocked = new bool[(int)OpponentType.LastType];
    
    public bool[] LevelUnlocked { get; set; }
    public bool[] OpponentUnlocked { get; set; }
}