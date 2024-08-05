using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Combat : MonoBehaviour, IAction
{
    public GameObject targetObject; 
    public float weaponRange = 2f; 
    private Mover mover;
    ActionScheduler actionScheduler;
    IAction action;


    private void Start()
    {
        mover = GetComponent<Mover>();
        actionScheduler = GetComponent<ActionScheduler>();
        
    }

    public void Attack(GameObject target)
    {
        targetObject = target;
        actionScheduler.StartAction(this); // Combat'ý aktif et
    }

    public void Cancel()
    {
        targetObject = null; // Saldýrýyý iptal et
 
    }

    void Update()
    {
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
                mover.Cancel(); 
            }
        }
    }

}
