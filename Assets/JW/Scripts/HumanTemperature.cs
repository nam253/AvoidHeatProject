using UnityEngine;

public class HumanTemperature : LivingEntity
{
    public AudioClip itemPickupClip; // 아이템 습득 소리
    
    private void OnTriggerEnter(Collider other)
    {
        // 아이템과 충돌한 경우 해당 아이템을 사용하는 처리
        if (!dead)
        {
            IItem item = other.GetComponent<IItem>();
            if (item != null)
            {
                item.Use(gameObject);
                Debug.Log(humanTemperature);
            }
        }
    }
}