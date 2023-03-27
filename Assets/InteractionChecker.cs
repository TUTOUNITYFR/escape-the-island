using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class InteractionChecker : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E;
    public int woodNeeded = 2;
    public int stoneNeeded = 2;
    public int fabricNeeded = 1;
    public int folliageNeeded = 3;
    public int chestNeeded = 1;
    public GameObject objectToDisable;
    public GameObject objectToEnable;
    public float interactionRange = 3f;

    public GameObject notEnoughResourcesText;

    private ResourceGatherer resourceGatherer;

    public GameObject victoryText;

    private float startTime;

    private void Start()
    {
        resourceGatherer = GetComponent<ResourceGatherer>();
        startTime = Time.time;
    }

    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            RaycastHit hit;
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out hit, interactionRange))
            {
                if (hit.collider.gameObject == objectToDisable)
                {
                    if(resourceGatherer.GetResourceCount(Resource.ResourceType.Wood) >= woodNeeded
                    && resourceGatherer.GetResourceCount(Resource.ResourceType.Stone) >= stoneNeeded
                    && resourceGatherer.GetResourceCount(Resource.ResourceType.Fabric) >= fabricNeeded
                    && resourceGatherer.GetResourceCount(Resource.ResourceType.Folliage) >= folliageNeeded
                    && resourceGatherer.GetResourceCount(Resource.ResourceType.Chest) >= chestNeeded)
                    {
                        resourceGatherer.RemoveResource(Resource.ResourceType.Wood, woodNeeded);
                        resourceGatherer.RemoveResource(Resource.ResourceType.Stone, stoneNeeded);
                        objectToDisable.SetActive(false);
                        objectToEnable.SetActive(true);

                        float duration = Time.time - startTime;
                        string durationString = string.Format("{0:0.00}", duration);
                        victoryText.SetActive(true);
                        victoryText.GetComponent<Text>().text = "You won! Time: " + durationString;
                    }
                    else
                    {
                        notEnoughResourcesText.SetActive(true);
                        StartCoroutine(HideText());
                    }
                }
            }
        }
    }

    private IEnumerator HideText()
    {
        yield return new WaitForSeconds(2f);
        notEnoughResourcesText.SetActive(false);
    }
}