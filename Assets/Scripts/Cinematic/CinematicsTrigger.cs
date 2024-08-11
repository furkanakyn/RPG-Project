using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicsTrigger : MonoBehaviour
{
    bool alreadyTriggered= false;
    private void OnTriggerEnter(Collider other)
    {
        if (!alreadyTriggered && other.gameObject.tag == "Player")
        {
            alreadyTriggered = true;
            GetComponent<PlayableDirector>().Play();
        }
    }
}
