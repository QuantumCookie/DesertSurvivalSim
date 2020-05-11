using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Player_Hydration : MonoBehaviour
{
    [SerializeField] private float maxSurvivalTime = 24; //How many real-time hours the player can survive from a full Hydration bar
    [SerializeField] private float maxHydration = 100f;
    
    private float _currentHydration;
    public float currentHydration => _currentHydration;
    public float percent => _currentHydration / maxHydration;

    private float _dehydrationRate;
    public float dehydrationRate => _dehydrationRate;


    private Desert_DayNightCycle dayNight;

    public delegate void ComputeEvent(ref float value);
    public event ComputeEvent computeHydration;

    private GameManager_Master gameManagerMaster;
    
    //Display
    public TextMeshProUGUI hydrationDisplay;
    [SerializeField] private float updateDelay = 0.5f;
    private float lastUpdate;

    private void Start()
    {
        dayNight = GameObject.FindGameObjectWithTag("DesertManager").GetComponent<Desert_DayNightCycle>();
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        _currentHydration = maxHydration;
        UpdateHydrationDisplay();
    }

    private void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;
        
        ComputeDehydrationRate();
        Dehydrate();
        UpdateHydrationDisplay();
    }

    private void ComputeDehydrationRate()
    {
        float currentSurvivalTime = maxSurvivalTime;
        computeHydration?.Invoke(ref currentSurvivalTime);
        //Debug.Log(currentSurvivalTime);
        
        _dehydrationRate = (maxHydration * 0.4f) / (currentSurvivalTime * dayNight.targetDayLength); //maxHydration * 24 / (currentSurvivalTime * dayNight.targetDayLength * 60);
    }

    private void Dehydrate()
    {
        _currentHydration -= _dehydrationRate * Time.deltaTime;
    }

    public void Replenish(float value)
    {
        _currentHydration = Mathf.Min(_currentHydration + value, maxHydration);
    }

    private void UpdateHydrationDisplay()
    {
        if (Time.time < lastUpdate + updateDelay) return;
        
        hydrationDisplay.text = Mathf.RoundToInt(_currentHydration * 100 / maxHydration).ToString() + "%";
        lastUpdate = Time.time;
    }
}
