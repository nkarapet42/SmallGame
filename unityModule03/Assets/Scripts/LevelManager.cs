using UnityEngine;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    public void LevelReply()
    {
        int index = GameManager.Instance.indexOfScene;
        Destroy(GameManager.Instance.gameObject);
        SceneManager.LoadScene(index);
    }

    public void NextLevel()
    {
        int sceneCount = SceneManager.sceneCountInBuildSettings;
        int index = GameManager.Instance.indexOfScene;
        Destroy(GameManager.Instance.gameObject);
        if (index < sceneCount - 3)
        {
            SceneManager.LoadScene(index + 1);
        }
        else
        {
            SceneManager.LoadScene("EndGame");
        }
    }

    public void HomePage()
    {
        SceneManager.LoadScene("Menu");
    }
}
