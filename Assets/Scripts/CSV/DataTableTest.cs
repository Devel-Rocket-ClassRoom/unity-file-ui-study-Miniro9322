using UnityEngine;

public class DataTableTest : MonoBehaviour
{
    public string NameStringTableKr = "StringTableKr";
    public string NameStringTableEn = "StringTableEn";
    public string NameStringTableJp = "StringTableJp";

    private StringTable table = new();

    public void onClickStringTabkeKr()
    {
        Debug.Log(DataTableManager.StringTable.Get("You Die"));

        //table.Load(NameStringTableKr);
        //Debug.Log(table.Get("Hello"));
        //table.GetAll();
    }

    public void onClickStringTabkeEn()
    {
        table.Load(NameStringTableEn);
        Debug.Log(table.Get("Hello"));
        table.GetAll();
    }

    public void onClickStringTabkeJp()
    {
        table.Load(NameStringTableJp);
        Debug.Log(table.Get("Hello"));
        table.GetAll();
    }
}
