using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour, IAction
{
    private NavMeshAgent agent;
    private Animator animator;
    private ActionScheduler actionScheduler;
    private Combat combat;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        actionScheduler = GetComponent<ActionScheduler>(); 
        combat = GetComponent<Combat>();
    }

    void Update()
    {
        UpdateAnimator();
    }

    public void MoveTo(Vector3 destination)
    {
        agent.destination = destination;
        agent.isStopped = false;
        
    }

    public void Cancel()
    {
        agent.isStopped = true;
    }


    private void UpdateAnimator()
    {
        Vector3 velocity = agent.velocity;
        Vector3 localVelocity = transform.InverseTransformDirection(velocity);
        float speed = localVelocity.z;
        animator.SetFloat("forwardSpeed", speed);
    }
}
