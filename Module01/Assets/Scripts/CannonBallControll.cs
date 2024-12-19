using UnityEngine;

public class CannonBallControll : MonoBehaviour
{
	[SerializeField]
	private Material[] materials = null;

    private string   ballMaterial = null;
    
    private void OnTriggerEnter(Collider player)
    {
        if (LayerMask.LayerToName(player.gameObject.layer) == ballMaterial)
        {
            Debug.Log(player.name + " Die");
            Destroy(player.gameObject);
            SceneEditor.Instance.ResetScene();
        }
    }

    public void ChangeMaterial()
    {
        if (materials == null || materials.Length == 0)
        {
            Debug.LogWarning("Список материалов пуст или не назначен.");
            return;
        }

        Material selectedMaterial = materials[Random.Range(0, materials.Length)];

        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
            renderer.material = selectedMaterial;
        ballMaterial = selectedMaterial.name;
    }
}
