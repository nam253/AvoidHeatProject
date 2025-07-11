using UnityEngine;

public class Closebutton : MonoBehaviour
{
    public GameObject closeButtonUI;
    public CharacterRaycaster leftCharacterRaycaster;
    public CharacterRaycaster rightCharacterRaycaster;
    
    public CharacterRaycaster[] allCharacterRaycasters;

    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void closeUi()
    {
        if (closeButtonUI != null)
        {
            closeButtonUI.SetActive(false);

            foreach (CharacterRaycaster raycaster in allCharacterRaycasters)
            {
                if (leftCharacterRaycaster != null)
                {
                    leftCharacterRaycaster.SetUiManuallyClosed(true);
                    leftCharacterRaycaster.SetUiActive(false);

                }
                if (rightCharacterRaycaster != null)
                {
                rightCharacterRaycaster.SetUiManuallyClosed(true); 
                rightCharacterRaycaster.SetUiActive(false); 
                }
            }
        }

    }
    
}
