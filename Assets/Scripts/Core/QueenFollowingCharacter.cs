using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QueenFallowingCharacter : MonoBehaviour
{
    GameObject player;
    public float stopRange = 3f;
    Mover mover;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        mover = GetComponent<Mover>();
    }
    private void Update()
    {
        transform.LookAt(player.transform.position);
        float distanceToPlayer = Vector3.Distance(transform.position, player.transform.position);
        if(stopRange < distanceToPlayer)
        {
            FollowPlayer();
        }
        else
        {
            mover.Cancel();
        }
    }
    private void FollowPlayer()
    {
        mover.MoveTo(player.transform.position, 1f);
    }
}
