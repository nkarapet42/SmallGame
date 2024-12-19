using UnityEngine;

public class CubeFollower : MonoBehaviour
{
	private Transform standingObject;
	private Transform originalParent;

	void OnCollisionEnter(Collision collision)
	{
		originalParent = collision.transform.parent;
		if (collision.gameObject.CompareTag("Player"))
		{
			foreach (ContactPoint contact in collision.contacts)
			{
				if (Vector3.Dot(contact.normal, Vector3.down) > 0.9f)
				{
					standingObject = collision.transform;
					standingObject.SetParent(transform);
					break;
				}
			}
		}
	}

	void OnCollisionExit(Collision collision)
	{
		if (collision.transform == standingObject)
		{
			standingObject.SetParent(originalParent);
			standingObject = null;
			originalParent = null;
		}
	}
}
