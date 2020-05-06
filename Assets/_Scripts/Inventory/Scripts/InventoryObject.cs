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

    public InventorySlot(BaseObject _item, int _amt)
    {
        item = _item;
        amount = _amt;
    }

    public void AddAmount(int value)
    {
        amount += value;
    }
}
