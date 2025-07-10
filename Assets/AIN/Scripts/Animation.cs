using System;
using UnityEngine;

public class Animation : MonoBehaviour
{
    private Animator animator;

    private bool bIsAwake = false;
    public float temperature;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        temperature = GetComponent<LivingEntity>().humanTemperature;

        if (temperature <= 36.5f && bIsAwake != true)
        {
            bIsAwake = true;
            animator.SetTrigger("standing");
        }
    }
}   
