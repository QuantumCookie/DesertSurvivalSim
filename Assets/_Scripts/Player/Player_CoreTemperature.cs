using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_CoreTemperature : MonoBehaviour
{
    private float _coreTemperature = 37;
    public float coreTemperature => _coreTemperature;
    
    [SerializeField] private float idealMax = 40;
    [SerializeField] private float idealMin = 35;
    [SerializeField] private float criticalMax = 43;
    [SerializeField] private float criticalMin = 30;

    [SerializeField] private float environmentMedian = 25;
    [SerializeField] private float bodyMedian = 37;
    
    private Desert_TemperatureCycle temperatureScript;
    private Player_Hydration hydrationScript;

    [SerializeField] private TextMeshProUGUI coreTempUI;
    [SerializeField] private float updateDelay = 0.5f;
    private float lastUpdate;
    
    private void Start()
    {
        temperatureScript = GameObject.FindGameObjectWithTag("DesertManager").GetComponent<Desert_TemperatureCycle>();
        hydrationScript = GetComponent<Player_Hydration>();
        UpdateUI();
    }

    private void Update()
    {
        UpdateTemperature();

        if (Time.time > lastUpdate + updateDelay)
        {
            UpdateUI();
        }
    }

    private float InverseLerp(float a, float b, float val)
    {
        return (b - val) / (b - a);
    }

    private float Lerp(float a, float b, float t)
    {
        return a * t + (1 - t) * b;
    }
    
    private void UpdateTemperature()
    {
        float envMin = temperatureScript.MinTemperature;
        float envMax = temperatureScript.MaxTemperature;
        float envTemp = temperatureScript.Temperature;

        float delta = InverseLerp(envMin, envMax, envTemp);
        float f = hydrationScript.percent * 0.5f;
        _coreTemperature = Lerp(criticalMin, criticalMax, delta * delta * delta);
    }

    private void UpdateUI()
    {
        coreTempUI.text = _coreTemperature.ToString("F1") + "°C";
    }
}
