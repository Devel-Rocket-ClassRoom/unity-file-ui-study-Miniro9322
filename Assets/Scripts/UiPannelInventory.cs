using System;
using TMPro;
using UnityEngine;

public class UiPannelInventory : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiInvenSlotList uiInvenSlotList;

    public TextMeshProUGUI SaveText;
    public TextMeshProUGUI LoadText;
    public TextMeshProUGUI AddText;
    public TextMeshProUGUI RemoveText;

    private void OnEnable()
    {
        OnLoad();
        OnChangeFiltering(filtering.value);
        OnChangeSorting(sorting.value);
    }

    public void OnChangeSorting(int index)
    {
        uiInvenSlotList.Sorting = (UiInvenSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiInvenSlotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.ItemList = uiInvenSlotList.getSaveItemDataList();
        SaveLoadManager.Data.Sorting = uiInvenSlotList.Sorting;
        SaveLoadManager.Data.Filtering = uiInvenSlotList.Filtering;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiInvenSlotList.SetSaveItemDataList(SaveLoadManager.Data.ItemList, SaveLoadManager.Data.Sorting, SaveLoadManager.Data.Filtering);
        sorting.value = (int)uiInvenSlotList.Sorting;
        filtering.value = (int)uiInvenSlotList.Filtering;
    }

    public void OnCreateItem()
    {
        uiInvenSlotList.AddRandomItem();
    }

    public void OnRemoveItem()
    {
        uiInvenSlotList.RemoveItem();

    }
}
