using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[CreateAssetMenu(fileName = "CharacterAudio", menuName = "Scriptable Objects/CharacterAudio")]

public class SeleneAudio : ScriptableObject
{
    [SerializeField]
    private EventReference footstepEventRef, clawAttackEventRef, jumpEventRef, landEventRef, damageEventRef;

    private EventInstance footstepEventInst, clawAttackEventInst, jumpEventInst, landEventInst, damageEventInst;
    
   /* public void FootstepEventPlay(string surfaceTag, GameObject obj)
    {
        if (footstepEvent.IsNull)
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
    }*/
}
