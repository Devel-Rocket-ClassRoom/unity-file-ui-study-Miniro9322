using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class LocalizationText : MonoBehaviour
{
    public TextMeshProUGUI text;
    public string id;

#if UNITY_EDITOR
    public Languages languages;
#endif

    private void OnValidate()
    {
#if UNITY_EDITOR
        OnChangeLanguage(languages);
#endif
    }



    private void OnEnable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged += OnChangeLanguage;

            OnChangedId();
        }
#if UNITY_EDITOR
        else
        {
            OnChangeLanguage(languages);
        }
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Variables.Language = Languages.Korean;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Variables.Language = Languages.English;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Variables.Language = Languages.Japanese;
        }
    }

    public void OnChangedId()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }

    private void OnChangeLanguage()
    {
        text.text = DataTableManager.StringTable.Get(id);
    }

#if UNITY_EDITOR
    private void OnChangeLanguage(Languages lang)
    {
        var stringTable = DataTableManager.GetStringTable(lang);
        text.text = stringTable.Get(id);
    }
#endif

    private void OnDisable()
    {
        if (Application.isPlaying)
        {
            Variables.OnLanguageChanged -= OnChangeLanguage;
        }
    }

#if UNITY_EDITOR
    [ContextMenu("ChangeLanguage()")]
    private void ChangeLanguage()
    {
        var texts = FindObjectsByType<TextMeshProUGUI>(FindObjectsSortMode.None);

        foreach(var text in texts)
        {
            var stringTable = DataTableManager.GetStringTable(languages);
            text.text = stringTable.Get(id);
        }
    }
#endif
}
