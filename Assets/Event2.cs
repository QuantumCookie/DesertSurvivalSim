using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Event2 : MonoBehaviour
{
    public Master master;
    public FloatVariable health;

    private void OnEnable()
    {
        master.playerDead += DoThis;//Call DoThis() if player dies
    }

    private void DoThis()
    {
        Debug.Log("Disable Input, with final health of " + health.value);
    }

    private void OnDisable()
    {
        master.playerDead -= DoThis;//Unsubscribe from event
    }
}
