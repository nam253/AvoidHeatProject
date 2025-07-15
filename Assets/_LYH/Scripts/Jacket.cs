using TMPro;
using UnityEngine;

public class Jacket : MonoBehaviour, IItem
{
    public LivingEntity livingEntity;

    public void Use(GameObject target)
    {

    }

    void OnDisable()
    {
        if (GameManager.gameManager.missionState == GameManager.State.LOOSENINGJACKET && livingEntity != null)
        {
            livingEntity.RestoreHealth(-1);
            Step.stepInstance.CompleteCurrentStep();
            GameManager.gameManager.missionState = GameManager.State.WATERING;
        }
    }
}
