using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Resource_MineComplete : MonoBehaviour
{
    private Resource_Master master;
    
    private void OnEnable()
    {
        master = GetComponent<Resource_Master>();
        master.onMineComplete += DestroyResource;
    }

    private void DestroyResource()
    {
        Destroy(gameObject);
    }

    private void OnDisable()
    {
        master.onMineComplete -= DestroyResource;
    }
}
