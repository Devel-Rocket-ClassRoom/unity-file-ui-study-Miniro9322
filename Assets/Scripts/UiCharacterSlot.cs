using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UiCharacterSlot : MonoBehaviour
{
    public int slotIndex = -1;

    public Image imageIcon;
    public TextMeshProUGUI CharacterName;
    public SaveCharacterData SaveCharacterData { get; private set; }

    public Button button;

    public void SetEmpty()
    {
        imageIcon.sprite = null;
        CharacterName.text = string.Empty;
        SaveCharacterData = null;
    }

    public void SetItem(SaveCharacterData data)
    {
        SaveCharacterData = data;
        imageIcon.sprite = SaveCharacterData.CharacterData.SpriteIcon;
        CharacterName.text = SaveCharacterData.CharacterData.StringName;
    }
}
