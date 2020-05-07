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
            inventory.items[i] = new InventorySlot(i);
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

    public void ClearSlot(int i)
    {
        inventory.items[i].item = null;
        inventory.items[i].amount = -1;
    }

    public void SwapSlots(int i, int j)
    {
        if (i == j) return;
        
        InventorySlot a = inventory.items[i];
        InventorySlot b = inventory.items[j];
        
        InventorySlot tmp = new InventorySlot(i);
        
        tmp.item = a.item;
        tmp.amount = a.amount;

        a.item = b.item;
        a.amount = b.amount;

        b.item = tmp.item;
        b.amount = tmp.amount;
    }

    public void UseItem(int i)
    {
        InventorySlot slot = inventory.items[i];
        
        if (slot.item.itemType == ItemType.Consumable)
        {
            //Apply stats
            inventory.items[i].Decrement();
            Debug.Log("Consumed " + slot.item.name);
        }
    }

    public void DiscardItem(int i)
    {
        ClearSlot(i);
    }
    
    private void OnApplicationQuit()
    {
        inventory.items = new InventorySlot[inventory.maxSlots];
    }
}
