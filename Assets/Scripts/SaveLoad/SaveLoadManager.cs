using UnityEngine;
using SaveDataVC = SaveDataV5;
using Newtonsoft.Json;
using System.IO;

public static class SaveLoadManager
{
    public enum SaveMode
    {
        Text,
        Encrypted,
    }

    public static SaveMode Mode { get; set; } = SaveMode.Text;

    private static readonly string SaveDirectory = $"{Application.persistentDataPath}/Save";

    private static readonly string[] SaveFileNames =
    {
        "SaveAuto",
        "Save1",
        "Save2",
        "Save3"
    };

    private static string GetSaveFilePath(int slot)
    {
        return GetSaveFilePath(slot, Mode);
    }

    private static string GetSaveFilePath(int slot, SaveMode mode)
    {
        string ext = mode == SaveMode.Text ? ".json" : ".dat";
        return Path.Combine(SaveDirectory, $"{SaveFileNames[slot]}{ext}");
    }

    public static int SaveDataVersion { get; } = 5;
    public static SaveDataVC Data { get; set; } = new();

    static SaveLoadManager()
    {
        if (!Load())
        {
            Debug.LogError("세이브 파일 로드 실패!");
        }
    }

    private static JsonSerializerSettings settings = new()
    {
        Formatting = Formatting.Indented,
        TypeNameHandling = TypeNameHandling.All,
    };

    public static bool Save(int slot = 0)
    {
        return Save(slot, Mode);
    }

    public static bool Save(int slot, SaveMode mode)
    {
        if(Data == null || slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }

        try
        {
            if (!Directory.Exists(SaveDirectory))
            {
                Directory.CreateDirectory(SaveDirectory);
            }

            string path = GetSaveFilePath(slot, mode);
            var json = JsonConvert.SerializeObject(Data, settings);

            switch (mode)
            {
                case SaveMode.Text:
                    File.WriteAllText(path, json);
                    return true;
                case SaveMode.Encrypted:
                    File.WriteAllBytes(path, CryptoUtil.Encrypt(json));
                    return true;
                default:
                    return false;
            }

            
        }
        catch
        {
            Debug.LogError("Save 예외");
            return false;
        }

    }

    public static bool Load(int slot = 0)
    {
        return Load(slot = 0, Mode);
    }

    public static bool Load(int slot, SaveMode mode)
    {
        if (slot < 0 || slot >= SaveFileNames.Length)
        {
            return false;
        }

        string path = GetSaveFilePath(slot, mode);
        
        if (File.Exists(path) == false)
        {
            return Save();
        }

        try
        {
            string json = string.Empty;
            switch (mode)
            {
                case SaveMode.Text:
                    json = File.ReadAllText(path);
                    break;
                case SaveMode.Encrypted:
                    json = CryptoUtil.Decrypt(File.ReadAllBytes(path));
                    break;
            }
            var saveData = JsonConvert.DeserializeObject<SaveData>(json, settings);
            while(saveData.Version < SaveDataVersion)
            {
                saveData = saveData.VersionUp();
            }
            Data = saveData as SaveDataVC;
            return true;
        }
        catch
        {
            Debug.LogError("Load 예외");
            return false;
        }
    }
}