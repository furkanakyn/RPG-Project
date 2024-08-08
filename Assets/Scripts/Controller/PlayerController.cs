using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public LayerMask enemyLayerMask;
    public LayerMask groundLayerMask;
    private Mover mover;
    private Combat combat;
    private ActionScheduler actionScheduler;
    private Health health;

    void Start()
    {
        mover = GetComponent<Mover>();
        combat = GetComponent<Combat>();
        health = GetComponent<Health>();
        actionScheduler = GetComponent<ActionScheduler>();
    }

    void Update()
    {
        if (health.IsDead()) return;
        if (InteractWithCombat()) return;
        if (InteractWithMovement()) return;
    }

    private bool InteractWithMovement()
    {
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            bool moveHit = Physics.Raycast(GetMouseRay(), out hit, 100, groundLayerMask);
            if (moveHit)
            {
                mover.MoveTo(hit.point);
                actionScheduler.StartAction(mover); 
                combat.Cancel();
                return true;
            }
        }
        return false;
    }

    private bool InteractWithCombat()
    {
    
        if (Input.GetMouseButton(1))
        {
            RaycastHit hit;
            bool hitEnemy = Physics.Raycast(GetMouseRay(), out hit, 100, enemyLayerMask);

            if (hitEnemy)
            {
                CombatTarget target = hit.collider.GetComponent<CombatTarget>();
                if (combat.CanAttack(target.gameObject))
                {
                    combat.Attack(hit.collider.gameObject);
                    actionScheduler.StartAction(combat);
                    return true;
                }
            }
        }
        return false;
    }


    private static Ray GetMouseRay()
    {
     
        return Camera.main.ScreenPointToRay(Input.mousePosition);
    }
}
