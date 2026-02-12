using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public SeleneAudio  SeleneAudio;
    public GameObject footstepSource;
    
    public void FootstepPlay()
    {
        Debug.DrawRay(footstepSource.transform.position, Vector3.down*1f, Color.red, duration:1f);
        RaycastHit hit;
        if (Physics.Raycast(footstepSource.transform.position, Vector3.down, out hit,1f))
        {
            Debug.Log("FootstepPlay on: " + hit.collider.gameObject.tag);
            SeleneAudio.FootstepEventPlay(hit.collider.tag, footstepSource);
        }
        else
        {
            Debug.Log("No hit...");
        }
    }
    
}
