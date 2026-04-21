using TMPro;
using UnityEngine;

public class UiPannelCharacterSlot : MonoBehaviour
{
    public TMP_Dropdown sorting;
    public TMP_Dropdown filtering;

    public UiCharacterSlotList uiCharacterSlotList;

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
        uiCharacterSlotList.Sorting = (UiInvenSlotList.SortingOptions)index;
    }

    public void OnChangeFiltering(int index)
    {
        uiCharacterSlotList.Filtering = (UiInvenSlotList.FilteringOptions)index;
    }

    public void OnSave()
    {
        SaveLoadManager.Data.CharacterList = uiCharacterSlotList.getSaveCharacterDataList();
        SaveLoadManager.Data.Sorting = uiCharacterSlotList.Sorting;
        SaveLoadManager.Data.Filtering = uiCharacterSlotList.Filtering;
        SaveLoadManager.Save();
    }

    public void OnLoad()
    {
        SaveLoadManager.Load();
        uiCharacterSlotList.SetSaveCharacterDataList(SaveLoadManager.Data.CharacterList, SaveLoadManager.Data.Sorting, SaveLoadManager.Data.Filtering);
        sorting.value = (int)uiCharacterSlotList.Sorting;
        filtering.value = (int)uiCharacterSlotList.Filtering;
    }

    public void OnCreateCharacter()
    {
        uiCharacterSlotList.AddRandomCharacter();
    }

    public void OnRemoveCharacter()
    {
        uiCharacterSlotList.RemoveCharacter();

    }
}
