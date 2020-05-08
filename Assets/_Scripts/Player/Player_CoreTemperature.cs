using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_CoreTemperature : MonoBehaviour
{
    private float _coreTemperature = 37;
    public float coreTemperature => _coreTemperature;
    
    [SerializeField] private float idealMax = 40;
    [SerializeField] private float idealMin = 35;
    [SerializeField] private float criticalMax = 43;
    [SerializeField] private float criticalMin = 32;

    [SerializeField] private float environmentMedian = 25;
    [SerializeField] private float bodyMedian = 37;
    
    private Desert_TemperatureCycle temperatureScript;
    private Player_Hydration hydrationScript;

    private void Start()
    {
        temperatureScript = GameObject.FindGameObjectWithTag("DesertManager").GetComponent<Desert_TemperatureCycle>();
        hydrationScript = GetComponent<Player_Hydration>();
    }

    private void Update()
    {
        UpdateTemperature();
    }

    private float inverseLerp(float a, float b, float val)
    {
        return (val - a) / (b - a);
    }
    
    private void UpdateTemperature()
    {
        float envMin = temperatureScript.MinTemperature;
        float envMax = temperatureScript.MaxTemperature;
        float envTemp = temperatureScript.Temperature;

        if (envTemp > environmentMedian)
        {
            float delta = inverseLerp(environmentMedian, envMax, envTemp);    
            
        }
        else
        {
            
        }
    }
}
