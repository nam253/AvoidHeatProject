using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;

[RequireComponent(typeof(XRGrabInteractable))]
[RequireComponent(typeof(AudioSource))]
public class GrabSound : MonoBehaviour
{
    [Tooltip("잡힐 때 재생할 소리")]
    public AudioClip grabClip;

    AudioSource _audio;
    XRGrabInteractable _grabInteractable;

    void Awake()
    {
        // AudioSource 세팅
        _audio = GetComponent<AudioSource>();
        _audio.playOnAwake = false;
        
        // XRGrabInteractable 세팅
        _grabInteractable = GetComponent<XRGrabInteractable>();
        // 잡힐 때(Select Entered) 이벤트 구독
        _grabInteractable.selectEntered.AddListener(OnGrab);
    }

    void OnDestroy()
    {
        // 씬 전환 등으로 파괴될 때 구독 해제
        _grabInteractable.selectEntered.RemoveListener(OnGrab);
    }

    // 실제 소리 재생 콜백
    private void OnGrab(SelectEnterEventArgs args)
    {
        if (grabClip != null)
            _audio.PlayOneShot(grabClip);
    }
}