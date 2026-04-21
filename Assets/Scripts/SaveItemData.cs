using Newtonsoft.Json;
using System;

[Serializable]
public class SaveItemData
{
    public Guid instanceId { get; set; }

    [JsonConverter(typeof(ItemDataConverter))]
    public ItemData ItemData { get; set; }

    public DateTime creationTime { get; set; }

    public UiInvenSlotList.SortingOptions sortingOptions { get; set; }
    public UiInvenSlotList.FilteringOptions filteringOptions     { get; set; }


    public static SaveItemData GetRandomItem()
    {
        SaveItemData newItem = new SaveItemData();
        newItem.ItemData = DataTableManager.ItemTable.GetRandom();
        return newItem;
    }

    public SaveItemData()
    {
        instanceId = Guid.NewGuid();
        creationTime = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{instanceId}\n{creationTime}\n{ItemData.Id}";
    }
}
