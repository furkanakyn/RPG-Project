using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class ScoutEnemy : MonoBehaviour
{
    [SerializeField] float visionRange = 15f; // Gözlem mesafesi
    [SerializeField] LayerMask enemyLayer; // Savaþçý düþmanlarýn katmaný
    [SerializeField] LayerMask playerLayer; // Oyuncu katmaný

    private GameObject player;
    private Mover mover;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
        mover = GetComponent<Mover>();
    }

    void Update()
    {
        if (PlayerInSight())
        {
            AlertNearbyEnemies();

            Debug.Log("Scout spotted the player!");
        }
    }

    private bool PlayerInSight()
    {
        
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, visionRange, playerLayer);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.gameObject == player)
            {
                return true;
            }
        }
        return false;
    }

    private void AlertNearbyEnemies()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, visionRange, enemyLayer);

        
        if (hitColliders.Length == 0)
        {
            Debug.LogWarning("No enemies found to alert.");
            return;
        }

        foreach (var hitCollider in hitColliders)
        {
            AIController enemyAI = hitCollider.GetComponent<AIController>();
            Health enemyHealth = hitCollider.GetComponent<Health>();
            if (enemyAI != null && enemyHealth != null && hitCollider.gameObject != this.gameObject) 
            {
                enemyAI.Alert(player);
                Debug.Log("Alerting enemy: " + hitCollider.name);
            }
            else
            {
                Debug.LogWarning("Enemy found, but no AIController component.");
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, visionRange);
    }
}
