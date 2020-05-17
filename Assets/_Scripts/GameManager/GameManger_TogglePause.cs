using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManger_TogglePause : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;

    public GameObject instructions, credits;

    private void OnEnable()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        instructions.SetActive(false);
        credits.SetActive(false);
    }

    private void Update()
    {
        if(!gameManagerMaster.isGameOver)
        {
            if (Input.GetKeyUp(KeyCode.Escape))
            {
                if (instructions.activeSelf || credits.activeSelf)
                {
                    instructions.SetActive(false);
                    credits.SetActive(false);
                }
                else
                {
                    TogglePause();   
                }
            }
        }
    }

    private void TogglePause()
    {
        if(!gameManagerMaster.isGameOver)
        {
            if (gameManagerMaster.isGamePaused) Time.timeScale = 1;
            else Time.timeScale = 0;

            gameManagerMaster.CallTogglePauseEvent();
        }
    }
    
    public void Resume()
    {
        TogglePause();
    }

    public void Instructions()
    {
        instructions.SetActive(true);
    }

    public void MainMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    
    public void Credits()
    {
        credits.SetActive(true);
    }
}
