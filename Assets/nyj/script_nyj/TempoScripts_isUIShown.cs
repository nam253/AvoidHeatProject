using Unity.VisualScripting;
using UnityEngine;

public class TempoScripts_isUIShown : MonoBehaviour
{
    public static TempoScripts_isUIShown isUIShownInstance
    {
        get
        {
            if (isUIShownInstanceInner == null)
            {
                isUIShownInstanceInner = FindAnyObjectByType<TempoScripts_isUIShown>();
            }

            return isUIShownInstanceInner;
        }
    }
    private static TempoScripts_isUIShown isUIShownInstanceInner;

    public bool bIsUIShown { get; set; } = false;
}
