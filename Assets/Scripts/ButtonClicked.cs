using UnityEngine;
using UnityEngine.UI;

public class ButtonClicked : MonoBehaviour
{
    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;
    public LocalizationText textAttack;


    public void Start()
    {
        SetEmpty();
    }

    public void SetEmpty()
    {
        icon.sprite = null;

        textName.id = string.Empty;
        textDesc.id = string.Empty;

        textName.text.text = string.Empty;
        textDesc.text.text = string.Empty;
        textAttack.text.text = string.Empty;
    }

    public void SetItemData(string itemId)
    {
        ItemData data = DataTableManager.ItemTable.Get(itemId);
        SetItemData(data);

    }

    public void SetItemData(ItemData data)
    {
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.id = data.Desc;

        textName.OnChangedId();
        textDesc.OnChangedId();

    }

    public void SetCharacterData(string characterid)
    {
        CharacterData data = DataTableManager.CharacterTable.Get(characterid);
        SetCharacterData(data);
    }

    public void SetCharacterData(CharacterData data)
    {
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.id = data.Desc;
        textAttack.text.text = data.State;

        textName.OnChangedId();
        textDesc.OnChangedId();
    }
}
