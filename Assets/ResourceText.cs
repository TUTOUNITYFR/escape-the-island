using UnityEngine;
using UnityEngine.UI;

public class ResourceText : MonoBehaviour
{
    public float maxDistance = 5f;  // Maximum distance to detect resources
    public LayerMask layerMask;     // Layer mask to detect resources
    public Text textObject;         // UI text object to display the message

    private void Update()
    {
        // Cast a ray from the center of the screen
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, maxDistance, layerMask))
        {
            // If the ray hits a resource object, display the message
            if (hit.collider.CompareTag("Resource") || hit.collider.CompareTag("Boat"))
            {
                textObject.text = "Press E to interact";
                textObject.gameObject.SetActive(true);
            }
            else
            {
                textObject.gameObject.SetActive(false);
            }
        }
        else
        {
            textObject.gameObject.SetActive(false);
        }
    }
}