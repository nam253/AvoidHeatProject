using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class TemporaryCollider : MonoBehaviour
{
    private InputDevice inputDevice;

    void Start()
    {
        List<InputDevice> inputDevices = new List<InputDevice>();
        InputDeviceCharacteristics characteristics = InputDeviceCharacteristics.HeldInHand | InputDeviceCharacteristics.Controller;
        InputDevices.GetDevicesWithCharacteristics(characteristics, inputDevices);

        if (inputDevices.Count > 0)
        {
            inputDevice = inputDevices[0];
        }
    }

    void Update()
    {
        CommonInput();
    }

    public void CommonInput()
    {
        inputDevice.TryGetFeatureValue(CommonUsages.trigger, out float triggered);
        if (triggered > 0f)
        {
            Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);

            for (int i = 0; i < colliders.Length; i++)
            {
                Jacket outers = colliders[i].GetComponent<Jacket>();
                if (outers != null)
                {
                    Debug.Log("Triggered");
                    outers.Use(GameObject.FindGameObjectWithTag("Character"));
                    break;
                }
            }
        }
    }

}
