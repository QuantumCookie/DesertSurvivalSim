using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Cold : MonoBehaviour
{
    private float maxCold = 100f;
    public float _maxCold => maxCold;
    private float cold = 0f;
    public float _cold => cold;

    [SerializeField] private float threshold = 35f;
    [SerializeField] private float hotThreshold = 42f;
    
    [SerializeField] private float coldBuildupRate = 0.1f;
    [SerializeField] private float sunHeatRate = 5f;
    [SerializeField] private float fireHeatRate = 10f;

    [SerializeField] private GameObject coldUI;
    
    private Player_CoreTemperature coreTemp;
    private Player_DetectItem itemDetector;
    private GameManager_Master gameManagerMaster;

    public Bonfire bonfire;
    
    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        coreTemp = GetComponent<Player_CoreTemperature>();
        itemDetector = GetComponent<Player_DetectItem>();
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
    }

    private void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;
        
        UpdateCold();
        UpdateColdUI();
    }

    private void UpdateCold()
    {
        if (CheckBonfire())
        {
            cold = Mathf.Max(0, cold - fireHeatRate * Time.deltaTime);
        }
        else
        {
            if (coreTemp.coreTemperature < threshold || coreTemp.coreTemperature > hotThreshold)
            {
                cold += coldBuildupRate * Time.deltaTime;
            
                if (cold > maxCold)
                {
                    cold = maxCold;
                    //gameManagerMaster.CallGameOverEvent();
                }
            }
            else
            {
                cold = Mathf.Max(0, cold - sunHeatRate * Time.deltaTime);
            }
        }
    }

    private void UpdateColdUI()
    {
        Vector3 scale = coldUI.transform.localScale;
        scale.x = cold / maxCold;

        coldUI.transform.localScale = scale;
    }

    private bool CheckBonfire()
    {
        return Vector3.Distance(transform.position, bonfire.transform.position) < 5f && bonfire.isBurning;
    }
}
