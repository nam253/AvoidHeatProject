using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Controls;
using UnityEngine.Rendering;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs;

public class ItemRaycast : MonoBehaviour
{
    public float rayDistance = 10f;
    public Transform leftRayOrigin;
    public Transform rightRayOrigin;
    public GameObject infoPanel;

    public Text titleText;
    public Text bodyText;
    public Vector3 panelOffset = new Vector3(0, 5f, 0);

    [System.Serializable]
    public class itemDescriptions
    {
        public string title;
        public string body;
    }
    [System.Serializable]
    public class TagToDescription
    {
        public string tag;
        public itemDescriptions description;
    }
    [Header("설명 매핑")]
    public List<TagToDescription> descriptions;
    private Dictionary<string, itemDescriptions> descriptionMap;

    private Transform lastTarget;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (infoPanel != null)
        {
            infoPanel.SetActive(false);
        }
        descriptionMap = new Dictionary<string, itemDescriptions>();
        foreach (var entry in descriptions)
        {
            descriptionMap[entry.tag] = entry.description;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (leftRayOrigin != null) PerformRaycast(leftRayOrigin);
        if (rightRayOrigin != null) PerformRaycast(rightRayOrigin);
    }

    void PerformRaycast(Transform rayOrigin)
    {
        Ray ray = new Ray(rayOrigin.position, rayOrigin.forward);
        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance))
        {
            string tag = hit.collider.tag;

            if (descriptionMap.ContainsKey(tag))
            {
                if (lastTarget != hit.collider.transform)
                {
                    titleText.text = descriptionMap[tag].title;
                    bodyText.text = descriptionMap[tag].body;
                    lastTarget = hit.collider.transform;
                }
            }
            if (!infoPanel.activeSelf)
            {
                infoPanel.SetActive(true);
            }
            infoPanel.transform.position = hit.collider.transform.position + panelOffset;

            return;
        }
        if (lastTarget == null || !IsStillHitiing(lastTarget))
        {
            if (infoPanel.activeSelf)
            {
                infoPanel.SetActive(false);
            }
            lastTarget = null;
        }
    }

    bool IsStillHitiing(Transform target)
    {
        if (target == null) return false;

        Vector3 toTargetL = (target.position - leftRayOrigin.position).normalized;
        Vector3 toTargetR = (target.position - rightRayOrigin.position).normalized;

        float dotL = Vector3.Dot(leftRayOrigin.forward, toTargetL);
        float dotR = Vector3.Dot(rightRayOrigin.forward, toTargetR);

        return dotL > 0.95f || dotR> 0.95f;
    }
}
