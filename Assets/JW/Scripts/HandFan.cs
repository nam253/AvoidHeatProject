using Unity.VisualScripting;
using UnityEngine;

public class HandFan : MonoBehaviour, IItem
{
    public float itemTemperature = -1; //감소되는 온도 

    public void Use(GameObject target) {
        // 전달받은 게임 오브젝트로부터 LivingEntity 컴포넌트 가져오기 시도
        LivingEntity life = target.GetComponent<LivingEntity>();
        
        // LivingEntity컴포넌트가 있다면
        if (life != null)
        {

            GameManager.gameManager.missionState = GameManager.State.AWAKEN;
            Debug.Log(GameManager.gameManager.missionState);
            
            // 체력 회복 실행
            life.RestoreHealth(itemTemperature);
            Debug.Log("온도 감소");
            Destroy(gameObject);
            
            // script_nyj 참고
            Step.stepInstance.CompleteCurrentStep();
        }
    }
}
