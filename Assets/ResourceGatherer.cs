using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResourceGatherer : MonoBehaviour
{
    public float maxInteractDistance = 2f;
    public KeyCode interactKey = KeyCode.E;
    public LayerMask interactLayerMask;

    [SerializeField]
    private Text inventoryText;

    private Dictionary<Resource.ResourceType, int> inventory;

    private void Awake()
    {
        inventory = new Dictionary<Resource.ResourceType, int>
        {
            { Resource.ResourceType.Wood, 0 },
            { Resource.ResourceType.Stone, 0 },
            { Resource.ResourceType.Fabric, 0 },
            { Resource.ResourceType.Folliage, 0 },
            { Resource.ResourceType.Chest, 0 },
        };
    }

    void Update()
    {
        // Check if player presses interact key
        if (Input.GetKeyDown(interactKey))
        {
            // Check if there's an object in front of the player that can be interacted with
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit, maxInteractDistance, interactLayerMask))
            {
                // Check if the object has a "Resource" tag
                if (hit.collider.CompareTag("Resource"))
                {
                    // Get the resource type from the object's Resource component
                    Resource resource = hit.collider.gameObject.GetComponent<Resource>();

                    // Gather the resource
                    int amountGathered = resource.Gather();

                    // Add the resource to the player's inventory
                    if (resource.resourceType == Resource.ResourceType.Wood)
                    {
                        inventory[Resource.ResourceType.Wood] += amountGathered;
                    }
                    else if (resource.resourceType == Resource.ResourceType.Stone)
                    {
                        inventory[Resource.ResourceType.Stone] += amountGathered;
                    }
                    else if (resource.resourceType == Resource.ResourceType.Fabric)
                    {
                        inventory[Resource.ResourceType.Fabric] += amountGathered;
                    }
                    else if (resource.resourceType == Resource.ResourceType.Folliage)
                    {
                        inventory[Resource.ResourceType.Folliage] += amountGathered;
                    }
                    else if (resource.resourceType == Resource.ResourceType.Chest)
                    {
                        inventory[Resource.ResourceType.Chest] += amountGathered;
                    }

                    // Update the inventory text
                    UpdateInventoryText();
                }
            }
        }
    }

    private void UpdateInventoryText()
    {
        // Build a string with the current inventory contents
        string inventoryString = "Inventory:\n";
        inventoryString += "Wood: " + inventory[Resource.ResourceType.Wood] + "\n";
        inventoryString += "Fabric: " + inventory[Resource.ResourceType.Fabric] + "\n";
        inventoryString += "Folliage: " + inventory[Resource.ResourceType.Folliage] + "\n";
        inventoryString += "Chest: " + inventory[Resource.ResourceType.Chest] + "\n";
        inventoryString += "Stone: " + inventory[Resource.ResourceType.Stone];

        // Update the text component
        inventoryText.text = inventoryString;
    }

    // Add a given amount of a given resource to the player's inventory
    public void AddResource(Resource.ResourceType resourceType, int amount)
    {
        inventory[resourceType] += amount;
    }

    // Get the current count of a given resource in the player's inventory
    public int GetResourceCount(Resource.ResourceType resourceType)
    {
        return inventory[resourceType];
    }

    // Remove a given amount of a given resource from the player's inventory
    public void RemoveResource(Resource.ResourceType resourceType, int amount)
    {
        inventory[resourceType] -= amount;

        // Make sure the resource count doesn't go below 0
        if (inventory[resourceType] < 0)
        {
            inventory[resourceType] = 0;
        }
    }
}