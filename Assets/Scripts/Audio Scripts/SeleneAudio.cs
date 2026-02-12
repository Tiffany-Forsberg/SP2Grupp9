using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[CreateAssetMenu(fileName = "SeleneAudio", menuName = "Scriptable Objects/SeleneAudio")]

public class SeleneAudio : ScriptableObject
{
    [SerializeField]
    private EventReference footstepEventRef, clawAttackEventRef, jumpEventRef, landEventRef, damageEventRef;

    private EventInstance footstepEventInst, clawAttackEventInst, jumpEventInst, landEventInst, damageEventInst;

    public void FootstepEventPlay(string surfaceTag, GameObject obj)
    {
        if (footstepEventRef.IsNull)
        {
            Debug.LogWarning("Event not found: footstepEvent");
        }
        else
        {
            footstepEventInst = RuntimeManager.CreateInstance(footstepEventRef);
            RuntimeManager.AttachInstanceToGameObject(footstepEventInst, obj, obj.GetComponent<Rigidbody>());
            
            // A switch statement that compares the surfaceTag and its content, and sets the Surface paramteter wth a unique value based on this
            switch (surfaceTag)
            {
                case "Stone":
                    footstepEventInst.setParameterByName("SurfaceTag", 0f);
                    break;
                case "Dirt":
                    footstepEventInst.setParameterByName("SurfaceTag", 1f);
                    break;
                case "Metal":
                    footstepEventInst.setParameterByName("SurfaceTag", 2f);
                    break;
            }
            
            footstepEventInst.start();
            footstepEventInst.release();
        }
    }

    public void clawAttackPlay(GameObject obj)
    {
        clawAttackEventInst = RuntimeManager.CreateInstance(clawAttackEventRef);

        RuntimeManager.AttachInstanceToGameObject(clawAttackEventInst, obj, obj.GetComponent<Rigidbody>());

        clawAttackEventInst.start();

        clawAttackEventInst.release();
    }
   
   public void JumpEventPlay(string surfaceTag, GameObject obj)
   {
       if (jumpEventRef.IsNull)
       {
           Debug.LogWarning("Event not found: jumpEvent");
       }
       else
       {
           jumpEventInst = RuntimeManager.CreateInstance(jumpEventRef);
            
           RuntimeManager.AttachInstanceToGameObject(jumpEventInst, obj, obj.GetComponent<Rigidbody>());
            
           jumpEventInst.start();
            
           jumpEventInst.release();
            
       }
   }
   
   public void LandEventPlay(string surfaceTag, GameObject obj)
   {
       if (landEventRef.IsNull)
       {
           Debug.LogWarning("Event not found: footstepEvent");
       }
       else
       {
           landEventInst = RuntimeManager.CreateInstance(landEventRef);
           RuntimeManager.AttachInstanceToGameObject(landEventInst, obj, obj.GetComponent<Rigidbody>());
            
           // A switch statement that compares the surfaceTag and its content, and sets the Surface paramteter wth a unique value based on this
           switch (surfaceTag)
           {
               case "Stone":
                   landEventInst.setParameterByName("SurfaceTag", 0f);
                   break;
               case "Dirt":
                   landEventInst.setParameterByName("SurfaceTag", 1f);
                   break;
               case "Metal":
                   landEventInst.setParameterByName("SurfaceTag", 2f);
                   break;
           }
           landEventInst.start();
           landEventInst.release();
       }
   } 


   public void DamageEventPlay(Transform transform)
   {
       RuntimeManager.PlayOneShot(damageEventRef, transform.position);
   } 
}
