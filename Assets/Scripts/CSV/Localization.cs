using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Localization : LocalizationText
{
    public Image icon;
    public string ItemId;
    public ButtonClicked itemInfo;

    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangedItemId();
#endif
    }

    private void OnEnable()
    {
        OnChangedItemId();
    }

    public void ButtonClicked()
    {
        itemInfo.SetItemData(ItemId);
    }

    private void OnChangedItemId()
    {
        var data = DataTableManager.ItemTable.Get(ItemId);
        id = data.Name;
        icon.sprite = data.SpriteIcon;
        base.OnChangedId();
    }
}
