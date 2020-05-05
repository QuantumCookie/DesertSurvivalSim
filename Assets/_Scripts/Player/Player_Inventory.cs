using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public InventoryObject inventory;
    
    private void OnTriggerEnter(Collider other)
    {
        Item_Master pickup = other.GetComponent<Item_Master>();
        
        if (pickup)
        {
            inventory.AddItem(pickup.item, 33);
            Destroy(other.gameObject);
        }
    }
    
    private void OnApplicationQuit()
    {
        inventory.items.Clear();
    }
}
