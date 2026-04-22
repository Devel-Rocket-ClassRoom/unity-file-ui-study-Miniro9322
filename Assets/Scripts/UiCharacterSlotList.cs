using Mono.Cecil;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UiInvenSlotList;

public class UiCharacterSlotList : MonoBehaviour
{
    public readonly System.Comparison<SaveCharacterData>[] comparisons =
    {
        (lhs, rhs) => lhs.creationTime.CompareTo(rhs.creationTime),
        (lhs, rhs) => rhs.creationTime.CompareTo(lhs.creationTime),
        (lhs, rhs) => lhs.CharacterData.StringName.CompareTo(rhs.CharacterData.StringName),
        (lhs, rhs) => rhs.CharacterData.StringName.CompareTo(lhs.CharacterData.StringName),
    };

    public readonly System.Func<SaveCharacterData, bool>[] filterings =
    {
        (x) => true,
    };

    public UiCharacterSlot prefab;
    public ScrollRect scrollRect;
    public UiCharacterInfo characterInfo;

    private List<UiCharacterSlot> uiSlotList = new();

    private List<SaveCharacterData> saveCharacterDataList = new();

    private SortingOptions sorting = SortingOptions.CreationTimeAscending;
    private FilteringOptions filtering = FilteringOptions.None;

    public string Type;
    private int maxSlot = 8;
    private int charCount = 0;

    public SortingOptions Sorting
    {
        get { return sorting; }
        set
        {
            if (sorting != value)
            {
                sorting = value;
                UpdateSlots();
            }
        }
    }

    public FilteringOptions Filtering
    {
        get { return filtering; }
        set
        {
            if (filtering != value)
            {
                filtering = value;
                UpdateSlots();
            }
        }
    }

    private int selectedSlotIndex = -1;

    private void OnsSelectSlot(SaveCharacterData saveCharacterData)
    {
        if(saveCharacterData == null)
        {
            return;
        }
        Debug.Log(1);
        characterInfo.SetSaveCharacterData(saveCharacterData);
    }

    private void Start()
    {
        if(saveCharacterDataList.Count >= maxSlot)
        {
            Debug.Log(charCount);
            onSelectSlot.AddListener(OnsSelectSlot);
            UpdateSlots();
            return;
        }
        onSelectSlot.AddListener(OnsSelectSlot);
        for(int i = 0; i < maxSlot; i++)
        {
            var character = new SaveCharacterData();
            character.CharacterData = DataTableManager.CharacterTable.Get(string.Empty);
            character.creationTime = DateTime.MaxValue;
            character.instanceId = Guid.Empty;
            saveCharacterDataList.Add(character);
        }
        UpdateSlots();
    }

    private void OnDisable()
    {
        saveCharacterDataList = null;
        Type = string.Empty;
    }

    public void SetSaveCharacterDataList(List<SaveCharacterData> source, SortingOptions sorting, FilteringOptions filtering)
    {
        saveCharacterDataList = source.ToList();
        this.sorting = sorting;
        this.filtering = filtering;
        foreach (var temp in saveCharacterDataList)
        {
            if (temp.CharacterData.SpriteIcon != null)
            {
                charCount++;
            }
        }
        UpdateSlots();
    }

    public List<SaveCharacterData> getSaveCharacterDataList()
    {
        return saveCharacterDataList;
    }

    public UnityEvent onUpdateCharacterSlots;
    public UnityEvent<SaveCharacterData> onSelectSlot;

    private void UpdateSlots()
    {
        var list = saveCharacterDataList.Where(filterings[(int)filtering]).ToList();
        list.Sort(comparisons[(int)sorting]);

        if (uiSlotList.Count < list.Count)
        {
            for (int i = uiSlotList.Count; i < list.Count; i++)
            {
                var newSlot = Instantiate(prefab, scrollRect.content);
                newSlot.slotIndex = i;
                newSlot.SetEmpty();
                newSlot.gameObject.SetActive(false);

                newSlot.button.onClick.AddListener(() =>
                {
                    selectedSlotIndex = newSlot.slotIndex;
                    onSelectSlot?.Invoke(newSlot.SaveCharacterData);
                });

                uiSlotList.Add(newSlot);
            }
        }

        for (int i = 0; i < uiSlotList.Count; i++)
        {
            if (i < list.Count)
            {
                uiSlotList[i].gameObject.SetActive(true);
                Debug.Log(list[i]);
                uiSlotList[i].SetItem(list[i]);
            }
            else
            {
                uiSlotList[i].gameObject.SetActive(false);
                uiSlotList[i].SetEmpty();
            }
        }

        selectedSlotIndex = -1;
        onUpdateCharacterSlots?.Invoke();
    }

    public void AddRandomCharacter()
    {
        if(charCount >= maxSlot)
        {
            return;
        }
        saveCharacterDataList[charCount] = SaveCharacterData.GetRandomCharacter();
        charCount++;
        UpdateSlots();
    }

    public void RemoveCharacter()
    {
        if (selectedSlotIndex == -1)
        {
            return;
        }

        saveCharacterDataList.Remove(uiSlotList[selectedSlotIndex].SaveCharacterData);
        var character = new SaveCharacterData();
        character.CharacterData = DataTableManager.CharacterTable.Get(string.Empty);
        character.CharacterData = DataTableManager.CharacterTable.Get(string.Empty);
        character.creationTime = DateTime.MaxValue;
        character.instanceId = Guid.Empty;
        saveCharacterDataList.Add(character);
        charCount--;
        UpdateSlots();
        characterInfo.SetEmpty();
    }
}