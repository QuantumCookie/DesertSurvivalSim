using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MineResource : MonoBehaviour
{
    private Player_DetectItem itemDetector;
    private Player_Inventory inventory;
    private GameManager_Master gameManagerMaster;
    private Player_EquipmentManager equipment;
    private Player_Animation playerAnimation;
    
    private void Start()
    {
        itemDetector = GetComponent<Player_DetectItem>();
        inventory = GetComponent<Player_Inventory>();
        equipment = GetComponent<Player_EquipmentManager>();
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        playerAnimation = GetComponent<Player_Animation>();
    }

    private void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("Pressed E");
            if (equipment.CanMine(itemDetector.resourceType))
            {
                playerAnimation.SetSwinging(true);
            }
            else if (itemDetector.itemType != ItemType.Null)
            {
                Item_Master item = itemDetector.collider.transform.root.GetComponent<Item_Master>();

                if(item.canPickup)
                {
                    inventory.AddItem(item.item, item.quantity);
                    item.CallOnPickup();
                }
            }
        }
    }

    public void ResetSwing()
    {
        playerAnimation.SetSwinging(false);
        
        Resource_Master resource = itemDetector.collider.transform.root.GetComponent<Resource_Master>();
        
        if(resource.ApplyDamage(equipment.damage))
        {
            inventory.AddItem(resource.data.yield, resource.data.quantity);
            resource.CallMineComplete();
        }
    }
}
