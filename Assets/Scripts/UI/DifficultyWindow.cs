using Newtonsoft.Json;
using System;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public enum Difficulty
{
    Easy,
    Normal,
    Hard,
}

[Serializable]
public class DifficultyInfo
{
    public Difficulty difficulty;
}

public class DifficultyWindow : GenericWindow
{
    public Toggle[] toggles;

    public int selected;

    private Difficulty diff = Difficulty.Easy;

    private void Awake()
    {
        toggles[0].onValueChanged.AddListener(OnEasy);
        toggles[1].onValueChanged.AddListener(OnNormal);
        toggles[2].onValueChanged.AddListener(OnHard);
    }

    public override void Open()
    {
        base.Open();
        var path = Path.Combine(Application.persistentDataPath, "Resouces/Difficulty", "Difficulty.json");

        if (File.Exists(path) == false)
        {
            Debug.Log("로드 오류");
            return;
        }

        var json = File.ReadAllText(path);
        var temp = JsonConvert.DeserializeObject<DifficultyInfo>(json);

        diff = temp.difficulty;

        toggles[(int)diff].isOn = true;
    }

    public void OnEasy(bool active)
    {
        diff = Difficulty.Easy;
    }

    public void OnNormal(bool active)
    {
        diff = Difficulty.Normal;
    }

    public void OnHard(bool active)
    {
        diff = Difficulty.Hard;
    }

    public void OnCancel()
    {
        base.Close();
        windowManager.Open(0);
    }

    public void OnApply()
    {
        var path = Path.Combine(Application.persistentDataPath, "Resouces/Difficulty");

        if(Directory.Exists(path) == false)
        {
            Directory.CreateDirectory(path);
        }

        var temp = new DifficultyInfo();
        temp.difficulty = diff;

        var filePath = Path.Combine(path, "Difficulty.json");
        var json = JsonConvert.SerializeObject(temp);

        File.WriteAllText(filePath, json);

        base.Close();
        windowManager.Open(0);
    }
}
