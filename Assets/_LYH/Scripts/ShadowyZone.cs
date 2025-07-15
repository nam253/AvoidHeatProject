using Unity.VisualScripting;
using UnityEngine;

public class ShadowyZone : MonoBehaviour
{
    public GameObject patient;
    void Start()
    {

    }

    void OnTriggerEnter(Collider collider)
    {
        if (GameManager.gameManager.missionState == GameManager.State.AVOIDINGSUN && collider.gameObject == patient)
        {
            Step.stepInstance.CompleteCurrentStep();
            GameManager.gameManager.missionState = GameManager.State.LOOSENINGJACKET;
        }
    }
}
