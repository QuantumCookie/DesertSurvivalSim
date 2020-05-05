using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MineResource : MonoBehaviour
{
    private Player_DetectItem itemDetector;
    private InventoryObject inventory;

    private void Start()
    {
        itemDetector = GetComponent<Player_DetectItem>();
        inventory = GetComponent<Player_Inventory>().inventory;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Debug.Log("Pressed E");
            if (itemDetector.type != ResourceType.Null)
            {
                ResourceSO data = itemDetector.collider.transform.root.GetComponent<Resource_Master>().data;
                
                inventory.AddItem(data.yield, data.quantity);
            }
            else if (itemDetector.itemType != ItemType.Null)
            {
                Item_Master item = itemDetector.collider.transform.root.GetComponent<Item_Master>();
                
                inventory.AddItem(item.item, item.quantity);
            }
        }
    }
}
