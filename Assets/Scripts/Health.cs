using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] float health = 100f;
    bool isDead = false;

    public bool IsDead()
    {
        return isDead;
    }
    
    
    public void TakeDamage(float damage)
    {
        health= Mathf.Max(health-damage, 0);
        if (health == 0)
        {
            DieMethod();
        }
    }

    private void DieMethod()
    {
        if (isDead == true)
        {
            return;
        }
        isDead = true;
        GetComponent<Animator>().SetTrigger("die");
        GetComponent<Collider>().enabled = false;
    }
}
