using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class CharacterLocalization : LocalizationText
{
    public Image icon;
    public string characterId;
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
        itemInfo.SetCharacterData(characterId);
    }

    private void OnChangedItemId()
    {
        var data = DataTableManager.CharacterTable.Get(characterId);
        
        id = data.Name;
        icon.sprite = data.SpriteIcon;
        base.OnChangedId();
    }
}
