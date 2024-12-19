using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public GameObject confirmQuitUI;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
                PauseGame();
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        confirmQuitUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void ShowConfirmQuit()
    {
        pauseMenuUI.SetActive(false);
        confirmQuitUI.SetActive(true);
    }

    public void CloseConfirmQuit()
    {
        pauseMenuUI.SetActive(true);
        confirmQuitUI.SetActive(false);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
