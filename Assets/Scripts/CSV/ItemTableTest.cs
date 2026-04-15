using UnityEngine;

public class ItemTableTest : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log(DataTableManager.ItemTable.Get("Item1"));
        }
    }
}
