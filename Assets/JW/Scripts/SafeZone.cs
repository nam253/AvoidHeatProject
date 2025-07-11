using UnityEngine;

public class SafeZone : MonoBehaviour
{
    public float coolingRatePerSecond = 1f;

    private void OnTriggerEnter(Collider other)
    {
        LivingEntity life = other.GetComponent<LivingEntity>();
        
        // LivingEntity컴포넌트가 있다면
        if (life != null)
        {
            // 체력 회복 실행
            life.RestoreHealth(coolingRatePerSecond);
            Debug.Log("온도 증가");
        }
    }
}
