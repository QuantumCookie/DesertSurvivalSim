using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Master : MonoBehaviour
{
    public delegate void GeneralEvent();

    public delegate void HealthChangeEvent(float delta);
    public event HealthChangeEvent OnHealthGain;
    public event HealthChangeEvent OnHealthDeduct;

    public void CallHealthGainEvent(float delta)
    {
        if (OnHealthGain != null)
        {
            OnHealthGain(delta);
        }
    }

    public void CallHealthDeductEvent(float delta)
    {
        if (OnHealthDeduct != null)
        {
            OnHealthDeduct(delta);
        }
    }
}
