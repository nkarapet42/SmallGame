using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DragAndDropManager : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private RectTransform rectTransform;
    private Canvas canvas;

    private Vector2 initialPosition;

    [SerializeField]
    public GameObject turretPrefab = null;

    [SerializeField]
    private int turretCost;

    [SerializeField]
    private Button button;

    private Color red;

    void Start()
    {
        red =  new Color(0.235f, 0f, 0f);
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
        initialPosition = rectTransform.anchoredPosition;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (button.GetComponent<Image>().color != red)
            rectTransform.anchoredPosition += eventData.position - (Vector2)rectTransform.position;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);

        if (hit.collider != null && hit.collider.CompareTag("TurretPoint")
            && hit.collider.GetComponent<PlacementZone>().CanPlaceTurret())
        {
            hit.collider.GetComponent<PlacementZone>().PlaceTurret(turretPrefab, turretCost);
            rectTransform.anchoredPosition = initialPosition;
        }
        else
        {
            rectTransform.anchoredPosition = initialPosition;
        }
    }
}
