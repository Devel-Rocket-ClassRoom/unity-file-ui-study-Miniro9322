using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

[System.Serializable]
public class SomeClass
{
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
}

[System.Serializable]
public class ObjectSaveData
{
    public string prefabName;
    public Vector3 pos;
    public Quaternion rot;
    public Vector3 scale;
    public Color color;
}

public class JsonPractice : MonoBehaviour
{
    public GameObject cube;
    public string[] prefNames =
    {
        "Cube",
        "Sphere",
        "Capsule",
        "Cylinder",
    };

    public string fileName = "practice.json";
    public string FileFullPath => Path.Combine(Application.persistentDataPath, "JsonTest", fileName);

    private JsonSerializerSettings jsonSettings;

    private void Awake()
    {
        jsonSettings = new JsonSerializerSettings();
        jsonSettings.Formatting = Formatting.Indented;
        jsonSettings.Converters.Add(new Vector3Converter());
        jsonSettings.Converters.Add(new QuaternionConverter());
        jsonSettings.Converters.Add(new ColorConverter());
    }

    private void CreateRandomObjcet()
    {
        var prefabName = prefNames[Random.Range(0, prefNames.Length)];
        var prefab = Resources.Load<JsonTestObject>(prefabName);
        var obj = Instantiate(prefab);
        obj.transform.position = Random.insideUnitSphere * 10f;
        obj.transform.rotation = Random.rotation;
        obj.transform.localScale = Vector3.one * Random.Range(0.5f, 3f);
        obj.GetComponent<Renderer>().material.color = Random.ColorHSV();

    }

    public void Save()
    {
        var objs = GameObject.FindGameObjectsWithTag("TestObject");
        var saveList = new List<ObjectSaveData>();
        foreach(var obj in objs)
        {
            var jsonTestObj = obj.GetComponent<JsonTestObject>();
            saveList.Add(jsonTestObj.GetSaveData());
        }
        //SomeClass obj = new()
        //{
        //    pos = cube.transform.position,
        //    rot = cube.transform.rotation,
        //    scale = cube.transform.localScale,
        //    color = cube.GetComponent<Renderer>().material.color,
        //};
        var json = JsonConvert.SerializeObject(saveList, jsonSettings);
        File.WriteAllText(FileFullPath, json);
    }

    public void Load()
    {
        Clear();

        string json = File.ReadAllText(FileFullPath);
        var saveList = JsonConvert.DeserializeObject<List<ObjectSaveData>>(json,jsonSettings);
        foreach(var obj in saveList)
        {
            var prefab = Resources.Load<JsonTestObject>(obj.prefabName);
            var jsonTestObj = Instantiate(prefab);
            jsonTestObj.Set(obj);
        }

        //cube.transform.position = obj.pos;
        //cube.transform.rotation = obj.rot;
        //cube.transform.localScale = obj.scale;
        //cube.GetComponent<Renderer>().material.color = obj.color;

        //foreach (var temp in createdObj)
        //{
        //    Instantiate(temp);
        //}
    }

    public void Create()
    {
        var temp = Random.Range(1, 10);

        for(int i = 0; i < temp; i++)
        {
            CreateRandomObjcet();
        }
    }

    public void Clear()
    {
        var obj = GameObject.FindGameObjectsWithTag("TestObject");
        foreach(var temp in obj)
        {
            Destroy(temp);
        }
    }
}