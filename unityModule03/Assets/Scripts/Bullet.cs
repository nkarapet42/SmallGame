using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 5f;
    private float damage = 1f;
    private GameObject targetEnemy;

    public void SetTarget(GameObject enemy)
    {
        targetEnemy = enemy;
    }

    public void SetDamage(float damageAmount)
    {
        damage = damageAmount;
    }

    public float GetDamage()
    {
        return damage;
    }

    void Update()
    {
        if (targetEnemy != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetEnemy.transform.position, speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetEnemy.transform.position) < 0.1f)
            {
                EnemyMovement enemy = targetEnemy.GetComponent<EnemyMovement>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                }
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
