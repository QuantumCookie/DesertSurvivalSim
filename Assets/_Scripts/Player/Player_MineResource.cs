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

    public GameObject balloonParent;
    public GameObject balloonPrefab;

    private bool isSwinging;
    private Collider collider;
    [HideInInspector]
    public GameObject Audio_manager;
    [HideInInspector]
    public Audio_manager audio_manager;

    private void Start()
    {
        itemDetector = GetComponent<Player_DetectItem>();
        inventory = GetComponent<Player_Inventory>();
        equipment = GetComponent<Player_EquipmentManager>();
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        playerAnimation = GetComponent<Player_Animation>();
        isSwinging = false;
        collider = null;
        Audio_manager = GameObject.FindWithTag("Audio");
        audio_manager = Audio_manager.transform.GetComponent<Audio_manager>();
    }

    private void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;

        if (isSwinging) return;

        if (Input.GetKeyDown(KeyCode.E))
        {
            //Debug.Log("Pressed E");
            if (equipment.CanMine(itemDetector.resourceType)&&(itemDetector.master.data.type == ResourceType.Tree ||itemDetector.master.data.type == ResourceType.Cactus))
            {
                isSwinging = true;
                collider = itemDetector.collider;
                playerAnimation.SetSwinging();
                audio_manager.Source.clip = audio_manager.Wood_chopping;
                audio_manager.Source.Play();
            }
            else if (equipment.CanMine(itemDetector.resourceType)&&(itemDetector.master.data.type == ResourceType.Rock ))
            {
                isSwinging = true;
                collider = itemDetector.collider;
                playerAnimation.SetSwinging();
                audio_manager.Source.clip = audio_manager.Rock_breaking;
                audio_manager.Source.Play();
            }
            else if (itemDetector.master != null && itemDetector.master.GetComponent<Animals_Attack>())
            {
                isSwinging = true;
                collider = itemDetector.collider;
                playerAnimation.SetSwinging();
                audio_manager.Source.clip = audio_manager.Wood_chopping;
                audio_manager.Source.Play();
            }
            else if (itemDetector.itemType != ItemType.Null)
            {
                Item_Master item = itemDetector.collider.transform.root.GetComponent<Item_Master>();

                if(item.canPickup)
                {
                    inventory.AddItem(item.item, item.quantity);

                    Balloon b = Instantiate(balloonPrefab, Camera.main.WorldToScreenPoint(item.transform.position), Quaternion.identity, balloonParent.transform).GetComponent<Balloon>();
                    b.SetText("+" + item.quantity + " " + item.item.name);

                    item.CallOnPickup();
                }
            }
        }
    }

    public void ResetSwing()
    {
        isSwinging = false;
          audio_manager.Source.Stop();

        Resource_Master resource = collider.transform.root.GetComponent<Resource_Master>();

        if(resource.ApplyDamage(equipment.damage))
        {
            inventory.AddItem(resource.data.yield, resource.data.quantity);

            Balloon b = Instantiate(balloonPrefab, Camera.main.WorldToScreenPoint(resource.transform.position), Quaternion.identity, balloonParent.transform).GetComponent<Balloon>();
            b.SetText("+" + resource.data.quantity + " " + resource.data.yield.name);

            resource.CallMineComplete();
        }

        collider = null;
    }
}
