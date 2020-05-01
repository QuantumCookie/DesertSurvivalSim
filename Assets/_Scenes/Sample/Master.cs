using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Master : MonoBehaviour
{
    public delegate void GeneralEvent();

    public event GeneralEvent playerDead;

    public void PlayerDead()
    {
        playerDead?.Invoke();//Calls all methods subscribed to this event
    }
}
