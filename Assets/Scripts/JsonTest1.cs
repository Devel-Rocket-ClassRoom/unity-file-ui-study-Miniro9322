using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;

[Serializable]
public class PlayerState
{
    public string playerName;
    public int lives;
    public float health;
    //[JsonConverter(typeof(Vector3Converter))] 2번 컨버트
    public Vector3 position;

    public override string ToString()
    {
        return $"{playerName} / {lives} / {health}";
    }
}

public class JsonTest1 : MonoBehaviour
{
    private JsonSerializerSettings jsonSetting;

    private void Awake()
    {
        jsonSetting = new();
        jsonSetting.Formatting = Formatting.Indented;
        jsonSetting.Converters.Add(new Vector3Converter());
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            //save
            PlayerState obj = new PlayerState()
            {
                playerName = "ABC",
                lives = 10,
                health = 10.999f
            };

            string pathFolder = Path.Combine(
                Application.persistentDataPath,
                "JsonTest"
            );

            if (!Directory.Exists(pathFolder))
            {
                Directory.CreateDirectory(pathFolder);
            }

            string path = Path.Combine(
                pathFolder,
                "player2.json"
            );

            string json = JsonConvert.SerializeObject(obj, jsonSetting);//, new Vector3Converter()); 1번 컨버트
            File.WriteAllText(path, json);

            Debug.Log(path);
            Debug.Log(json);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            //load
            string path = Path.Combine(Application.persistentDataPath, "JsonTest", "player2.json");

            string json = File.ReadAllText(path);
            PlayerState obj = JsonConvert.DeserializeObject<PlayerState>(json, jsonSetting);//, new Vector3Converter()); 1번 컨버트

            Debug.Log(json);
            Debug.Log(obj);
        }
    }
}
