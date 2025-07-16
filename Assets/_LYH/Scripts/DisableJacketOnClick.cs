using UnityEngine;
using UnityEngine.InputSystem;

public class DisableJacketOnClick : MonoBehaviour
{
    [Tooltip("버튼 입력용 Action(Trigger 등)")]
    public InputActionProperty clickAction;

    [Tooltip("교체할 캐릭터 Prefab")]
    public GameObject replacementPrefab;

    [Tooltip("레이캐스트 최대 거리")]
    public float maxDistance = 10f;

    [Tooltip("감지할 레이어")]
    public LayerMask targetLayer;

    void OnEnable()
    {
        clickAction.action.Enable();
        clickAction.action.performed += OnClick;
    }

    void OnDisable()
    {
        clickAction.action.performed -= OnClick;
        clickAction.action.Disable();
    }

    private void OnClick(InputAction.CallbackContext ctx)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        if (Physics.Raycast(ray, out var hit, maxDistance, targetLayer))
        {
            var jacket = hit.transform.gameObject.GetComponentInChildren<Jacket>();

            Debug.Log(jacket);

            if (GameManager.gameManager.missionState == GameManager.State.LOOSENINGJACKET && jacket != null)
            {
                jacket.gameObject.SetActive(false);
            }
        }
    }
}