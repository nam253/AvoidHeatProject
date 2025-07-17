using TMPro;
using UnityEngine;
using UnityEngine.Lumin;

public class Step : MonoBehaviour
{
    public static Step stepInstance;
    public GameObject[] stepPanels;
    public int currentStep = 0;
    public LivingEntity livingentity;
    public GameObject CallingButton;
    public TextMeshProUGUI TemperatureText;
    public AudioSource audioSource;
    public AudioClip buttonClickSound;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (stepInstance == null)
        {
            stepInstance = this;
        }

        for (int i = 0; i < stepPanels.Length; i++)
        {
            stepPanels[i].SetActive(false);
        }

        stepPanels[0].SetActive(true);

        UpdateTemperatrueUI();
    }

    public void CompleteCurrentStep()
    {
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
        stepPanels[currentStep].SetActive(false);

        currentStep++;
        Debug.Log(currentStep);

        if (currentStep < stepPanels.Length)
        {
            stepPanels[currentStep].SetActive(true);
        }

        if (currentStep == 1 && CallingButton != null)
        {
            CallingButton.SetActive(false);
        }

        /*if (currentStep > 1)
            {
                livingentity.startingTemperature -= temperatureDropPerStep;
            }*/
            UpdateTemperatrueUI();
    }

    public void UpdateTemperatrueUI()
    {
        TemperatureText.text = "환자 체온: " + livingentity.humanTemperature.ToString("F1") + "°C";
    }

    
}

