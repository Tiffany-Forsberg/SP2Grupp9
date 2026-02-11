using UnityEngine;

public class DisplayCollider : MonoBehaviour
{
    [SerializeField] private BoxCollider2D confoundCollider;

    private void OnValidate()
    {
        if (!confoundCollider) 
        {
            Debug.LogWarning("Display Confounds is missing a reference to a Box Collider 2D :(", this);
        }
    }
    
    private void OnDrawGizmos()
    {
        if (!confoundCollider) return;
        
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireCube(confoundCollider.transform.position + (Vector3)confoundCollider.offset, confoundCollider.size);
    }
}
