using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class StageCompletion : MonoBehaviour
{
	public Transform[] players; 
	public Collider[] exitZones; 
	private Dictionary<int, float> playerTimers = new Dictionary<int, float>();
	private HashSet<int> playersAligned = new HashSet<int>(); 
	private float requiredTime = 3f;

	private bool exitStatus = false;

	void Start()
	{
		Debug.Log("Started with " + players.Length + " players");
		for (int i = 0; i < players.Length; i++)
		{
			playerTimers[i] = 0f;
		}
	}

	void Update()
	{
		if (!exitStatus && playersAligned.Count == players.Length)
		{
			exitStatus = true;
			Debug.Log("Stage Completed");
			LoadNextLevel();
		}
	}

	private void OnTriggerStay(Collider other)
	{
	
		for (int i = 0; i < players.Length; i++)
		{
			if (other.transform == players[i])
			{
				if (IsPlayerInZone(players[i], exitZones[i]))
				{
					playerTimers[i] += Time.deltaTime; 

					if (playerTimers[i] >= requiredTime && !playersAligned.Contains(i))
					{
						playersAligned.Add(i);
						Debug.Log(players[i].name + " is aligned!");
					}
				}
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
	
		for (int i = 0; i < players.Length; i++)
		{
			if (other.transform == players[i])
			{
				playerTimers[i] = 0f; 
				if (playersAligned.Contains(i))
				{
					playersAligned.Remove(i);
					Debug.Log(players[i].name + " left the zone!");
					exitStatus = false;
				}
			}
		}
	}

	private bool IsPlayerInZone(Transform player, Collider zone)
	{
		return zone.bounds.Contains(player.position); 
	}

	private void LoadNextLevel()
	{
		int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
		int nextSceneIndex = (currentSceneIndex + 1) % SceneManager.sceneCountInBuildSettings;

		Debug.Log($"Loading next stage: {nextSceneIndex}");
		SceneManager.LoadScene(nextSceneIndex);
	}

}
