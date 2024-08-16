using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenFollowingCharacter : MonoBehaviour
{
    GameObject player;
    public float stopRange = 3f;
    public float speedFraction = 1f;
    Mover mover;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        mover = GetComponent<Mover>();
    }
    private void Update()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
      if(distanceToPlayer > stopRange)
        {
            mover.MoveTo(player.transform.position,1f);
        }
        else
        {
            mover.Cancel();
        }
    }
}
