using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

public static class FileManager
{
    private static readonly string path = Application.persistentDataPath + "/GameInfo.txt";
    public static void SaveData(GameInfo gameInfo)
    {
        BinaryFormatter formatter = new BinaryFormatter();
        FileStream stream = new FileStream(path, FileMode.Create);
        formatter.Serialize(stream, gameInfo);
        stream.Close();
    }
    public static GameInfo LoadGameInfo()
    {
        if (File.Exists(path))
        {
            BinaryFormatter formatter = new BinaryFormatter();
            FileStream stream = new FileStream(path, FileMode.Open);
            GameInfo gameInfo = formatter.Deserialize(stream) as GameInfo;
            stream.Close();
            return gameInfo;
        }
        else
        {
            return new GameInfo(1, 0, 0, 1, 4, 1, false, false, 0);
        }
    }

    public static void DestroyData()
    {
        File.Delete(path);
    }

    public static bool CheckIfExistSavedData()
    {
        return File.Exists(path);
    }
}

