using UnityEngine;

public class PlacementZone : MonoBehaviour
{
    public bool isOccupied = false;

    public bool CanPlaceTurret()
    {
        return !isOccupied;
    }

    public void PlaceTurret(GameObject turretPrefab, int turretCost)
    {

        if (GameManager.Instance.ConsumeEnergy(turretCost))
        {
            isOccupied = true;
            GameObject turret = Instantiate(turretPrefab, transform.position, Quaternion.identity);
            turret.transform.SetParent(transform.parent);
            gameObject.SetActive(false);
        }
        else
        {
            Debug.Log("Not enough energy to place turret!");
        }
    }
}
