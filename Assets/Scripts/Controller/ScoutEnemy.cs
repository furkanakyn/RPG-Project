using UnityEngine;

public class ScoutEnemy : MonoBehaviour
{
    [SerializeField] float visionRange = 15f; // G�zlem mesafesi
    [SerializeField] LayerMask enemyLayer; // Sava��� d��manlar�n katman�
    [SerializeField] LayerMask playerLayer; // Oyuncu katman�

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
        // G�zc� d��man yak�n�ndaki di�er d��manlar� uyar�yor
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, visionRange, enemyLayer);

        // E�er etrafta d��man yoksa fonksiyondan ��k
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
