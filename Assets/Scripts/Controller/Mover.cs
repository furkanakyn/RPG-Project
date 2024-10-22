using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour, IAction
{
    private NavMeshAgent agent;
    private Animator animator;
    private Health health;
    private ActionScheduler actionScheduler;
    private Combat combat;
    [SerializeField] float maxSpeed = 6f;

    private void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        health = GetComponent<Health>();
        actionScheduler = GetComponent<ActionScheduler>(); 
        combat = GetComponent<Combat>();
    }

    void Update()
    {
        agent.enabled = !health.IsDead();
        UpdateAnimator();
    }

    public void MoveTo(Vector3 destination,float speedFraction)
    {
        if (!agent.isActiveAndEnabled) return;
        if(gameObject.name == "Queen" )
        {
          
        }
        agent.speed = maxSpeed * speedFraction;
        agent.destination = destination;
        agent.isStopped = false;

    }

    public void Cancel()
    {
        if (!agent.isActiveAndEnabled) return;

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
