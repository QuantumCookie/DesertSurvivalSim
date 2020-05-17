using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_DestroyOnPickup : MonoBehaviour
{
    private Item_Master master;
    
    private void OnEnable()
    {
        master = GetComponent<Item_Master>();
        master.onPickup += DestroyResource;
    }

    private void DestroyResource()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        master.onPickup -= DestroyResource;
    }
}
