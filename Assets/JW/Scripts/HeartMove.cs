using UnityEngine;

public class HeartMove : MonoBehaviour
{
    [Header("Target to Pulse")]
    [Tooltip("심장 이미지의 Transform")]
    public Transform heartTransform;

    [Header("Temperature Range")]
    [Tooltip("펄스가 멈추는 기준 온도 (시작 온도)")]
    public float minTemp = 34.5f;
    [Tooltip("가장 빠른 펄스 속도를 내는 온도")]
    public float maxTemp = 39.5f;
    [Tooltip("참조할 플레이어 온도 컴포넌트")]
    public HumanTemperature playerTemp;

    [Header("Scale Settings")]
    [Tooltip("최소 스케일 (정상)")]
    public float minScale = 1.0f;
    [Tooltip("최대 스케일 (뛸 때)")]
    public float maxScale = 1.4f;

    [Header("Period Range (초)")]
    [Tooltip("느린 펄스 주기")]
    public float maxPeriod = 1.3f;
    [Tooltip("빠른 펄스 주기")]
    public float minPeriod = 0.3f;

    private float timer = 0f;

    void Update()
    {
        if (playerTemp == null || heartTransform == null)
            return;

        float temp = playerTemp.humanTemperature;

        // 체온이 기준 이하이면 기본 스케일 고정
        if (temp <= minTemp)
        {
            heartTransform.localScale = Vector3.one * minScale;
            timer = 0f;
            return;
        }

        // 온도에 따른 정규화 (0~1)
        float norm = Mathf.Clamp01((temp - minTemp) / (maxTemp - minTemp));
        // 주기를 온도에 따라 보간
        float period = Mathf.Lerp(maxPeriod, minPeriod, norm);

        timer += Time.deltaTime;
        // 0~1 사이 위상 계산 (사인 곡선)
        float phase = timer / period * Mathf.PI * 2f;
        float sin = (Mathf.Sin(phase) + 1f) / 2f; // 0~1

        // 스케일 보간
        float scale = Mathf.Lerp(minScale, maxScale, sin);
        heartTransform.localScale = Vector3.one * scale;
    }
}
