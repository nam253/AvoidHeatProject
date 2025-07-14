using UnityEngine;
using UnityEngine.UI;
using System.Collections;

[RequireComponent(typeof(Canvas))]
public class ScreenFlash : MonoBehaviour
{
    [Header("Flash Settings")]
    public Image flashImage;
    [Range(0,1)] public float maxAlpha = 0.5f;

    [Header("Temperature Range")]
    public float minTemp = 36.5f;
    public float maxTemp = 39.5f;

    [Header("Blink Timing (초)")]
    public float minPeriod = 0.5f;
    public float maxPeriod = 3.0f;

    public HumanTemperature playerTemperature;

    private bool isBlinking = false;

    void Reset()
    {
        flashImage = GetComponentInChildren<Image>();
        if (flashImage)
        {
            flashImage.color = new Color(1,0,0,0);
            flashImage.raycastTarget = false;
        }
    }

    void Update()
    {
        if (!isBlinking 
            && playerTemperature != null 
            && playerTemperature.humanTemperature > minTemp)
        {
            StartCoroutine(BlinkRoutine());
        }
    }

    private IEnumerator BlinkRoutine()
    {
        isBlinking = true;

        while (playerTemperature.humanTemperature > minTemp)
        {
            float temp = playerTemperature.humanTemperature;
            float norm = Mathf.Clamp01((temp - minTemp) / (maxTemp - minTemp));

            float alphaTarget = norm * maxAlpha;
            float period = Mathf.Lerp(maxPeriod, minPeriod, norm);

            float half = period * 0.5f;
            // Fade-in
            for (float t = 0f; t < half; t += Time.deltaTime)
            {
                float a = Mathf.Lerp(0f, alphaTarget, t / half);
                flashImage.color = new Color(1,0,0,a);
                yield return null;
            }
            // Ensure fully on
            flashImage.color = new Color(1,0,0, alphaTarget);

            // Fade-out
            for (float t = 0f; t < half; t += Time.deltaTime)
            {
                float a = Mathf.Lerp(alphaTarget, 0f, t / half);
                flashImage.color = new Color(1,0,0,a);
                yield return null;
            }
            // Ensure fully off
            flashImage.color = new Color(1,0,0, 0f);
        }

        // 끝나면 완전 투명
        flashImage.color = new Color(1,0,0,0);
        isBlinking = false;
    }
}
