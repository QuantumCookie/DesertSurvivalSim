using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CraftingUI : MonoBehaviour
{
    public GameObject craftingMenuUI;
    private GameManager_Master gameManagerMaster;
    [HideInInspector]
    public GameObject Audio_manager;
    [HideInInspector]
    public Audio_manager audio_manager;

    private void Start()
    {
        Initialize();
        gameManagerMaster.OnGameOver += DisableMenu;
        Audio_manager = GameObject.FindWithTag("Audio");
        audio_manager = Audio_manager.transform.GetComponent<Audio_manager>();
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
            audio_manager.Source.clip = audio_manager.Crafting;
            audio_manager.Source.Play();
          
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
