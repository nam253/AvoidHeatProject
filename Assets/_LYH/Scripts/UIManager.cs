using System;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
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

    void Awake()
    {
        if (uiManager != this) { Destroy(gameObject); }

        missionObjectivePanel = GetComponentInChildren<MissionPanel>();
        temperatureMeterPanel = GetComponentInChildren<TemperaturePanel>();

        mOTexts = Resources.Load<TextAsset>("Texts/MissionObjectives");

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

    void UpdateUIs()
    {
        Desc mO = JsonUtility.FromJson<Desc>(mOTexts.ToString());
        Text txt = missionObjectivePanel.gameObject.GetComponentInChildren<Text>();
        
        txt.text += mO.description;
    }
}
