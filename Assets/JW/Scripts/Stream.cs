using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stream : MonoBehaviour
{
   private LineRenderer lineRenderer = null;
   private ParticleSystem splashParticle = null;
   
   private Coroutine pourRoutine = null;
   
   
   private Vector3 targetPosition = Vector3.zero;
   public float amount = 1;

   public bool isAlcohol = false;
   // 플레이어 레이어 번호 캐시
   public LayerMask targetLayers;

   private void Awake()
   {
      lineRenderer     = GetComponent<LineRenderer>();
      splashParticle   = GetComponentInChildren<ParticleSystem>();
      
   }

   private void Start()
   {
      MoveToPosition(0,transform.position);
      MoveToPosition(1,transform.position);
   }

   public void Begin()
   {
      StartCoroutine(UpdateParticle());
      pourRoutine = StartCoroutine(BeginPour());
   }

   private IEnumerator BeginPour()
   {
      while (gameObject.activeSelf)
      {
         targetPosition = FindEndPoint();
         MoveToPosition(0,transform.position);
         AnimateToPosition(1,targetPosition);
         
         yield return null;
      }
   }
   private void OnTriggerEnter(Collider other)
   {
      // 1) 해당 오브젝트 레이어가 LayerMask에 포함되어 있는지 확인
      if ((targetLayers.value & (1 << other.gameObject.layer)) == 0) 
         return;

      // 2) HumanTemperature 컴포넌트 확인
      if (other.TryGetComponent<HumanTemperature>(out var human))
      {
         human.RestoreHealth(amount);
         if (!isAlcohol)
         {
            if (GameManager.gameManager.missionState == GameManager.State.WATERING)
            {
               GameManager.gameManager.missionState = GameManager.State.FANNING;
            }
            Debug.Log(GameManager.gameManager.missionState);   

            // script_nyj 참고
            Step.stepInstance.CompleteCurrentStep();
         }
         Debug.Log($"Other Layer: {other.gameObject.layer}, Mask Value: {targetLayers.value}");
         Debug.Log($"Stream hit player, new temp: {human.humanTemperature}");
      }
   }
   public void End()
   {
      StopCoroutine(pourRoutine);
      pourRoutine = StartCoroutine(EndPour());
   }

   private IEnumerator EndPour()
   {
      while (!HasReachedPosition(0, targetPosition))
      {
         AnimateToPosition(0,targetPosition);
         AnimateToPosition(1,targetPosition);
         
         yield return null;
      }
      Destroy(gameObject);
   }
   private Vector3 FindEndPoint()
   {
      RaycastHit hit;
      Ray ray = new Ray(transform.position, Vector3.down);
      
      Physics.Raycast(ray, out hit,2.0f);
      Vector3 endPoint = hit.collider ? hit.point : ray.GetPoint(2.0f);
      
      return endPoint;
   }

   private void MoveToPosition(int index, Vector3 targetPosition)
   {
      lineRenderer.SetPosition(index, targetPosition);
      
   }

   private void AnimateToPosition(int index, Vector3 targetPosition)
   {
      Vector3 currentPoint = lineRenderer.GetPosition(index);
      Vector3 newPosition = Vector3.MoveTowards(currentPoint, targetPosition, Time.deltaTime * 1.75f);
      lineRenderer.SetPosition(index, newPosition);
   }

   private bool HasReachedPosition(int index, Vector3 targetPosition)
   {
      Vector3 currentPosition = lineRenderer.GetPosition(index);
      return currentPosition == targetPosition;
   }

   private IEnumerator UpdateParticle()
   {
      while (gameObject.activeSelf)
      {
         splashParticle.gameObject.transform.position = targetPosition;
         
         bool isHitting = HasReachedPosition(1, targetPosition);
         splashParticle.gameObject.SetActive(isHitting);
      
         yield return null;
      }
   }
   
}
