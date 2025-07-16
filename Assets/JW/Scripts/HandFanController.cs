using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CapsuleCollider), typeof(AudioSource))]
public class HandFanController : MonoBehaviour
{
    [Tooltip("쿨링 범위용 Trigger Collider")]
    public CapsuleCollider coolingZone;

    [Tooltip("쿨링 지속 시간(초)")]
    public float activeDuration = 3f;

    [Tooltip("초당 감소 온도량")]
    public float coolingRatePerSecond = 0.3f;

    [Header("On/Off Sound")]
    [Tooltip("켜거나 끌 때 재생할 클립")]
    public AudioClip windSound;
    public AudioClip offSound;
    private AudioSource    audioSource;
    private bool           isActive = false;
    private Coroutine      coolingCoroutine;

    void Reset()
    {
        // Collider 자동 연결
        coolingZone = GetComponent<CapsuleCollider>();
        coolingZone.isTrigger = true;
        coolingZone.enabled   = false;

        // AudioSource 자동 연결
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
    }

    void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();

    }

    /// <summary>
    /// 버튼 또는 입력으로 호출하세요.
    /// 다시 누르면 꺼집니다.
    /// </summary>
    public void Activate()
    {
        if (isActive)
        {
            // 이미 켜진 상태라면 즉시 끄기
            StopCoolingManual();
        }
        else
        {
            // 꺼진 상태라면 켜기
            StartCooling();
        }
    }

    private void StartCooling()
    {
        PlaySound(windSound);
        PlaySound(offSound);
        isActive = true;
        coolingZone.enabled = true;
        // 자동 종료 코루틴 시작
        coolingCoroutine = StartCoroutine(CoolingRoutine());
    }

    private void StopCoolingManual()
    {
        
        // 실행 중인 코루틴이 있으면 중단
        if (coolingCoroutine != null)
        {
            StopCoroutine(coolingCoroutine);
            coolingCoroutine = null;
            audioSource.Stop();
        }
        PlaySound(offSound);
        // 즉시 비활성화
        coolingZone.enabled = false;
        isActive = false;

        //PlaySound(windSound);
    }

    private IEnumerator CoolingRoutine()
    {
        float timer = 0f;
        while (timer < activeDuration)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        // 자동으로 꺼질 때도 효과음
        EndCoolingAutomatic();
    }

    private void EndCoolingAutomatic()
    {
        coolingZone.enabled = false;
        isActive = false;
        coolingCoroutine = null;
    }

    void OnTriggerStay(Collider other)
    {
        if (!isActive) return;

        if (other.TryGetComponent<HumanTemperature>(out var human) && !human.dead)
        {
            if (GameManager.gameManager.missionState == GameManager.State.FANNING)
            {
                // script_nyj 참고
                Step.stepInstance.CompleteCurrentStep();
                GameManager.gameManager.missionState = GameManager.State.ICEBAG;
            }
            
            Debug.Log(GameManager.gameManager.missionState);
            human.RestoreHealth(-coolingRatePerSecond * Time.deltaTime);
            Debug.Log(human.humanTemperature);

        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null) return;
        audioSource.PlayOneShot(clip);
    }
}
