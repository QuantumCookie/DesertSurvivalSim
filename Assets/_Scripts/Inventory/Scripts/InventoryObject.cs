using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class InventoryObject : ScriptableObject
{
    public int maxSlots = 25;
    public InventorySlot[] items = new InventorySlot[25];
}

[System.Serializable]
public class InventorySlot
{
    public BaseObject item;
    public int amount;
    public int slot;

    public InventorySlot(int _slot)
    {
        item = null;
        amount = -1;
        slot = _slot;
    }
    
    public InventorySlot(BaseObject _item, int _amt, int _slot)
    {
        item = _item;
        amount = _amt;
        slot = _slot;
    }

    public int AddAmount(int value)
    {
        if (amount == -1) return value;

        int amountToAdd = Mathf.Min(item.stackSize - amount, value);
        amount += amountToAdd;

        return value - amountToAdd;
    }

    public void Decrement()
    {
        amount--;
        if (amount <= 0) amount = -1;
    }
}
