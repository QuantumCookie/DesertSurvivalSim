using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Inventory", menuName = "Inventory/New Inventory")]
public class InventoryObject : ScriptableObject
{
    public List<InventorySlot> items = new List<InventorySlot>();
    
    public void AddItem(BaseObject item, int amount)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].item.Equals(item))
            {
                items[i].AddAmount(amount);
                return;
            }
        }

        items.Add(new InventorySlot(item, amount));
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
