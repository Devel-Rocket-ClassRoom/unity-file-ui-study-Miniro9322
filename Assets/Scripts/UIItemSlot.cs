using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIItemSlot : MonoBehaviour
{
    public int slotIndex = -1;

    public Image imageIcon;
    public TextMeshProUGUI itemName;
    public SaveItemData SaveItemData { get; private set; }

    public Button button;

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        itemName.text = string.Empty;
        SaveItemData = null;
    }

    public void SetItem(SaveItemData data)
    {
        SaveItemData = data;
        imageIcon.sprite = SaveItemData.ItemData.SpriteIcon;
        itemName.text = SaveItemData.ItemData.StringName;
    }
}
