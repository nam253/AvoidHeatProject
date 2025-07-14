using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Transform camPos;

    void Start()
    {
        camPos = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        transform.LookAt(camPos);
    }
}
