using System;
using System.IO;
using System.Net;
using System.Text.Json;
using friHockey_v5.Players.AI.Opponents;
using friHockey_v5.Scene;

namespace friHockey_v5;

public class GameProgress
{

    private SaveData _saveData;

    public GameProgress()
    {
        _saveData = new SaveData
        {
            LevelUnlocked =
            {
                [(int)OpponentType.Iceman] = true
            },
            OpponentUnlocked =
            {
                [(int)LevelType.Hockey] = true
            }
        };
    }

    // private static SaveData NewGameSaveData()
    // {
    //     return new SaveData
    //     {
    //         LevelUnlocked =
    //         {
    //             [(int)OpponentType.Iceman] = true
    //         },
    //         OpponentUnlocked =
    //         {
    //             [(int)LevelType.Hockey] = true
    //         }
    //     };
    // }

    // public GameProgress(NSCoder aDecoder)
    // {
    //     for (int i = 0; i < (int)LevelType.LastType; i++)
    //     {
    //         _levelUnlocked[i] = aDecoder.DecodeBoolForKey($"levelUnlocked{i}");
    //     }
    //
    //     for (int i = 0; i < (int)OpponentType.LastType; i++)
    //     {
    //         _opponentUnlocked[i] = aDecoder.DecodeBoolForKey($"opponentUnlocked{i}");
    //     }
    //
    // }

    // void EncodeWithCoder(NSCoder aCoder)
    // {
    //     for (int i = 0; i < (int)LevelType.LastType; i++)
    //     {
    //         aCoder.EncodeBoolForKey(_levelUnlocked[i], $"levelUnlocked{i}");
    //     }
    //
    //     for (int i = 0; i < (int)OpponentType.LastType; i++)
    //     {
    //         aCoder.EncodeBoolForKey(_opponentUnlocked[i], $"opponentUnlocked{i}");
    //     }
    //
    // }

    public static GameProgress LoadProgress()
    {
        string serializedData = File.ReadAllText(Constants.ProgressFilePath);
        var saveData = JsonSerializer.Deserialize<SaveData>(serializedData);

        if (saveData is null)
            return new GameProgress();

        return new GameProgress
        {
            _saveData = saveData
        };

        // GameProgress progress = null;
        // string rootPath = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, true).ObjectAtIndex(0);
        // string archivePath = rootPath.StringByAppendingPathComponent(Constants.ProgressFilePath());
        // progress = NSKeyedUnarchiver.UnarchiveObjectWithFile(archivePath);
        // if (!progress)
        // {
        //     progress = new GameProgress();
        // }
        //
        // Console.WriteLine("Progress retain count: %d", progress.RetainCount());
        // return progress;
    }

    public static void DeleteProgress()
    {
        File.Delete(Constants.ProgressFilePath);
        
        // string rootPath = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, true).ObjectAtIndex(0);
        // string archivePath = rootPath.StringByAppendingPathComponent(Constants.ProgressFilePath());
        // NSError error;
        // NSFileManager.DefaultManager().RemoveItemAtPathError(archivePath, error);
    }

    public void SaveProgress()
    {
        string serializedData = JsonSerializer.Serialize<SaveData>(_saveData);
        File.WriteAllText(Constants.ProgressFilePath, serializedData);
        
        // string rootPath = NSSearchPathForDirectoriesInDomains(NSDocumentDirectory, NSUserDomainMask, true).ObjectAtIndex(0);
        // string archivePath = rootPath.StringByAppendingPathComponent(Constants.ProgressFilePath());
        // NSKeyedArchiver.ArchiveRootObjectToFile(this, archivePath);
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
        this.SaveProgress();
    }

    public void UnlockOpponent(OpponentType type)
    {
        _saveData.OpponentUnlocked[(int)type] = true;
        this.SaveProgress();
    }
}