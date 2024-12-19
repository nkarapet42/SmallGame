using UnityEngine;

public class Base : MonoBehaviour
{
    [SerializeField]
    private GameObject[] health = null;

    private int index;

    private void Start()
    {
        index = health.Length - 1;
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            if (index >= 0)
            {
                health[index].SetActive(false);
                index--;
            }
            GameManager.Instance.TakeDamageOnBase();
        }
    }
}
