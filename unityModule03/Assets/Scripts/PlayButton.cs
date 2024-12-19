using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public Button playButton;

    void Start()
    {
        playButton.interactable = true;
    }

    public void PlayGame()
    {
        playButton.interactable = false;
        SceneManager.LoadScene(1);
    }
}
