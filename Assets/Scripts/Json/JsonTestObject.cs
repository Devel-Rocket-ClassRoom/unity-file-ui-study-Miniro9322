using UnityEngine;

public class JsonTestObject : MonoBehaviour
{
    public string prefabName;

    public void Set(ObjectSaveData data)
    {

        transform.position = data.pos;
        transform.rotation = data.rot;
        transform.localScale = data.scale;
        gameObject.GetComponent<Renderer>().material.color = data.color;
    }

    public ObjectSaveData GetSaveData()
    {
        ObjectSaveData obj = new ObjectSaveData();
        obj.prefabName = prefabName;
        obj.pos = transform.position;
        obj.rot = transform.rotation;
        obj.scale = transform.localScale;
        obj.color = gameObject.GetComponent<Renderer>().material.color;

        return obj;
    }
}
