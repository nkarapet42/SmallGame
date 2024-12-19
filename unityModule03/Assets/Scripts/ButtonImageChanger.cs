using UnityEngine;
using UnityEngine.UI;

public class ButtonImageChanger : MonoBehaviour
{
    [SerializeField]
    private Button button;

    [SerializeField]
    private Color activeColor = Color.white;

    private Color inactiveColor;

    [SerializeField]
    private int eneryRecover;

    private void Start()
    {
        inactiveColor = new Color(0.235f, 0f, 0f);
    }
    private void Update()
    {
        if (button != null)
        {
            Image buttonImage = button.GetComponent<Image>();
            if (buttonImage != null)
            {
                if (GameManager.Instance.currentEnergy >= eneryRecover)
                {
                    buttonImage.color = activeColor;
                }
                else
                {
                    buttonImage.color = inactiveColor;
                }
            }
        }
    }
}
