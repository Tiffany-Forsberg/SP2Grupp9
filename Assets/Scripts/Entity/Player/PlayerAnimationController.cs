using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    [SerializeField] private Animator animator;
    
    [SerializeField] private Rigidbody2D rigidbody2D;

    [SerializeField] private string yVelocityVariable;
    
    [SerializeField] private string xVelocityVariable;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.SetFloat(yVelocityVariable, rigidbody2D.linearVelocityY);
        animator.SetFloat(xVelocityVariable, rigidbody2D.linearVelocityX);
    }

    public void SetBoolTrue(string animatorBool)
    {
        animator.SetBool(animatorBool, true);
    }
    
    public void SetBoolFalse(string animatorBool)
    {
        animator.SetBool(animatorBool, false);
    }
    
    public void SetTrigger(string animatorBool)
    {
        animator.SetTrigger(animatorBool);
    }
}
