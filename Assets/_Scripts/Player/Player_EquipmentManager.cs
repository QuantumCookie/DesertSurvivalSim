using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player_EquipmentManager : MonoBehaviour
{
    private List<Equipment_Master> list;
    private Equipment_Master active;
    private bool locked;
    
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        list = new List<Equipment_Master>(GetComponentsInChildren<Equipment_Master>());
        
        if(list.Count == 0) return;

        for (int i = 0; i < list.Count; i++)
        {
            list[i].gameObject.SetActive(false);
        }
        
        list[0].gameObject.SetActive(true);
        active = list[0];
        
        locked = false;
    }

    public bool CanMine(ResourceType type)
    {
        return !locked && active.equipment.harvests.Contains(type);
    }
}
