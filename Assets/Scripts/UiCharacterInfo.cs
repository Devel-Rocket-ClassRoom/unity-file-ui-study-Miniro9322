using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCharacterInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0}: {1}";

    public Image CharacterIcon;
    public Image ItemIcon;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textAtk;
    public UiPannelInventory inventory;

    public void SetEmpty()
    {
        CharacterIcon.sprite = null;
        textName.text = string.Empty;
        textDesc.text = string.Empty;
        textAtk.text = string.Empty;
    }

    public void SetSaveCharacterData(SaveCharacterData saveCharacterData)
    {
        CharacterData data = saveCharacterData.CharacterData;

        CharacterIcon.sprite = data.SpriteIcon;
        ItemIcon.sprite = saveCharacterData.ItemData.SpriteIcon;
        textName.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Name"), data.StringName);
        textDesc.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Desc"), data.StringDesc);
        Debug.Log(data.AttackDmg);
        textAtk.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Attack"), data.AttackDmg);
    }

    public void OpenInventory()
    {
        inventory.gameObject.SetActive(true);
    }
}
