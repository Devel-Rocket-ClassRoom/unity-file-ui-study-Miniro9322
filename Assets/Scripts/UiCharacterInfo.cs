using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCharacterInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0}: {1}";

    public Image CharacterIcon;
    public UIItemSlot Weapon;
    public UIItemSlot Equip;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textAtk;
    public UiPannelInventory inventory;
    public SaveCharacterData saveCharacterData { get; private set; }

    public void SetEmpty()
    {
        CharacterIcon.sprite = null;
        textName.text = string.Empty;
        textDesc.text = string.Empty;
        textAtk.text = string.Empty;
        Weapon.SetEmpty();
        Equip.SetEmpty();
    }

    public void SetSaveCharacterData(SaveCharacterData saveCharacterData)
    {
        CharacterData data = saveCharacterData.CharacterData;
        this.saveCharacterData = saveCharacterData;

        CharacterIcon.sprite = data.SpriteIcon;
        Weapon.SetItem(saveCharacterData.Weapon);
        Debug.Log("오류안남");
        Equip.SetItem(saveCharacterData.Equip);
        Debug.Log("오류안남");
        textName.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Name"), data.StringName);
        textDesc.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Desc"), data.StringDesc);
        textAtk.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Attack"), data.AttackDmg);
        Debug.Log(saveCharacterData.Weapon);
    }

    public void OpenInventory(string type)
    {
        if(CharacterIcon.sprite == null)
        {
            return;
        }

        inventory.gameObject.SetActive(true);
        inventory.uiInvenSlotList.Type = type;
    }
}
