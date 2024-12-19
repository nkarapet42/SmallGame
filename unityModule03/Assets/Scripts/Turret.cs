using Unity.VisualScripting;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float fireRate = 1f;
    public float damage = 1f;
    public float bulletSpeed = 5f;

    private float nextFireTime = 0f;
    private GameObject targetEnemy = null;

    public Vector2 fireOffset = new Vector2(1f, 0f);

    void Update()
    {
        if (targetEnemy != null)
        {
            if (Time.time >= nextFireTime)
            {
                Shoot();
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && targetEnemy != null)
        {
            Vector2 firePoint = (Vector2)transform.position + fireOffset;

            GameObject bullet = Instantiate(bulletPrefab, firePoint, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.SetTarget(targetEnemy);
                bulletScript.SetDamage(damage);
                bulletScript.speed = bulletSpeed;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (targetEnemy == null && other.CompareTag("Enemy"))
        {
            targetEnemy = other.gameObject;
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (targetEnemy == null && other.CompareTag("Enemy"))
        {
            targetEnemy = other.gameObject;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (targetEnemy == other.gameObject)
        {
            targetEnemy = null;
        }
    }
}
