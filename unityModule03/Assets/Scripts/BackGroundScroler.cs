using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{
    public float scrollSpeed = 0.1f;
    private Vector2 startPosition;

    void Start()
    {
        startPosition = transform.position;
    }

    void Update()
    {
        float offset = Mathf.Sin(Time.time * scrollSpeed) * 0.5f;
        transform.position = startPosition + new Vector2(offset, 0);
    }
}
