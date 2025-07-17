using UnityEngine;

// 심장 아이콘 교체 필요
// 현재 기온 표시 페이드아웃
// 인트로 교체 필요
// 랙돌 개선
// UI 통일성 필요
// 텍스트 정렬, Kitsch한 폰트 등의 디테일 개선
// 임무목표: 전체 스탭을 띄우고 현재 스텝만 강조하는 식의 표현 방식도 고려

// 왜 물붓기에서 스탭이 단번에 건너뛰어진거지..?
// :: Stream.cs 에서 CompleteCurrentStep() 를 부를 때 조건 검수를 하지 않는 문제가 있음

public class GameManager : MonoBehaviour
{
    // Static Instance
    public static GameManager gameManager
    {
        get
        {
            if (gameManagerInner == null)
            {
                gameManagerInner = FindFirstObjectByType<GameManager>();
            }
            return gameManagerInner;
        }
    }
    private static GameManager gameManagerInner;

    // Mission Objectives
    public enum State
    {
        CALLING,            // Call 119
        AVOIDINGSUN,        // Move him to any shadowy place
        LOOSENINGJACKET,    // Loosen his jacket
        WATERING,           // Pour water to him
        FANNING,            // Fan with something wide
        ICEBAG,             // Touch him with icebag to decrease temperature
        AWAKEN,             // He's now awake
    }
    [SerializeField] private State missionStateInner = State.CALLING; // Initial state
    public State missionState
    {
        get { return missionStateInner; }
        set { missionStateInner = value; }
    }

    void Awake()
    {
        if (gameManager != this)
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (missionState == State.AWAKEN && GameObject.Find("Patient") != null)
        {
            TryGetComponent<PrefabShifting>(out PrefabShifting shifting);

            shifting.ShiftPrefab();
        }
    }
}
