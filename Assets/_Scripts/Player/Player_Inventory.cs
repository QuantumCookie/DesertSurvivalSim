using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public InventoryObject inventory;

    private void Start()
    {
        CreateInventory();
    }

    private void CreateInventory()
    {
        inventory.items = new InventorySlot[inventory.maxSlots];
    }
    
    private void OnApplicationQuit()
    {
        //inventory.items.Clear();
    }
}
