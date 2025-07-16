using UnityEngine;

public class LookAt : MonoBehaviour
{
    public Vector3 posOffset = Vector3.zero;
    public Transform camPos;

    void Start()
    {
        camPos = GameObject.Find("Main Camera").GetComponent<Transform>();
    }

    void FixedUpdate()
    {
        transform.position = camPos.position + new Vector3(0, 0, 1f);
        transform.LookAt(camPos);
    }
}
