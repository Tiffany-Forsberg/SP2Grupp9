using UnityEngine;

public class EnemyDeath_DesignPlaceholder : MonoBehaviour
{
    int i = 0;

    public void Die()
    {
        Destroy(gameObject);
         i++;
        Debug.Log("Deaths: " + i);
    }
}
