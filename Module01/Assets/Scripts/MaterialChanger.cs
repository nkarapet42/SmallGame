using UnityEngine;

public class MaterialChanger : MonoBehaviour
{
	[SerializeField]
	private Renderer[] objectsToChange;
	[SerializeField]
	private Material newMaterial = null;

	public void ChangeMaterial(string materialName)
	{
		newMaterial = Resources.Load<Material>($"Materials/MyMaterial/{materialName}");
		if (objectsToChange == null || newMaterial == null)
		{
			Debug.LogWarning("Не указаны объекты или материал для изменения.");
			return;
		}
		foreach (Renderer obj in objectsToChange)
		{
			if (obj != null)
			{
				obj.material = newMaterial;
			}
		}
	}
}
