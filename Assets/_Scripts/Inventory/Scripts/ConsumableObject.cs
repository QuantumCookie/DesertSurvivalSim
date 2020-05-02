using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Consumable", menuName = "Inventory/Items/New Consumable Item")]
public class ConsumableObject : BaseObject
{
    private void Awake()
    {
        itemType = ItemType.Consumable;
    }
}
