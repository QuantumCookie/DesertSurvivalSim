using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public float life = 100f;
    private float lifeDrainRate = 2f;

    public GameObject healthUI, healthDrainOverlay;
    
    private Player_Hydration hydration;
    private Player_Cold cold;
    private GameManager_Master gameManagerMaster;

    private void Start()
    {
        hydration = GetComponent<Player_Hydration>();
        cold = GetComponent<Player_Cold>();
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
    }

    private void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;
        
        if (hydration.currentHydration <= 0 || cold._cold >= cold._maxCold)
        {
            life -= lifeDrainRate * Time.deltaTime;
            healthDrainOverlay.SetActive(true);

            if (life <= 0)
            {
                life = 0;
                gameManagerMaster.CallGameOverEvent();
            }
        }
        else
        {
            healthDrainOverlay.SetActive(false);
        }

        Vector3 scale = healthUI.transform.localScale;
        scale.x = life / 100f;
        healthUI.transform.localScale = scale;
    }

    public void Add(float value)
    {
        life = Mathf.Min(100f, life + value);
    }

    public void Attack(float value)
    {
        life = Mathf.Max(0, life - value);
        if (life <= 0) gameManagerMaster.CallGameOverEvent();
    }
}
