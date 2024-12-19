using UnityEngine;

public class CameraSwitch : MonoBehaviour
{
	public Transform[] players;
	public Vector3 offset;
	public float smoothSpeed = 0.125f;
	private int currentPlayerIndex = -1;
	private int countPlayer;

	private bool isDied = false;
	void Start()
	{
		countPlayer = players.Length;
	}

	void Update()
	{
		if (isDied)
			return ;
		CheckLength();
		if (isDied)
			return ;
		if (currentPlayerIndex >= 0
			&& players[currentPlayerIndex] == null)
		{
			if (currentPlayerIndex >= 1 &&
				players[currentPlayerIndex - 1] != null)
				TrySwitchPlayer(currentPlayerIndex - 1);
			else
			{
				currentPlayerIndex = 0;
				while (!players[currentPlayerIndex])
				{
					currentPlayerIndex++;
					if (players[currentPlayerIndex])
					{
						TrySwitchPlayer(currentPlayerIndex);
						break ;
					}
				}
			}
		}
		if (Input.GetKeyDown(KeyCode.Alpha1)) TrySwitchPlayer(0);
		if (Input.GetKeyDown(KeyCode.Alpha2)) TrySwitchPlayer(1);
		if (Input.GetKeyDown(KeyCode.Alpha3)) TrySwitchPlayer(2);
	}

	void LateUpdate()
	{
		if (isDied)
			return ;
		if (currentPlayerIndex != -1)
		{
			Transform target = players[currentPlayerIndex];
			if (target != null)
			{
				Vector3 desiredPosition = target.position + offset;
				Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
				transform.position = smoothedPosition;
			}
		}
	}

	private void CheckLength()
	{
		int index = 0;
		int count = 0;

		while (index < countPlayer)
		{
			if (players[index] == null)
				count++;
			index++;
		}
		if (count == countPlayer)
		{
			isDied = true;
			currentPlayerIndex = -1;
		}
	}

	private void TrySwitchPlayer(int index)
	{
		if (index >= 0 && index < players.Length
			&& players[index] != null
			&& players[index].gameObject.activeInHierarchy)
		{
			if (currentPlayerIndex != -1)
			{
				if (players[currentPlayerIndex])
				{
					players[currentPlayerIndex].GetComponent<CharacterControll>().isActive = false;
				}
			}
			currentPlayerIndex = index;
			players[currentPlayerIndex].GetComponent<CharacterControll>().isActive = true;
		}
	}
}
