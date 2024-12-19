using System.Collections;
using UnityEngine;

public class LeverControll : MonoBehaviour
{
	[SerializeField]
	private Animator leverAnimator;
	[SerializeField]
	private Animator gateAnimator;
	[SerializeField]
	private MaterialChanger materialChanger;
	public  string playerName = null;
	private bool isPlayerInTrigger = false;
	private bool isLeverOpen = false;
	private bool isAnimating = false;
	[SerializeField]
	private bool isPlayer = false;

	private void OnTriggerEnter(Collider other)
	{
		CharacterControll playerController = other.GetComponent<CharacterControll>();
		if (other.CompareTag("Player")
			&& LayerMask.LayerToName(other.gameObject.layer) == playerName
			&& playerController.isActive)
		{
			isPlayerInTrigger = true;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (!isPlayer)
		{
			if (Input.GetKeyDown(KeyCode.E))
			{
				playerName = LayerMask.LayerToName(other.gameObject.layer);
				isPlayer = true;
				ChangeColor();
			}
			else
				return ;
		}
		if (other.CompareTag("Player")
			&& LayerMask.LayerToName(other.gameObject.layer) == playerName)
		{
			CharacterControll playerController = other.GetComponent<CharacterControll>();
			if (playerController != null)
			{
				if (playerController.isActive)
				{
					isPlayerInTrigger = true;
				}
				else
				{
					isPlayerInTrigger = false;
				}
			}
		}
	}

	private void OnTriggerExit(Collider other)
	{
		if (other.CompareTag("Player")
			&& LayerMask.LayerToName(other.gameObject.layer) == playerName)
		{
			isPlayerInTrigger = false;
		}
	}

	private void Update()
	{
		if (leverAnimator != null && isPlayerInTrigger)
		{
			if (leverAnimator.GetCurrentAnimatorStateInfo(0).IsName("LeverOpen") ||
				leverAnimator.GetCurrentAnimatorStateInfo(0).IsName("LeverClose"))
			{
				if (leverAnimator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1.0f)
				{
					OnAnimationComplete();
				}
			}
		}
		if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E) && !isAnimating)
		{
			ToggleLever();
		}
	}

	private void ToggleLever()
	{
		if (leverAnimator == null || gateAnimator == null)
		{
			Debug.LogWarning("Animator не назначен на LeverControl.");
			return;
		}

		isAnimating = true;
		if (isLeverOpen)
		{
			leverAnimator.Play("LeverClose", 0, 0.0f);
			StartCoroutine(PlayGateAnimationWithDelay("CloseGate", 0.5f));
		}
		else
		{
			leverAnimator.Play("LeverOpen", 0, 0.0f);
			StartCoroutine(PlayGateAnimationWithDelay("OpenGate", 0.5f));
		}

		isLeverOpen = !isLeverOpen;
	}

	private IEnumerator PlayGateAnimationWithDelay(string gateAnimationName, float delay)
	{
		yield return new WaitForSeconds(delay);
		gateAnimator.Play(gateAnimationName, 0, 0.0f);
	}

	private void ChangeColor()
	{
		if (materialChanger != null)
		{
			materialChanger.ChangeMaterial(playerName);
		}
		else
		{
			Debug.LogWarning("Скрипт MaterialChanger не назначен!");
		}
	}
	public void OnAnimationComplete()
	{
	    isAnimating = false;
	}
}
