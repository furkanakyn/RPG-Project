using UnityEngine;

public class ScoutEnemy : MonoBehaviour
{
    [SerializeField] float visionRange = 15f; // Gözlem mesafesi
    [SerializeField] LayerMask enemyLayer; // Savaþçý düþmanlarýn katmaný
    [SerializeField] LayerMask playerLayer; // Oyuncu katmaný

    private GameObject player;

    void Start()
    {
        player = GameObject.FindWithTag("Player");
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
        // Gözcü düþman yakýnýndaki diðer düþmanlarý uyarýyor
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, visionRange, enemyLayer);

        // Eðer etrafta düþman yoksa fonksiyondan çýk
        if (hitColliders.Length == 0)
        {
            Debug.LogWarning("No enemies found to alert.");
            return;
        }

        foreach (var hitCollider in hitColliders)
        {
            AIController enemyAI = hitCollider.GetComponent<AIController>();
            if (enemyAI != null && hitCollider.gameObject != this.gameObject) // Kendini kontrol etmeyi unutma
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
