using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
	public GameObject[] players;

	private Dictionary<GameObject, Vector3> initialPositions = new Dictionary<GameObject, Vector3>();

	private void Start()
	{
		foreach (var player in players)
		{
			if (player != null && !initialPositions.ContainsKey(player))
			{
				initialPositions[player] = player.transform.position;
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (initialPositions.ContainsKey(other.gameObject))
		{
			other.gameObject.transform.position = initialPositions[other.gameObject];
		}
	}
}
