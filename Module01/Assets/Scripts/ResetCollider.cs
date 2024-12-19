using UnityEngine;

public class ResetCollider : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        SceneEditor.Instance.ResetScene();
    }
}
