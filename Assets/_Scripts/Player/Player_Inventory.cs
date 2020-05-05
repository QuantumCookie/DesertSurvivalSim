using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public InventoryObject inventory;
    
    private void OnApplicationQuit()
    {
        inventory.items.Clear();
    }
}
