using UnityEngine;
using FMODUnity;
using FMOD.Studio;

[CreateAssetMenu(fileName = "EnemyBasicAudio", menuName = "Scriptable Objects/EnemyBasicAudio")]

public class BasicEnemyAudio : ScriptableObject
{
   [SerializeField]
   private EventReference basicEnemyAttackEventRef,basicEnemyDeathEventRef, basicEnemyDamageEventRef;
   private EventInstance basicEnemyAttackEventInst, basicEnemyDeathEventInst, basicEnemyDamageEventInst;
   
   //Public void method for playing the sound when an enemy attack
   public void basicEnemyAttackPlay(GameObject obj)
   {
       //Creating EventInstance based on the information stored in the EventReference enemyDameageRef
       basicEnemyAttackEventInst = RuntimeManager.CreateInstance(basicEnemyAttackEventRef);

       //Links the eventReference to a specified GameObject decided by the GameObject-parameter obj, and its Rigibody
       RuntimeManager.AttachInstanceToGameObject(basicEnemyAttackEventInst, obj, obj.GetComponent<Rigidbody>());

       //Starts the EventInstance
       basicEnemyAttackEventInst.start();

       //Releases the EventInstance from memory
       basicEnemyAttackEventInst.release();
   }

   public void basicEnemyDeathPlay(GameObject obj)
   {
       basicEnemyDeathEventInst = RuntimeManager.CreateInstance(basicEnemyDeathEventRef);

       RuntimeManager.AttachInstanceToGameObject(basicEnemyDeathEventInst, obj, obj.GetComponent<Rigidbody>());

       basicEnemyDeathEventInst.start();

       basicEnemyDeathEventInst.release();   
   }

   public void basicEnemyDamagePlay(GameObject obj)
   {
       basicEnemyDamageEventInst = RuntimeManager.CreateInstance(basicEnemyDamageEventRef);
       
       RuntimeManager.AttachInstanceToGameObject(basicEnemyDamageEventInst, obj, obj.GetComponent<Rigidbody>());

       basicEnemyDamageEventInst.start();

       basicEnemyAttackEventInst.release();
   }
}
