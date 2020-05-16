using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_MineResource : MonoBehaviour
{


  [HideInInspector]
  public GameObject Audio_manager;
  [HideInInspector]
  public Audio_manager audio_manager;
    private Player_DetectItem itemDetector;
    private Player_Inventory inventory;
    private GameManager_Master gameManagerMaster;
    private Player_EquipmentManager equipment;
    private Player_Animation playerAnimation;

    public GameObject balloonParent;
    public GameObject balloonPrefab;

<<<<<<< HEAD
=======
    private bool isSwinging;
    private Collider collider;
    
>>>>>>> 72ea04d46cd790885e567c8caaa157aa25006cfd
    private void Start()
    {
        itemDetector = GetComponent<Player_DetectItem>();
        inventory = GetComponent<Player_Inventory>();
        equipment = GetComponent<Player_EquipmentManager>();
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        playerAnimation = GetComponent<Player_Animation>();
<<<<<<< HEAD
        Audio_manager = GameObject.FindWithTag("Audio");
        audio_manager = Audio_manager.transform.GetComponent<Audio_manager>();
=======
        isSwinging = false;
        collider = null;
>>>>>>> 72ea04d46cd790885e567c8caaa157aa25006cfd
    }

    private void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;

<<<<<<< HEAD
=======
        if (isSwinging) return;
        
>>>>>>> 72ea04d46cd790885e567c8caaa157aa25006cfd
        if (Input.GetKeyDown(KeyCode.E))
        {

            //Debug.Log("Pressed E");
            if (equipment.CanMine(itemDetector.resourceType)&&(itemDetector.master.data.type==  ResourceType.Tree || itemDetector.master.data.type==  ResourceType.Cactus))
            {
<<<<<<< HEAD
                playerAnimation.SetSwinging(true);
                audio_manager.Source.clip = audio_manager.Wood_chopping;
                  audio_manager.Source.Play();

            }
            else if (equipment.CanMine(itemDetector.resourceType)&&itemDetector.master.data.type==  ResourceType.Rock)
            {
                playerAnimation.SetSwinging(true);
                audio_manager.Source.clip = audio_manager.Rock_breaking;
                          audio_manager.Source.Play();

=======
                isSwinging = true;
                collider = itemDetector.collider;
                playerAnimation.SetSwinging();
>>>>>>> 72ea04d46cd790885e567c8caaa157aa25006cfd
            }
            else if (itemDetector.itemType != ItemType.Null)
            {
                Item_Master item = itemDetector.collider.transform.root.GetComponent<Item_Master>();

                if(item.canPickup)
                {
                    inventory.AddItem(item.item, item.quantity);

                    Balloon b = Instantiate(balloonPrefab, Camera.main.WorldToScreenPoint(item.transform.position), Quaternion.identity, balloonParent.transform).GetComponent<Balloon>();
                    b.SetText("+" + item.quantity + " " + item.name);

                    item.CallOnPickup();
                }
            }



        }
    }

    public void ResetSwing()
    {
<<<<<<< HEAD
        playerAnimation.SetSwinging(false);

        Resource_Master resource = itemDetector.collider.transform.root.GetComponent<Resource_Master>();

=======
        isSwinging = false;
        
        Resource_Master resource = collider.transform.root.GetComponent<Resource_Master>();
        
>>>>>>> 72ea04d46cd790885e567c8caaa157aa25006cfd
        if(resource.ApplyDamage(equipment.damage))
        {
            inventory.AddItem(resource.data.yield, resource.data.quantity);


            Balloon b = Instantiate(balloonPrefab, Camera.main.WorldToScreenPoint(resource.transform.position), Quaternion.identity, balloonParent.transform).GetComponent<Balloon>();
            b.SetText("+" + resource.data.quantity + " " + resource.data.yield.name);

            resource.CallMineComplete();

            audio_manager.Source.Stop();
        }

        collider = null;
    }
}
