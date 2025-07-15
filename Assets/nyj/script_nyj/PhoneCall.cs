using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PhoneCall : MonoBehaviour
{
    public GameObject Phoneimg;
    public TextMeshProUGUI NumberText;

    public AudioSource audioSource;
    public AudioClip buttonSound;

    public AudioClip endCallNarration;

    private string phoneNumber = "";
    // public Step step;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
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

        if (audioSource && buttonSound)
        {
            audioSource.PlayOneShot(buttonSound);
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
