using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Resource", menuName = "Inventory/Items/New Resource Item")]
public class ResourceObject : BaseObject
{
    private void Awake()
    {
        itemType = ItemType.Resource;
    }
}
