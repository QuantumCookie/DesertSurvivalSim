using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_TemperatureEffect : MonoBehaviour
{
    [SerializeField] private float maxEffect = 7;//At max temperature, survival time (indirectly, dehydration rate) with full water level decreases by this value (in hrs)
    [SerializeField] private float thresholdTemperature = 25;//Every degree rise in temp above this value (in °C) increases dehydration rate
    [SerializeField] private float deltaPerDegree = 0.46f;

    private Desert_TemperatureCycle temp;
    private Player_Hydration hydrationScript;
    
    private void Start()
    {
        temp = GameObject.FindGameObjectWithTag("DesertManager").GetComponent<Desert_TemperatureCycle>();
        hydrationScript = GetComponent<Player_Hydration>();
        hydrationScript.computeHydration += AffectDehydrationRate;
    }

    private void AffectDehydrationRate(ref float survivalTime)
    {
        survivalTime -= Mathf.Min(maxEffect, Mathf.Max(temp.Temperature - thresholdTemperature, 0) * deltaPerDegree);
    }

    private void OnDisable()
    {
        hydrationScript.computeHydration -= AffectDehydrationRate;   
    }
}
