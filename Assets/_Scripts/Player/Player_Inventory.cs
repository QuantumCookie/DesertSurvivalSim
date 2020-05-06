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

        for (int i = 0; i < inventory.maxSlots; i++)
        {
            inventory.items[i] = new InventorySlot();
        }
    }

    public void AddItem(BaseObject _item, int _amt)
    {
        //Try to add the item to an already existing, unfilled stack
        for (int i = 0; i < inventory.maxSlots; i++)
        {
            if (inventory.items[i].item == _item)
            {
                _amt = inventory.items[i].AddAmount(_amt);
                if (_amt == 0) return;
            }
        }
        
        //If all stacks filled, assign a new empty slot
        for (int i = 0; i < inventory.maxSlots; i++)
        {
            if (inventory.items[i].amount == -1)
            {
                inventory.items[i].item = _item;
                inventory.items[i].amount = 0;
                _amt = inventory.items[i].AddAmount(_amt);
                if (_amt == 0) return;
            }
        }
        
        //No space in inventory
    }
    
    private void OnApplicationQuit()
    {
        //inventory.items.Clear();
    }
}
