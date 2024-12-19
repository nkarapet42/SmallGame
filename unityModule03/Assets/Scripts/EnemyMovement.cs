using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypointIndex = 0;

    public float speed = 2f;
    
    [SerializeField]
    private float health = 3f;

    private bool init = false;

    void Update()
    {
        if (currentWaypointIndex < waypoints.Length)
        {
            Transform targetWaypoint = waypoints[currentWaypointIndex];
            transform.position = Vector2.MoveTowards(transform.position, targetWaypoint.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetWaypoint.position) < 0.1f)
            {
                currentWaypointIndex++;
            }
        }
        else
        {
            GameManager.Instance.enemyInMap--;
            Destroy(gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health > 0)
            Debug.Log("Enemy Health: " + health);
        if (health <= 0)
        {
            Debug.Log("Enemy Destroyed!");
            if (gameObject)
            {
                GameManager.Instance.totalKill++;
                GameManager.Instance.enemyInMap--;
            }
            Destroy(gameObject);
        }
    }
}
