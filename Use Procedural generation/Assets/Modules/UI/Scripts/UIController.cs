using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject[] UILayers;

    protected void ChangeTab(string layerName)
    {
        foreach (GameObject tmpLayer in UILayers)
        {
            if (tmpLayer.name != layerName)
            {
                tmpLayer.SetActive(false);
            }
            else
            {
                tmpLayer.SetActive(true);
            }
        }
    }

}
