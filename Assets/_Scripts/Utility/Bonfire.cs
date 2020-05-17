using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire : MonoBehaviour
{
    [SerializeField] private float burnRate = 0.01f;
    [SerializeField] private ParticleSystem fire;
    [SerializeField] private Light light;
    
    private float maxWood = 10f;
    private float wood = 1f;

    private bool isActive = true;

    private void Start()
    {
        isActive = true;
    }

    private void Update()
    {
        Burn();
    }

    private void Burn()
    {
        if (!isActive) return;
        
        wood = Mathf.Max(0, wood - burnRate * Time.deltaTime);
        if (Mathf.CeilToInt(wood) == 0)
        {
            ToggleFire();
        }
    }

    public void Refuel(float amount)
    {
        wood = Mathf.Min(maxWood, wood + amount);
        if (!isActive) ToggleFire();
    }

    private void ToggleFire()
    {
        fire.gameObject.SetActive(!fire.gameObject.activeSelf);
        light.gameObject.SetActive(!light.gameObject.activeSelf);    
        isActive = !isActive;
    }

    public bool isBurning => isActive;
}
