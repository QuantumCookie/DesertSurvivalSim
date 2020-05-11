using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_EquipmentManager : MonoBehaviour
{
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
        list = new List<Equipment_Master>(GetComponentsInChildren<Equipment_Master>());
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        
        if(list.Count == 0) return;

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

    public bool CanMine(ResourceType type)
    {
        return !locked && list[activeIndex].equipment.harvests.Contains(type);
    }
}
