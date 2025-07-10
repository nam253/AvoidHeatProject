using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit.Interactors;

public class CharacterRaycaster : MonoBehaviour
{
    public GameObject uiCanvasPrefab;
    public string targetTag = "Character";
    public float raycastDistance = 10f;

    public InputActionReference raycastActivateAction;

    private XRRayInteractor rayInteractor;

    private GameObject currentUICanvasInstance;

    private bool uiActive = false;

    private bool uiManuallyClosed = false;

    void Awake()
    {
        rayInteractor = GetComponent<XRRayInteractor>();
        if (rayInteractor == null)
        {
            enabled = false;
            return;
        }
        if (raycastActivateAction != null && raycastActivateAction.action != null)
        {
            raycastActivateAction.action.performed += OnRaycastActivate;
            raycastActivateAction.action.Enable();
        }
        if (uiCanvasPrefab != null)
        {
            uiCanvasPrefab.SetActive(false);
        }
    }

    void OnDestroy()
    {
        if (raycastActivateAction != null && raycastActivateAction.action != null)
        {
            raycastActivateAction.action.performed -= OnRaycastActivate;
            raycastActivateAction.action.Disable();
        }
    }

    private void OnRaycastActivate(InputAction.CallbackContext context)
    {
        PerformRaycast();
    }

    void PerformRaycast()
    {
        if (uiManuallyClosed)
        {
            return;
        }
        RaycastHit hit;
        Vector3 rayOrigin = transform.position;
        Vector3 rayDirection = transform.forward;

        if (Physics.Raycast(rayOrigin, rayDirection, out hit, raycastDistance))
        {
            if (hit.collider != null && hit.collider.CompareTag(targetTag))
            {
                if (uiCanvasPrefab != null)
                {
                    GameObject currentUICanvasInstance = Instantiate(uiCanvasPrefab, transform.position + transform.forward * 1f, Quaternion.identity);
                    Closebutton closebuttonScript = currentUICanvasInstance.GetComponentInChildren<Closebutton>(true);

                    if (closebuttonScript != null)
                    {
                        closebuttonScript.closeButtonUI = currentUICanvasInstance;
                        closebuttonScript.allCharacterRaycasters = FindObjectsOfType<CharacterRaycaster>();
                    }

                    currentUICanvasInstance.SetActive(true);

                    uiActive = true;
                }
                else if (!uiActive && currentUICanvasInstance != null)
                {
                    currentUICanvasInstance.SetActive(true);
                    uiActive = true;
                }


            }


        }

    }
    public void SetUiManuallyClosed(bool closed)
    {
        uiManuallyClosed = closed;

        if (closed)
        {
            uiActive = false;

            if (currentUICanvasInstance != null && currentUICanvasInstance.activeSelf)
            {
                currentUICanvasInstance.SetActive(false);
            }
        }
    }
    public void SetUiActive(bool activeState)
    {
        uiActive = activeState;
        Debug.Log($"CharacterRaycaster: uiActive set to: {activeState}");
    }
    
}
