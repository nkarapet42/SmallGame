using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneEditor : MonoBehaviour
{
	public float cooldownTime = 5.0f;
	private float nextResetTime = 0.0f;
	public static SceneEditor Instance { get; private set; }

	void Awake()
	{
		if (Instance == null)
		{
			Instance = this;
			DontDestroyOnLoad(gameObject);
		}
		else
		{
			Destroy(gameObject);
		}
	}

	void Update()
	{
		if (Time.time >= nextResetTime)
		{
			if (Input.GetKeyDown(KeyCode.R)
				|| Input.GetKeyDown(KeyCode.Backslash))
			{
				ResetScene();
			}
		}
	}

	public void ResetScene()
	{
		nextResetTime = Time.time + cooldownTime;
		SceneManager.LoadScene(SceneManager.GetActiveScene().name);
	}
}
