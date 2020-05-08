using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = System.Random;

public class Desert_TemperatureCycle : MonoBehaviour
{
    [SerializeField] private Vector2 minTemperature;
    [SerializeField] private Vector2 maxTemperature;
    
    private Desert_DayNightCycle dayNight;

    private float _temp;
    public float Temperature => _temp;

    private float minTemp, maxTemp;
    public float MinTemperature => minTemp;
    public float MaxTemperature => maxTemp;
    
    //Display
    public TextMeshProUGUI temperatureDisplay;
    private float updateDelay = 1f;
    private float lastUpdate;
    
    private void Start()
    {
        dayNight = GetComponent<Desert_DayNightCycle>();
        UpdateTemperature();
        UpdateDisplay();
    }

    private void Update()
    {
        if(Time.time > lastUpdate + updateDelay)
        {
            UpdateTemperature();
            UpdateDisplay();

            lastUpdate = Time.time;
        }
    }

    private void UpdateTemperature()
    {
        Random prng = new Random((int)(dayNight.dayNumber * 523984902));
        minTemp = minTemperature.x + (minTemperature.y - minTemperature.x) * (float)prng.NextDouble();
        maxTemp = maxTemperature.x + (maxTemperature.y - maxTemperature.x) * (float)prng.NextDouble();
        _temp = minTemp + (maxTemp - minTemp) * Mathf.Sin(dayNight.timeOfDay * Mathf.PI);
    }

    private void UpdateDisplay()
    {
        temperatureDisplay.text = _temp.ToString("F1") + "°C";
    }
}
