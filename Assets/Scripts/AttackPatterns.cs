using UnityEngine;

public class AttackPatterns : MonoBehaviour
{
    public Rigidbody2D rb;

    void FixedUpdate()
    {
        rb.linearVelocityX = 5;
    }
}
