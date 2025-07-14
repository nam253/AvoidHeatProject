using UnityEngine;

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
    private State missionStateInner = State.CALLING; // Initial state
    public State missionState
    {
        get { return missionStateInner; }
        set { missionStateInner = value; }
    }

    void Awake()
    {
        if (gameManager != null)
        {
            Destroy(gameObject);
        }
    }
}
