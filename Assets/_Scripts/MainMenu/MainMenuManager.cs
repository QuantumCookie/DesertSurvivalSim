using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
public Canvas Button_Holder;

    public GameObject instructions, credits;
    
    public void StartGame()
    {
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            instructions.SetActive(false);
            credits.SetActive(false);
        }
    }

    public void Instructions()
    {
        instructions.SetActive(true);
    }

    public void Credits()
    {
        credits.SetActive(true);
    }
}
