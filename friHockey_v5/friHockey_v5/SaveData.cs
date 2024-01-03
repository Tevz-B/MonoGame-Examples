
using System;
using System.Linq;
using System.Text.Json;
using friHockey_v5.Level;
using friHockey_v5.Players.AI.Opponents;

namespace friHockey_v5;

[Serializable]
public class SaveData
{
    private bool[] _levelUnlocked = new bool[(int)LevelType.LastType];
    private bool[] _opponentUnlocked = new bool[(int)OpponentType.LastType];
    
    public bool[] LevelUnlocked { get => _levelUnlocked; set => _levelUnlocked = value; }
    public bool[] OpponentUnlocked { get => _opponentUnlocked; set => _opponentUnlocked = value; }

    public SaveData()
    {
        LevelUnlocked[(int)LevelType.Hockey] = true;
        OpponentUnlocked[(int)OpponentType.Iceman] = true;
    }

    public static SaveData UnlockedEverything()
    {
        var ret = new SaveData();
        
        for (int i = 0; i < (int)LevelType.LastType; i++)
        {
            ret.LevelUnlocked[i] = true;
        }
        
        for (int i = 0; i < (int)OpponentType.LastType; i++)
        {
            ret.OpponentUnlocked[i] = true;
        }
        
        return ret;
    }
}