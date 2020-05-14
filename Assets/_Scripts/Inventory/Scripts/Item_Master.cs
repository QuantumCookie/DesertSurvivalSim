using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item_Master: MonoBehaviour
{
    public BaseObject item;
    public int quantity;
    public bool canPickup;
    
    public delegate void GeneralEvent();

    public event GeneralEvent onPickup;

    private void OnEnable()
    {
        canPickup = true;
    }

    public void CallOnPickup()
    {
        onPickup?.Invoke();
    }
}
