using UnityEngine;

public class BarInformation : MonoBehaviour
{
    public GameObject[] ranks;

    public GameObject[] result;
    public GameObject nextLevel = null;

    private void Start()
    {
        for (int i = 0; i < ranks.Length; i++)
            ranks[i].SetActive(false);
        for (int i = 0; i < result.Length; i++)
            result[i].SetActive(false);
        nextLevel.SetActive(false);
        ShowResult();       
    }

    private void ShowResult()
    {
        if (!GameManager.Instance.statusExit)
        {
            result[0].SetActive(true);
            ranks[0].SetActive(true);
            return ;
        }
        else if (GameManager.Instance.baseHealth == 1
                || GameManager.Instance.baseHealth == 2)
            ranks[1].SetActive(true);
        else if (GameManager.Instance.baseHealth == 3)
            ranks[2].SetActive(true);
        else if (GameManager.Instance.baseHealth == 4)
            ranks[3].SetActive(true);
        else if (GameManager.Instance.baseHealth == 5)
            ranks[4].SetActive(true);
        result[1].SetActive(true);
        nextLevel.SetActive(true);
    }
}
