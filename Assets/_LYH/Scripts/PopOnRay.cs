using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class PopOnRay : MonoBehaviour
{
    XRRayInteractor xrRayInteractor;
    RaycastHit raycastHit;
    GameObject raycastHitTarget;

    void Awake()
    {
        TryGetComponent<XRRayInteractor>(out xrRayInteractor);
    }

    void Update()
    {
        xrRayInteractor.TryGetCurrent3DRaycastHit(out raycastHit);

        if (raycastHit.collider.GetComponent<XRSimpleInteractable>() != null)
        {
            raycastHitTarget = raycastHit.collider.gameObject;

            
        }
    }
}
