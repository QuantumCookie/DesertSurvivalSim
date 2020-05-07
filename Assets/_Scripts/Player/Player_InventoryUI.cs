using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InventoryUI : MonoBehaviour
{
    public GameObject inventoryUI;
    private GameManager_Master gameManagerMaster;

    private void Start()
    {
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        inventoryUI.SetActive(false);
    }

    private void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;

        if (Input.GetKeyDown(KeyCode.I))
        {
            inventoryUI.SetActive(!inventoryUI.activeSelf);
        }
    }
}
