using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Desert_DayNightCycle : MonoBehaviour
{
    [SerializeField] [Range(0.1f, 13)]
    private float _targetDayLength = 0.5f; //in minutes
    private float targetDayLength
    {
        get { return _targetDayLength; }
    }
    
    [SerializeField] [Range(0, 1)] 
    private float _timeOfDay = 0.5f;
    private float timeOfDay
    {
        get { return _timeOfDay; }
    }
    
    [SerializeField]
    private int _dayNumber = 0;
    private float dayNumber
    {
        get { return _dayNumber; }
    }
    
    [SerializeField] 
    private Transform dailyRotation;
    
    [SerializeField] 
    private Light sun;

    [SerializeField]
    private Gradient intensityGradient;
    
    private float intensity;

    [SerializeField]
    private float sunBaseIntensity = 1f;
    
    [SerializeField]
    private float sunVariation = 1.5f;
    
    private float _timeScale = 100f;

    public bool pause = false;

    private void Update()
    {
        if (!pause)
        {
            UpdateTimeScale();
            UpdateTime();
            AdjustSunRotation();
            SunIntensity();
        }
    }

    private void UpdateTimeScale()
    {
        _timeScale = 1440 / _targetDayLength;
    }

    private void UpdateTime()
    {
        _timeOfDay += Time.deltaTime * _timeScale / 86400;
        if (_timeOfDay > 1)
        {
            _dayNumber++;
            _timeOfDay--;
        }
    }

    private void AdjustSunRotation()
    {
        float sunAngle = _timeOfDay * 360f;
        dailyRotation.localRotation = Quaternion.Euler(0, 0, sunAngle);
    }

    private void SunIntensity()
    {
        intensity = Vector3.Dot(sun.transform.forward, Vector3.down);
        intensity = Mathf.Clamp01(intensity);
        sun.intensity = intensity * sunVariation + sunBaseIntensity;
        sun.color = intensityGradient.Evaluate(sun.intensity);
    }
}
