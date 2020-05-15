using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_EquipmentManager : MonoBehaviour
{
    public Transform equipmentRoot;
    private List<Equipment_Master> list;
    private GameManager_Master gameManagerMaster;
    private int activeIndex;
    private bool locked;

    public float damage => list[activeIndex].equipment.damage;
    
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        list = new List<Equipment_Master>(equipmentRoot.GetComponentsInChildren<Equipment_Master>());
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        
        if(list.Count == 0)
        {
            activeIndex = -1;
            return;
        }

        for (int i = 0; i < list.Count; i++)
        {
            list[i].gameObject.SetActive(false);
        }
        
        list[0].gameObject.SetActive(true);
        activeIndex = 0;
        
        locked = false;
    }

    private void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            CycleEquipment();
        }
    }

    private void CycleEquipment()
    {
        list[activeIndex].gameObject.SetActive(false);
        activeIndex = (activeIndex + 1) % list.Count;
        list[activeIndex].gameObject.SetActive(true);
    }

    public EquipmentObject Equip(EquipmentObject eq)
    {
        if (activeIndex == -1)
        {
            list.Add(Instantiate(eq.prefab).GetComponent<Equipment_Master>());
            activeIndex = 0;
            return null;
        }
        else
        {
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i].equipment.inventoryItem == eq)
                {
                    list[activeIndex].gameObject.SetActive(false);
                    list[i].gameObject.SetActive(true);
                    int x = activeIndex;
                    activeIndex = i;
                    return list[x].equipment.inventoryItem;
                }
            }
            
            list.Add(Instantiate(eq.prefab).GetComponent<Equipment_Master>());
            activeIndex = list.Count - 1;
            return null;
        }
    }

    public bool CanMine(ResourceType type)
    {
        return !locked && list[activeIndex].equipment.harvests.Contains(type);
    }
}
