using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlDeactivate : MonoBehaviour
{
    GameObject player;
    ActionScheduler actionScheduler;
    Mover mover;

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        actionScheduler = player.GetComponent<ActionScheduler>();
        mover = player.GetComponent<Mover>();

        GetComponent<PlayableDirector>().played += DisableControl;
        GetComponent<PlayableDirector>().stopped += EnableControl;
    }

    void EnableControl(PlayableDirector pD)
    {
        player.GetComponent<PlayerController>().enabled = true;
    }

    void DisableControl(PlayableDirector pD)
    {
        actionScheduler.CancelCurrentAction();
        mover.Cancel(); 
        player.GetComponent<PlayerController>().enabled = false;
    }
}
