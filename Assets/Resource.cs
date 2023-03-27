using UnityEngine;

public class Resource : MonoBehaviour
{
    public enum ResourceType
    {
        Wood,
        Stone,
        Fabric,
        Folliage,
        Chest
    }

    public ResourceType resourceType;
    public int maxAmount = 10;
    public int amountLeft = 10;

    public int Gather()
    {
        int amountGathered = Mathf.Min(1, amountLeft); // Gather 1 unit or less if there's not enough left
        amountLeft -= amountGathered;

        if (amountLeft <= 0)
        {
            Destroy(gameObject);
        }

        return amountGathered;
    }
}