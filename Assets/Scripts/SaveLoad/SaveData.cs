using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static UnityEngine.LowLevelPhysics2D.PhysicsLayers;

[System.Serializable]
public abstract class SaveData
{
    public int Version { get; protected set; }

    public abstract SaveData VersionUp();
}

[System.Serializable]
public class SaveDataV1 : SaveData
{
    public string PlayerName { get; set; } = string.Empty;

    public SaveDataV1()
    {
        Version = 1;
    }

    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV2();

        saveData.Name = PlayerName;

        return saveData;
    }
}

[System.Serializable]
public class SaveDataV2 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;

    public SaveDataV2()
    {
        Version = 2;
    }

    public override SaveData VersionUp()
    {
        var saveData = new SaveDataV3();

        saveData.Name = Name;
        saveData.Gold = Gold;

        return saveData;
    }
}

[System.Serializable]
public class SaveDataV3 : SaveData
{
    public string Name { get; set; } = string.Empty;
    public int Gold = 0;
    public List<string> ItemIds = new();

    public SaveDataV3()
    {
        Version = 3;
    }

    public override SaveData VersionUp()
    {
        SaveDataV4 data = new SaveDataV4();
        data.Name = Name;
        data.Gold = Gold;
        foreach(string id in ItemIds)
        {
            SaveItemData itemData = new SaveItemData();
            itemData.ItemData = DataTableManager.ItemTable.Get(id);
            data.ItemList.Add(itemData);
        }
        return data;
    }
}

[System.Serializable]
public class SaveDataV4 : SaveDataV2
{
    public List<SaveItemData> ItemList = new();

    public SaveDataV4()
    {
        Version = 4;
    }

    public override SaveData VersionUp()
    {
        var data = new SaveDataV5();
        data.Name = Name;
        data.Gold = Gold;
        data.ItemList = ItemList.ToList();
        return data;
    }
}

[System.Serializable]
public class SaveDataV5 : SaveDataV4
{
    public List<SaveCharacterData> CharacterList = new();
    public SaveItemData Weapon;
    public SaveItemData Equip;
    public UiInvenSlotList.SortingOptions Sorting = UiInvenSlotList.SortingOptions.CreationTimeAscending;
    public UiInvenSlotList.FilteringOptions Filtering = UiInvenSlotList.FilteringOptions.None;

    public SaveDataV5()
    {
        Version = 5;
    }

    public override SaveData VersionUp()
    {
        throw new System.NotImplementedException();
    }
}