using TMPro;
using UnityEngine;
using UnityEngine.TextCore.Text;
using UnityEngine.UI;

public class UiItemInfo : MonoBehaviour
{
    public static readonly string FormatCommon = "{0}: {1}";

    public Image imageIcon;
    public TextMeshProUGUI textName;
    public TextMeshProUGUI textDesc;
    public TextMeshProUGUI textType;
    public TextMeshProUGUI textValue;
    public TextMeshProUGUI textCost;

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        textName.text = string.Empty;
        textDesc.text = string.Empty;
        textType.text = string.Empty;
        textValue.text = string.Empty;
        textCost.text = string.Empty;
    }

    public void SetSaveItemData(SaveItemData saveItemData)
    {
        ItemData data = saveItemData.ItemData;

        imageIcon.sprite = data.SpriteIcon;
        textName.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Name"), data.StringName);
        textDesc.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Desc"), data.StringDesc);

        string id = data.Type.ToString();

        textType.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Type"), DataTableManager.StringTable.Get(id));
        textValue.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Name"), data.Value);
        textCost.text = string.Format(FormatCommon, DataTableManager.StringTable.Get("Name"), data.Cost);
    }
}
