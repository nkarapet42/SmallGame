using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KillCounterDisplay : MonoBehaviour
{
    public TextMeshProUGUI killTextTMP;

    void Update()
    {
        if (GameManager.Instance != null)
        {
            killTextTMP.text = GameManager.Instance.totalKill.ToString();
        }
    }
}
