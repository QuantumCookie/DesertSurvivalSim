using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_FeedBonfire : MonoBehaviour
{
    private Player_DetectItem itemDetector;
    private Player_Inventory inventory;
    private GameManager_Master gameManagerMaster;

    public BaseObject wood;
    public GameObject balloon, balloonParent;
    
    private void Start()
    {
        itemDetector = GetComponent<Player_DetectItem>();
        inventory = GetComponent<Player_Inventory>();
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
    }

    private void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (itemDetector.collider)
            {
                Bonfire b = itemDetector.collider.GetComponent<Bonfire>();
                if (b)
                {
                    if (inventory.GetTotalItemCount(wood) > 0)
                    {
                        inventory.RemoveItem(wood, 1);
                        b.Refuel(1);
                        Balloon text = Instantiate(balloon, Camera.main.WorldToScreenPoint(transform.position), Quaternion.identity, balloonParent.transform).GetComponent<Balloon>();
                        text.SetText("-1 " + wood.name);
                    }
                }
            }
        }
    }
}
