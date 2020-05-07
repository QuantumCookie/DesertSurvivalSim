using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_PauseMenuUI : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;

    public GameObject pauseMenu;

    private void OnEnable()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        pauseMenu.SetActive(false);

        gameManagerMaster.OnTogglePause += UpdatePauseMenuUI;
    }

    private void UpdatePauseMenuUI()
    {
        if (gameManagerMaster.isGamePaused) pauseMenu.SetActive(true);
        else pauseMenu.SetActive(false);
    }
    
    private void OnDisable()
    {
        gameManagerMaster.OnTogglePause -= UpdatePauseMenuUI;
    }
}
