using UnityEngine;

public class AIController : MonoBehaviour
{
    private Combat combat;
    private Health health;
    private GameObject player;
    private Mover mover;
    private ActionScheduler actionScheduler;
    [SerializeField] PatrolPath patrolPath;
    [SerializeField] float followDistance;
    [SerializeField] float suspicionTime;
    [SerializeField] float wayPointTolorence = 1f;
    [SerializeField] float wayPointStopTime = 2f;
    [Range(0,1)][SerializeField] float patrolSpeedFraction = 0.2f;
    private Vector3 enemyLocation;
    private float timeSinceLastSawPlayer = 0f;
    private float timeSinceArrivedWayPoint = 0f;
    private int currentWayPointIndex = 0;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        health = GetComponent<Health>();
        combat = GetComponent<Combat>();
        mover = GetComponent<Mover>();
        actionScheduler = GetComponent<ActionScheduler>();
        enemyLocation = transform.position;
    }

    void Update()
    {
        if (health.IsDead()) return;

        if (DistanceToPlayer() < followDistance && combat.CanAttack(player))
        {
            timeSinceLastSawPlayer = 0;
            combat.Attack(player);
            Debug.Log("Attacking player.");
        }
        else if (timeSinceLastSawPlayer < suspicionTime)
        {
            actionScheduler.CancelCurrentAction();
        }
        else
        {
            Vector3 nextPosition = enemyLocation;
            if (patrolPath != null)
            {
                if (AtWayPoint())
                {
                    timeSinceArrivedWayPoint = 0;
                    CycleWayPoint();

                }
                nextPosition = GetNextWayPoint();
            }
            if (timeSinceArrivedWayPoint > wayPointStopTime)
            {
                mover.MoveTo(nextPosition,patrolSpeedFraction);
            }
        }
        timeSinceLastSawPlayer += Time.deltaTime;
        timeSinceArrivedWayPoint += Time.deltaTime;
    }
    public void Alert(GameObject targetPlayer)
    {
        player = targetPlayer;
        Debug.Log("Enemy alerted by scout!");
        if (combat.CanAttack(player))
        {
            combat.Attack(player);
        }
    }

    private bool AtWayPoint()
    {
        float distanceWayPoint = Vector3.Distance(transform.position, GetNextWayPoint());
        return distanceWayPoint < wayPointTolorence;
    }

    private void CycleWayPoint()
    {
        currentWayPointIndex = patrolPath.GetNextIndex(currentWayPointIndex);
    }

    private Vector3 GetNextWayPoint()
    {
        return patrolPath.GetWayPointPosition(currentWayPointIndex);
    }

    private float DistanceToPlayer()
    {
        return Vector3.Distance(player.transform.position, transform.position);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, followDistance);
    }
}
