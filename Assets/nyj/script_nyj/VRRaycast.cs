using Unity.XR.Oculus;
using UnityEngine;
using UnityEngine.UI;

public class VRRaycast : MonoBehaviour
{
    public float rayDistance = 10f;
    //public LayerMask targetLayer;
    public string targetTag = "Character";
    public GameObject uiPanel;
    public Button closeButton;

    public GameObject missionPanel;
    private bool uiShownOnce = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
        if (closeButton != null)
        {
            closeButton.onClick.AddListener(CloseUIPanel);
        }
        if (missionPanel != null)
        {
            missionPanel.SetActive(false);
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (!uiShownOnce)
        {
            PerformRaycast();
        }

    }

    void PerformRaycast()
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, rayDistance))
        {
            if (hit.collider.CompareTag(targetTag))
            {
                uiPanel.SetActive(true);
                uiShownOnce = true;
            }
        }
    }

    public void CloseUIPanel()
    {
        if (uiPanel != null)
        {
            uiPanel.SetActive(false);
        }
        if (missionPanel != null)
        {
            missionPanel.SetActive(true);
        }
    }
}
