using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCall : MonoBehaviour
{
    public GameObject Phoneimg;
    public TextMeshProUGUI NumberText;

    public AudioSource audioSource;
    public AudioClip buttonSound1;
    public AudioClip buttonSound9;

    public AudioClip endCallNarration;

    private string phoneNumber = "";
    
    void Start()
    {
        if (Phoneimg != null)
        {
            Phoneimg.SetActive(false);
        }
        
    }
    public void OpenPhone()
    {
        Phoneimg.SetActive(true);
    }

    public void OnNumberPressed(string digit)
    {
        phoneNumber += digit;
        NumberText.text = phoneNumber;

        if (audioSource)
        {
            switch (digit)
            {
                case "1":
                    if (buttonSound1 != null)
                        audioSource.PlayOneShot(buttonSound1);
                    break;
                case "9":
                    if (buttonSound9 != null)
                        audioSource.PlayOneShot(buttonSound9);
                    break;

            }
                
        }
    }
    public void ClosePhone()
    {
        GameManager.gameManager.missionState = GameManager.State.AVOIDINGSUN;
        Step.stepInstance.CompleteCurrentStep();
        Debug.Log(GameManager.gameManager.missionState);
        // step.CompleteCurrentStep();

        if (audioSource && endCallNarration)
        {
            audioSource.PlayOneShot(endCallNarration);
        }
        Phoneimg.SetActive(false);
    }
}
