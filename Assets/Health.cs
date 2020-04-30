using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    public FloatVariable health;

    private Master master;

    private void Start()
    {
        master = GetComponent<Master>();
        StartCoroutine(Damage());
    }

    private IEnumerator Damage()
    {
        while (health.value > 0)
        {
            health.value -= 10;
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Update()
    {
        if (health.value <= 0)
        {
            master.PlayerDead();//Invoke player dead delegate in Master script
            Destroy(gameObject);
        }
    }
}
