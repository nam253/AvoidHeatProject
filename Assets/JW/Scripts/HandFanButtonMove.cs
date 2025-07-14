using UnityEngine;

public class HandFanButtonMove : MonoBehaviour
{
    [Tooltip("이동할 로컬 X 거리")]
    public float moveDistance = 0.0222f;

    private Vector3 originalLocalPos;
    private Rigidbody rb;
    private bool isMoved = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true;  
        // 로컬 기준 원위치 저장
        originalLocalPos = transform.localPosition;
    }

    /// <summary>
    /// 버튼 누를 때마다 로컬 위치를 토글
    /// </summary>
    public void TogglePosition()
    {
        // 목표 로컬 위치 계산
        Vector3 targetLocal = isMoved
            ? originalLocalPos
            : originalLocalPos + Vector3.right * moveDistance;

        // Rigidbody로 바로 이동 (월드 좌표로 변환)
        Vector3 targetWorld = transform.parent != null
            ? transform.parent.TransformPoint(targetLocal)
            : targetLocal;
        rb.MovePosition(targetWorld);

        isMoved = !isMoved;
    }
}