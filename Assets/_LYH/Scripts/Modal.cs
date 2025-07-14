using System.Collections.Generic;
using Unity.XR.CoreUtils;
using UnityEngine;

public class Modal : MonoBehaviour
{
    public bool isRaycastHit { get; set; }
    GameObject modal;

    void Start()
    {
        modal = GetComponentInChildren<Modal>().gameObject;
    }

    void Update()
    {
        if (isRaycastHit)
        {
            
        }
    }
}
