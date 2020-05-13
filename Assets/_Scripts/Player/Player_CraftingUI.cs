using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CraftingUI : MonoBehaviour
{
    public GameObject craftingMenuUI;
    private GameManager_Master gameManagerMaster;
    
    private void Start()
    {
        Initialize();
        gameManagerMaster.OnGameOver += DisableMenu;
    }

    private void Initialize()
    {
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        DisableMenu();
    }

    private void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;

        if (Input.GetKeyDown(KeyCode.C))
        {
            ToggleCraftingMenu();
        }
    }

    private void ToggleCraftingMenu()
    {
        craftingMenuUI.SetActive(!craftingMenuUI.activeSelf);
    }

    private void DisableMenu()
    {
        craftingMenuUI.SetActive(false);
    }

    private void OnDisable()
    {
        gameManagerMaster.OnGameOver -= DisableMenu;
    }
}
