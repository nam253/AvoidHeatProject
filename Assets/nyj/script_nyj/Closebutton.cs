using UnityEngine;

public class Closebutton : MonoBehaviour
{
    public GameObject closeButtonUI;
    
    public CharacterRaycaster[] allCharacterRaycasters;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void closeUi()
    {
        if (closeButtonUI != null)
        {
            closeButtonUI.SetActive(false);

            foreach (CharacterRaycaster raycaster in allCharacterRaycasters)
            {
                if (raycaster != null)
                {
                    raycaster.SetUiManuallyClosed(true);
                    raycaster.SetUiActive(false);
                    
                }
            }
        }

    }
    
}
