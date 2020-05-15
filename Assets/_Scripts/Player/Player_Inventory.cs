using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Inventory : MonoBehaviour
{
    public InventoryObject inventory;

    private Player_UseItem useHandler;
    private Player_EquipmentManager equipmentManager;

    private void Start()
    {
        Initialize();
        CreateInventory();
    }

    private void Initialize()
    {
        useHandler = GetComponent<Player_UseItem>();
        equipmentManager = GetComponent<Player_EquipmentManager>();
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

    public void RemoveItem(BaseObject _item, int _amt)
    {
        //At this point it is guaranteed that the inventory contains at least <_amt> number of items
        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].item == _item)
            {
                if (inventory.items[i].amount > _amt)
                {
                    inventory.items[i].amount -= _amt;
                }
                else
                {
                    _amt -= inventory.items[i].amount;
                    ClearSlot(i);
                }

                if (_amt <= 0) return;
            }
        }
    }

    public int GetTotalItemCount(BaseObject _item)
    {
        int count = 0;

        for (int i = 0; i < inventory.items.Length; i++)
        {
            if (inventory.items[i].amount != -1 && inventory.items[i].item == _item) count += inventory.items[i].amount;
        }

        return count;
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
            useHandler.UseItem(slot.item);
            inventory.items[i].Decrement();
            Debug.Log("Consumed " + slot.item.name);
        }
        else if (slot.item.itemType == ItemType.Equipment)
        {
            EquipmentObject replacement = equipmentManager.Equip((EquipmentObject) slot.item);
            if (replacement == null) ClearSlot(i);
            else
            {
                inventory.items[i].item = replacement;
                inventory.items[i].amount = 1;
            }
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
