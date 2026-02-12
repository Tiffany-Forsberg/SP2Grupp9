using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    public SeleneAudio SeleneAudio;
    public GameObject footstepSource;
    
    public void FootstepPlay()
    {
        //Debug.DrawRay(footstepSource.transform.position, Vector3.down*1f, Color.red, duration:1f);
        //RaycastHit hit;

        RaycastHit2D hit = Physics2D.Raycast(footstepSource.transform.position, Vector2.down, 1f,
            LayerMask.GetMask("Ground"));
        if (hit)
        {
            Debug.Log("FootstepPlay on: " + hit.collider.gameObject.tag);
            SeleneAudio.FootstepEventPlay(hit.collider.tag, footstepSource);
            
            Debug.Log("Yay!");
        }
        
        

        /*if (Physics.Raycast(footstepSource.transform.position, Vector3.down, out hit,1f))
        {
            Debug.Log("FootstepPlay on: " + hit.collider.gameObject.tag);
            SeleneAudio.FootstepEventPlay(hit.collider.tag, footstepSource);
        }
        else
        {
            Physics.Raycast(footstepSource.transform.position, Vector3.down, out hit, 1f);
            Debug.Log("No sound for you..." + hit);
        }*/
    }
    
}
