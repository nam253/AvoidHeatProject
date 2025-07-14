using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMove : MonoBehaviour
{
    public GameObject startButten;
    public GameObject modeButten;

    bool isMode;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isMode = false;
        startButten.SetActive(true);
        modeButten.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Mode()
    {
        isMode = true;
        startButten.SetActive(false);
        modeButten.SetActive(true);
    }

    public void ChangeScene(int i)
    {
        SceneManager.LoadScene(i);
    }
    
    
}
