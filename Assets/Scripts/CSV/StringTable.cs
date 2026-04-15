using System.Collections.Generic;
using UnityEngine;

public class StringTable : DataTable
{   
    public class Data
    {
        public string Id { get; set; }
        public string String { get; set; }
    }

    private readonly Dictionary<string, string> table = new();
    public static readonly string Unknown = "키 없음";

    public override void Load(string fileName)
    {
        table.Clear();

        var path = string.Format(FormatPath, fileName);
        TextAsset textAssest = Resources.Load<TextAsset>(path);
        var list = LoadCSV<Data>(textAssest.text);
        foreach(var item in list)
        {
            if (!table.ContainsKey(item.Id))
            {
                table.Add(item.Id, item.String);
            }
            else
            {
                Debug.LogError($"키 중복: {item.Id}");
            }
        }
    }

    public string Get(string key)
    {
        if (!table.ContainsKey(key))
        {
            return Unknown;
        }
        return table[key];
    }

    public void GetAll()
    {
        foreach(var item in table)
        {
            Debug.Log($"{item.Key} : {item.Value}");
        }
    }
}
