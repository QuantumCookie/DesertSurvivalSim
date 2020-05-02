using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryObject : ScriptableObject
{
    private List<InventorySlot> items = new List<InventorySlot>();

    public void AddItem(BaseObject item, int amount)
    {
        bool hasItem = false;
        
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].Equals(item))
            {
                hasItem = true;
                items[i].AddAmount(amount);
            }
        }

        if (!hasItem)
        {
            items.Add(new InventorySlot(item, amount));
        }
    }
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
