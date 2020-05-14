using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_PricklyPear : MonoBehaviour
{
    public GameObject fruits;
    public float regenerationTime = 120f;
    
    private Item_Master itemMaster;
    private float timer;

    private void Start()
    {
        itemMaster = GetComponent<Item_Master>();
        itemMaster.onPickup += DisableMaster;
    }

    private void DisableMaster()
    {
        itemMaster.canPickup = false;
        fruits.SetActive(false);
        timer = 0;
    }

    private void Update()
    {
        if (itemMaster.canPickup) return;
        
        timer += Time.deltaTime;

        if (timer > regenerationTime) EnableMaster();
    }

    private void EnableMaster()
    {
        itemMaster.canPickup = true;
        fruits.SetActive(true);
    }

    private void OnDisable()
    {
        itemMaster.onPickup -= DisableMaster;
    }
}
