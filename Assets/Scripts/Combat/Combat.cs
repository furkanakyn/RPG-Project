using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class Combat : MonoBehaviour, IAction
{
    private Health targetObject;
    private CombatTarget target;
    private Mover mover;
    private ActionScheduler actionScheduler;
    IAction action;
    float timeSinceLastAttack;
    [SerializeField] float timeBetweenAttacks = 1f;
    [SerializeField] float weaponDamage;
    [SerializeField] float weaponRange = 2f;


    private void Start()
    {
        mover = GetComponent<Mover>();
        actionScheduler = GetComponent<ActionScheduler>();
        
    }

    public void Attack(GameObject target)
    {
        targetObject = target.GetComponent<Health>();
        actionScheduler.StartAction(this); 
    }

    public void Cancel()
    {
        GetComponent<Animator>().ResetTrigger("attack");
        GetComponent<Animator>().SetTrigger("stopAttack");
        targetObject = null; 
    }

    void Update()
    {
        timeSinceLastAttack += Time.deltaTime;
        if (targetObject == null)
        {
            return;
        }
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
        if (targetObject.IsDead() == true)
        {
            GetComponent<Animator>().ResetTrigger("attack");
            Cancel();
            return;
        }
    }
    
    private void AttackMethod()
    {
       transform.LookAt(targetObject.transform);
        if (timeSinceLastAttack > timeBetweenAttacks)
        {
            GetComponent<Animator>().ResetTrigger("stopAttack");
            GetComponent<Animator>().SetTrigger("attack");
            timeSinceLastAttack = 0; 
        }
    }
    public bool CanAttack(GameObject combatTarget)
    {
        if (combatTarget == null)
        {
            Debug.Log("CombatTarget Null");
            return false;
           
        }

        Health healtToTest = combatTarget.GetComponent<Health>();
        return healtToTest != null && !healtToTest.IsDead();
    }
    void Hit()
    {
        if(targetObject == null)
        {
            return;
        }
        Health healtToTest = GetComponent<Health>();
        targetObject.TakeDamage(weaponDamage);
    }
}
