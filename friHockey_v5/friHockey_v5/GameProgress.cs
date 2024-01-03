using System;
using System.IO;
using System.Text.Json;
using friHockey_v5.Level;
using friHockey_v5.Players.AI.Opponents;

namespace friHockey_v5;

public class GameProgress
{
    private SaveData _saveData = new();

    public static GameProgress LoadProgress()
    {
        SaveData saveData = null;
        if (File.Exists(Constants.ProgressFilePath))
        {
            string serializedData = File.ReadAllText(Constants.ProgressFilePath);
            try
            {
                saveData = JsonSerializer.Deserialize<SaveData>(serializedData);
            }
            catch (JsonException)
            {
                Console.WriteLine("Game progress parse failed. Create new game.");
            }
            
        }
        else
        {
            File.Create(Constants.ProgressFilePath);
        }

        if (saveData is null)
            return new GameProgress();
            
        return new GameProgress
        {
            _saveData = saveData
        };
    }

    public static void DeleteProgress()
    {
        File.Delete(Constants.ProgressFilePath);
    }

    public void SaveProgress()
    {
        string serializedData = JsonSerializer.Serialize<SaveData>(_saveData);
        File.WriteAllText(Constants.ProgressFilePath, serializedData);
    }

    public bool IsLevelUnlocked(LevelType type)
    {
        return _saveData.LevelUnlocked[(int)type];
    }

    public bool IsOpponentUnlocked(OpponentType type)
    {
        return _saveData.OpponentUnlocked[(int)type];
    }

    public void UnlockLevel(LevelType type)
    {
        _saveData.LevelUnlocked[(int)type] = true;
        SaveProgress();
    }

    public void UnlockOpponent(OpponentType type)
    {
        _saveData.OpponentUnlocked[(int)type] = true;
        SaveProgress();
    }
}