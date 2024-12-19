using UnityEngine;

public class ExitButton : MonoBehaviour
{
    public void ExitGame()
    {
        Debug.Log("Exiting game...");

        Application.Quit();

        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #endif
    }
}
