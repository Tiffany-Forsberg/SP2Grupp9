using Unity.Cinemachine;
using UnityEngine;

public class VerticalCameraFollow : MonoBehaviour
{
    [SerializeField] private Rigidbody2D playerRigidBody;
    [SerializeField] private CinemachinePositionComposer positionComposer;
    
    [SerializeField] private float upwardsDamping;
    [SerializeField] private float downwardsDamping;
    [SerializeField] private float defaultDamping = 3.0f;

    private void Update()
    {
        if (playerRigidBody.linearVelocityY > 0)
        {
            positionComposer.Damping.y = upwardsDamping;
        }
        else if (playerRigidBody.linearVelocityY < 0)
        {
            positionComposer.Damping.y = downwardsDamping;
        }
        else
        {
            positionComposer.Damping.y = defaultDamping;
        }
    }
}
