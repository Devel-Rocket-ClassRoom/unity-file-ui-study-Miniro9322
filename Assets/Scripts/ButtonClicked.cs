using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClicked : MonoBehaviour
{
    public Image icon;
    public LocalizationText textName;
    public LocalizationText textDesc;
    public TextMeshProUGUI textState;
    private CharacterData data;

    private void OnEnable()
    {
        Variables.OnLanguageChanged += LanguageChange;
    }

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
        textState.text = string.Empty;
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
        data = DataTableManager.CharacterTable.Get(characterid);
        SetCharacterData(data);
    }

    private void LanguageChange()
    {
        textState.text = data.State;
    }

    public void SetCharacterData(CharacterData data)
    {
        icon.sprite = data.SpriteIcon;
        textName.id = data.Name;
        textDesc.id = data.Desc;
        textState.text = data.State;

        Debug.Log(textState.text);

        textName.OnChangedId();
        textDesc.OnChangedId();
    }

    private void OnDisable()
    {
        Variables.OnLanguageChanged -= LanguageChange;
    }
}
