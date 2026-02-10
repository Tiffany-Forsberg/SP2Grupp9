using System;
using UnityEngine;


 public class AudioTrigger : MonoBehaviour
{
    public enum Action
    {
        Play,
        Stop
    }
    [Serializable]
    public struct AudioSettings
    {
        public Location location;
        public Location action;
    }

    public AudioSettings[] triggerEnterSettings;

    /*private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            foreach (AudioSettings i in triggerEnterSettings)
            {
                switch (i.action)
                {
                    case Action.Play:
                        AudioManager.Instance.PlayAudio(i.location);
                        break;
                    case Action.Stop:
                        AudioManager.Instance.StopAudio(i.location);
                        break;
                }
            }
        }
    }
    */
}
