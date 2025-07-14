using UnityEngine;

public class Outers : MonoBehaviour, IItem
{
    public void Use(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    void OnDisable()
    {
        GameManager.gameManager.missionState = GameManager.State.WATERING;
    }
}
