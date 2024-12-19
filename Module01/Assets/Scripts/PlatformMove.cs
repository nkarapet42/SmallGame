using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
	public Vector3 pointA;
	public Vector3 pointB;
	public float speed = 2f;
	private Vector3 target;
	private Transform originalParent;

	void Start()
	{
		target = pointB;
	}

	void FixedUpdate()
	{
	
		transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

	
		if (Vector3.Distance(transform.position, target) < 0.01f)
		{
			target = (target == pointA) ? pointB : pointA;
		}
	}

	private void OnTriggerEnter(Collider collider)
	{
		originalParent = collider.transform.parent;
		if (collider.gameObject.CompareTag("Player"))
		{
			collider.transform.SetParent(transform, true);
		}
	}

	private void OnTriggerExit(Collider collider)
	{
		if (collider.gameObject.CompareTag("Player"))
		{
			collider.transform.SetParent(originalParent, true);
		}
		originalParent = null;
	}
}