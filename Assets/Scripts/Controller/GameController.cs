using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Health bossHealth;
    public Direction direction;
    public GameObject queen;
    private bool isBossDefeated = false;
    private bool isQueenSpawned = false;

    void Start()
    {
    }

    void Update()
    {
        if (!isBossDefeated && bossHealth.IsDead())
        {
            OnBossDefeated();
            isBossDefeated = true; 
        }
    }
    private void OnBossDefeated()
    {
        direction.MakeDirectionVisible();
        Debug.Log("Boss defeated!");
    }
    public void SpawnQueen()
    {
        if (!isQueenSpawned)
        {
            Vector3 positionQueen = new Vector3(-2, 2.85f, 17);
            Instantiate(queen, positionQueen, queen.transform.rotation);
        }
        isQueenSpawned=true;
    }
}
