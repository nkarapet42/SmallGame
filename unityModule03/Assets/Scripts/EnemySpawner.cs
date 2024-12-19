using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] waypoints;
    public float spawnInterval = 3f;

    private int countEnemy = 0;

    private static readonly int maxEnemy = 30;

    void Start()
    {
        InvokeRepeating(nameof(SpawnEnemy), 0f, spawnInterval);
    }

    void SpawnEnemy()
    {
        if (countEnemy >= maxEnemy
            && GameManager.Instance.enemyInMap <= 0)
            GameManager.Instance.statusEnd = true;
        if (countEnemy < maxEnemy)
        {
            countEnemy++;
            GameObject newEnemy = Instantiate(enemyPrefab, transform.position, Quaternion.identity);

            EnemyMovement enemyMovement = newEnemy.GetComponent<EnemyMovement>();
            if (enemyMovement != null)
            {
                enemyMovement.waypoints = waypoints;
            }
        }
    }
}
