using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.Interaction.Toolkit;

public class PrefabShifting : MonoBehaviour
{
    [Tooltip("교체할 캐릭터 Prefab")]
    public GameObject oldPrefab;
    public GameObject replacementPrefab;

    public void ShiftPrefab()
    {
        // 교체할 대상의 루트

        // 새 Prefab 생성(같은 위치·회전·부모로)
        var newObj = Instantiate(
            replacementPrefab,
            oldPrefab.transform.position,
            oldPrefab.transform.rotation,
            oldPrefab.transform.parent
        );

        // (선택) 기존 Transform 세팅 복사
        newObj.transform.localScale = oldPrefab.transform.localScale;
            
        // 원본 삭제
        Destroy(oldPrefab);
    }
}