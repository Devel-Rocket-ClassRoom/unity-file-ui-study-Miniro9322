using Newtonsoft.Json;
using System;
using UnityEngine;

[Serializable]
public class SaveCharacterData
{
    public Guid instanceId { get; set; }

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData ItemData { get; set; }

    [JsonConverter(typeof(CharacterDataConverter))]
    public CharacterData CharacterData { get; set; }

    public DateTime creationTime { get; set; }

    public UiInvenSlotList.SortingOptions sortingOptions { get; set; }
    public UiInvenSlotList.FilteringOptions filteringOptions { get; set; }


    public static SaveCharacterData GetRandomCharacter()
    {
        SaveCharacterData newItem = new SaveCharacterData();
        newItem.CharacterData = DataTableManager.CharacterTable.GetRandom();
        return newItem;
    }

    public SaveCharacterData()
    {
        instanceId = Guid.NewGuid();
        creationTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{instanceId}\n{creationTime}\n{CharacterData.Id}";
    }
}
