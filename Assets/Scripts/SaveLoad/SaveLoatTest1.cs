using CsvHelper;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using UnityEngine;

public class SaveLoatTest1 : MonoBehaviour
{
    private List<string> ids = new();

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SaveLoadManager.Data = new SaveDataV5();
            SaveLoadManager.Data.Name = "TEST1234";
            SaveLoadManager.Data.Gold = 4321;
            foreach(var id in ids)
            {
                var temp = new SaveItemData();
                temp.ItemData = DataTableManager.ItemTable.Get(id);
                SaveLoadManager.Data.ItemList.Add(temp);
            }
            SaveLoadManager.Save();
            ids.Clear();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            if(SaveLoadManager.Load() == false)
            {
                Debug.Log("세이브 파일 없음");
            }
            else
            {
                Debug.Log(SaveLoadManager.Data.Name);
                Debug.Log(SaveLoadManager.Data.Gold);
                foreach (var id in SaveLoadManager.Data.ItemList)
                {
                    Debug.Log(id.instanceId);
                    Debug.Log(id.ItemData.Name);
                    Debug.Log(id.creationTime);
                }
            }   
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            ids.Add(DataTableManager.ItemTable.GetRandomKey());
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            SaveLoadManager.Data = new SaveDataV5();
            SaveLoadManager.Data.Name = "TEST1234";
            SaveLoadManager.Data.Gold = 4321;
            foreach (var id in ids)
            {
                var temp = new SaveItemData();
                temp.ItemData = DataTableManager.ItemTable.Get(id);
                SaveLoadManager.Data.ItemList.Add(temp);
            }
            SaveLoadManager.Save(0, SaveLoadManager.SaveMode.Encrypted);
            ids.Clear();
        }

        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            if (SaveLoadManager.Load(0, SaveLoadManager.SaveMode.Encrypted) == false)
            {
                Debug.Log("세이브 파일 없음");
            }
            else
            {
                Debug.Log(SaveLoadManager.Data.Name);
                Debug.Log(SaveLoadManager.Data.Gold);
                foreach (var id in SaveLoadManager.Data.ItemList)
                {
                    Debug.Log(id.instanceId);
                    Debug.Log(id.ItemData.Name);
                    Debug.Log(id.creationTime);
                }
            }
        }
    }
}