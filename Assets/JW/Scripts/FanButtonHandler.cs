using UnityEngine;
using UnityEngine.InputSystem;

public class FanButtonHandler : MonoBehaviour
{
    [Tooltip("손풍기 컨트롤러 참조")]
    public HandFanController fanController;
    public HandFanButtonMove fanMove;
    [Tooltip("활성화 Input Action (예: XR Controller 버튼)")]
    public InputActionProperty activateAction;
    
    
    void OnEnable()
    {
        activateAction.action.Enable();
        activateAction.action.performed += OnActivate;
    }

    void OnDisable()
    {
        activateAction.action.performed -= OnActivate;
        activateAction.action.Disable();
    }

    void OnActivate(InputAction.CallbackContext ctx)
    {
        Debug.Log($"[FanButtonHandler] Activate pressed at {Time.time}");
        fanController.Activate();
        fanMove.TogglePosition();
    }
}