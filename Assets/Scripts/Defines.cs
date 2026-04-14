public enum Languages
{
    Korean,
    English,
    Japanese,
}

public enum ItemTypes
{
    Weapon,
    Equip,
    Consumable,
}

public static class Variables
{
    public static event System.Action OnLanguageChanged;

    private static Languages language = Languages.Korean;

    public static Languages Language
    {
        get
        {
            return language;
        }
        set
        {
            if(language == value)
            {
                return;
            }

            language = value;
            DataTableManager.ChangeLanguage(language);
            OnLanguageChanged?.Invoke();
        }
    }
}

public static class DataTableIds
{
    public static readonly string[] StringTableIds =
    {
        "StringTableKr",
        "StringTableEn",
        "StringTableJp"
    };

    public static string String => StringTableIds[(int)Variables.Language];

    public static readonly string Item = "ItemTable";

    public static readonly string Character = "CharacterTable";

}
