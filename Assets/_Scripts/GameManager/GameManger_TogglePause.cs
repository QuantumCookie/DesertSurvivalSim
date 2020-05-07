using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManger_TogglePause : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;

    private void OnEnable()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
    }

    private void Update()
    {
        if(!gameManagerMaster.isGameOver)
        {
            if (Input.GetKeyUp(KeyCode.Escape)) TogglePause();
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
        //Show instructions panel
    }

    public void Settings()
    {
        //Show settings
    }

    public void MainMenu()
    {
        //Return to main menu
    }

    public void Quit()
    {
        //Quit
    }
    
    public void Credits()
    {
        //Show credits
    }
}
