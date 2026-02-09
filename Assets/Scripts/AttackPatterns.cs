using UnityEngine;

public class AttackPatterns : MonoBehaviour
{
    [SerializeField] private AttackBehaviour attackBehaviour;
    [SerializeField] private float speed = 5f;
    public Rigidbody2D rb;

    void FixedUpdate()
    {
        rb.linearVelocity = attackBehaviour.AttackDirection * speed;
    }
}
