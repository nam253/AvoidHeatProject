using TMPro;
using UnityEngine;

public class Outers : MonoBehaviour, IItem
{
    public void Use(GameObject target)
    {
        LivingEntity livingEntity = target.GetComponent<LivingEntity>();
        if (GameManager.gameManager.missionState == GameManager.State.LOOSENINGJACKET && livingEntity != null)
        {
            livingEntity.RestoreHealth(-1);
            GameManager.gameManager.missionState = GameManager.State.WATERING;
            gameObject.SetActive(false);
        }
    }
}
