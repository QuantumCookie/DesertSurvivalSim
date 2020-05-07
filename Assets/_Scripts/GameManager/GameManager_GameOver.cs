using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_GameOver : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;

    public GameObject gameOverUI;

    private CanvasGroup cg;
    private float fadeVelocity, fadeDuration = 1f;

    private void OnEnable()
    {
        Initialize();
    }

    private void Initialize()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        cg = gameOverUI.GetComponent<CanvasGroup>();
        cg.alpha = 0;
    }

    private void Update()
    {
        if(gameManagerMaster.isGameOver)
        {
            cg.alpha = Mathf.SmoothDamp(cg.alpha, 1f, ref fadeVelocity, fadeDuration);
        }
    }

    private void OnDisable()
    {
        
    }
}
