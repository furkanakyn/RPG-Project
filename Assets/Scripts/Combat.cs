using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Combat : MonoBehaviour, IAction
{
    public GameObject targetObject; 
    public float weaponRange = 2f; 
    private Mover mover;
    ActionScheduler actionScheduler;
    IAction action;
    float timeSinceLastAttack; 
    public float timeBetweenAttacks = 1f;
    [SerializeField] float weaponDamage;


    private void Start()
    {
        mover = GetComponent<Mover>();
        actionScheduler = GetComponent<ActionScheduler>();
        
    }

    public void Attack(GameObject target)
    {
        targetObject = target;
        actionScheduler.StartAction(this); 
    }

    public void Cancel()
    {
        targetObject = null; 
 
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        
        if (targetObject != null)
        {
            float distanceToTarget = Vector3.Distance(transform.position, targetObject.transform.position);

            if (distanceToTarget > weaponRange)
            {
                mover.MoveTo(targetObject.transform.position);
                actionScheduler.StartAction(this); 
            }
            else
            {
                AttackMethod();
                mover.Cancel();

            }
        }
    }
    
    private void AttackMethod()
    {
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            GetComponent<Animator>().SetTrigger("attack");
            timeSinceLastAttack = 0; 
        }
    }
    void Hit()
    {
        Health healt = targetObject.GetComponent<Health>();
        healt.TakeDamage(weaponDamage);
    }
}
