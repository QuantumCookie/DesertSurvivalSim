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

    public InventorySlot()
    {
        item = null;
        amount = -1;
    }
    
    public InventorySlot(BaseObject _item, int _amt)
    {
        item = _item;
        amount = _amt;
    }

    public int AddAmount(int value)
    {
        if (amount == -1) return value;

        int amountToAdd = Mathf.Min(item.stackSize - amount, value);
        amount += amountToAdd;

        return value - amountToAdd;
    }
}
