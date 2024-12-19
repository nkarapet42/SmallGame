using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public float baseHealth = 5f;
    public int maxEnergy = 5;
    public int currentEnergy;
    public int energyRecoveryAmount = 5;
    public float energyRecoveryTime = 10f;
    public GameObject[] energyIndicators;
    private float energyRecoveryTimer;
    public bool statusExit = false;
    public bool statusEnd = false;
    public int totalKill = 0;
    public int enemyInMap = 30;

    public int indexOfScene;
    private bool temp = true;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        indexOfScene = SceneManager.GetActiveScene().buildIndex;
        currentEnergy = maxEnergy;
        if (energyIndicators != null)
            UpdateEnergyIndicators();
        energyRecoveryTimer = energyRecoveryTime;
    }

    void Update()
    {
        if (!temp)
            return ;
        if (gameObject == null)
        {
            return ;
        }
        if (totalKill >= 30 || statusEnd)
        {
            temp = false;
            statusExit = true;
            SceneManager.LoadScene("Score");
        }
        if (energyIndicators != null)
            RecoverEnergyOverTime();
    }

    public void TakeDamageOnBase()
    {
        baseHealth--;
        if (baseHealth > 0)
            Debug.Log("Base Health: " + baseHealth);
        if (baseHealth <= 0)
        {
            Debug.Log("Base Destroyed!");
            temp = false;
            SceneManager.LoadScene("Score");
        }
    }

    public bool ConsumeEnergy(int amount)
    {
        if (currentEnergy >= amount)
        {
            currentEnergy -= amount;
            UpdateEnergyIndicators();
            return true;
        }
        else
        {
            Debug.Log("Not enough energy!");
            return false;
        }
    }

    private void RecoverEnergyOverTime()
    {
        if (currentEnergy < maxEnergy)
        {
            energyRecoveryTimer -= Time.deltaTime;
            if (energyRecoveryTimer <= 0f)
            {
                currentEnergy += energyRecoveryAmount;
                if (currentEnergy > maxEnergy)
                {
                    currentEnergy = maxEnergy;
                }
                UpdateEnergyIndicators();
                energyRecoveryTimer = energyRecoveryTime;
            }
        }
    }

    private void UpdateEnergyIndicators()
    {
        for (int i = 0; i < energyIndicators.Length; i++)
        {
            if (i < currentEnergy)
            {
                energyIndicators[i].SetActive(true);
            }
            else
            {
                energyIndicators[i].SetActive(false);
            }
        }
    }

    void EndGame()
    {
        Debug.Log("Game Over");
        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
