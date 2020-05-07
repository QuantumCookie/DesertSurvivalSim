using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class Resource_Master : MonoBehaviour
{
    public ResourceSO data;
    private float _health;
    public float health => _health;

    public delegate void GeneralEvent();

    public event GeneralEvent onMineComplete;
    
    private void Start()
    {
        _health = data.damageNeeded;
    }

    public bool ApplyDamage(float damage)
    {
        _health -= damage;
        return _health <= 0;
    }

    public void CallMineComplete()
    {
        onMineComplete?.Invoke();
    }
}
