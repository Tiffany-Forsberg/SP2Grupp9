using UnityEngine;

public class DestroyObjectEvent : MonoBehaviour
{
    public void DestroyObjectImmediate(GameObject target)
    {
        DestroyImmediate(target);
    }

    public void DestroyObject(GameObject target)
    {
        Destroy(target);
    }
}
