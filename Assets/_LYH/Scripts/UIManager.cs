using System;
using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    // Coroutine Delayer
    private WaitForSeconds delayer = new WaitForSeconds(0.8f);

    // UIManager Static Instance
    private static UIManager uiManagerInner;
    public static UIManager uiManager
    {
        get
        {
            if (uiManagerInner == null)
            {
                uiManagerInner = FindFirstObjectByType<UIManager>();
            }
            return uiManagerInner;
        }
    }

    // UI Panels
    private MissionPanel missionObjectivePanel;
    private TemperaturePanel temperatureMeterPanel;

    // JSON for Descriptions
    [Serializable]
    public class Desc
    {
        public int id;
        public string state;
        public string description;
    }
    public TextAsset mOTexts { get; private set; } // Save Json via "Resources.Load<TextAsset>(Your filepath)"
    public GameManager.State currentState;
    public GameManager.State previousState;

    void Awake()
    {
        if (uiManager != this) { Destroy(gameObject); }

        missionObjectivePanel = GetComponentInChildren<MissionPanel>();
        temperatureMeterPanel = GetComponentInChildren<TemperaturePanel>();

        mOTexts = Resources.Load<TextAsset>("Texts/MissionObjectives1");

        // DontDestroyOnLoad(gameObject);

        // <Basic Setting for JSON Read>
        // mOTexts = Resources.Load<TextAsset>("Texts/MissionObjectives");
        // Desc fromJson = JsonUtility.FromJson<Desc>(mOTexts.ToString());
        // Debug.Log(fromJson.description);
        // </Basic Setting for JSON Read>
    }

    void Start()
    {
        
    }

    void Update()
    {
        UpdateUIs();
    }

    public IEnumerator StateCheck()
    {
        currentState = GameManager.gameManager.missionState;

        while (true)
        {
            if (currentState != previousState)
            {
                
            }

            yield return delayer;
        }

        previousState = currentState;
    }

    void UpdateUIs()
    {
        mOTexts = Resources.Load<TextAsset>("Texts/MissionObjectives1");

        Desc mO = JsonUtility.FromJson<Desc>(mOTexts.ToString());
        Text txt = missionObjectivePanel.gameObject.GetComponentInChildren<Text>();

        txt.text += mO.description;
        txt.text += "\n\n";
    }
}
