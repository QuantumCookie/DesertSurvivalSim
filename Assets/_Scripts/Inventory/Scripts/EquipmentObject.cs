using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Equipment", menuName = "Inventory/Items/New Equipment Item")]
public class EquipmentObject : BaseObject
{
    private void Awake()
    {
        itemType = ItemType.Equipment;
    }
}
