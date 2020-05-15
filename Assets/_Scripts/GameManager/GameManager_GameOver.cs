using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager_GameOver : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;

    public GameObject gameOverUI;
    public GameObject mainMenuButton;
    public TextMeshProUGUI text;

    private CanvasGroup cg;
    private float fadeVelocity, fadeDuration = 1f;

    private Desert_DayNightCycle dayNight;

    private void OnEnable()
    {
        Initialize();
        gameManagerMaster.OnGameOver += GameOver;
    }

    private void Initialize()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        dayNight = GameObject.FindGameObjectWithTag("DesertManager").GetComponent<Desert_DayNightCycle>();
        cg = gameOverUI.GetComponent<CanvasGroup>();
        cg.alpha = 0;
        mainMenuButton.SetActive(false);
        text.gameObject.SetActive(false);
    }

    private void GameOver()
    {
        StartCoroutine(Display());
    }

    private IEnumerator Display()
    {
        while (cg.alpha < 0.95f)
        {
            cg.alpha = Mathf.SmoothDamp(cg.alpha, 1f, ref fadeVelocity, fadeDuration);
            yield return null;
        }

        text.text = "You survived " + dayNight.dayNumber + " days. Pathetic.";
        text.gameObject.SetActive(true);
        
        yield return new WaitForSeconds(3f);
        
        mainMenuButton.SetActive(true);
    }
    
    private void OnDisable()
    {
        gameManagerMaster.OnGameOver -= GameOver;   
    }
}
