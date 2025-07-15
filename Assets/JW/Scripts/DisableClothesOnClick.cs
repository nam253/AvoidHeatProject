using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class DisableClothesOnClick : MonoBehaviour
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
            // 교체할 대상의 루트
            var oldRoot = hit.transform.root.gameObject;

            // 새 Prefab 생성(같은 위치·회전·부모로)
            var newObj = Instantiate(
                replacementPrefab,
                oldRoot.transform.position,
                oldRoot.transform.rotation,
                oldRoot.transform.parent
            );

            // (선택) 기존 Transform 세팅 복사
            newObj.transform.localScale = oldRoot.transform.localScale;

            // 원본 삭제
            Destroy(oldRoot);
        }
    }
}