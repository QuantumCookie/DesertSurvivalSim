using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Master : MonoBehaviour
{
    public bool isGameOver;
    public bool isGamePaused;

    public delegate void GeneralEvent();

    public event GeneralEvent OnGameOver;
    public event GeneralEvent OnTogglePause;
    public event GeneralEvent OnWaveOver;

    private void OnEnable()
    {
        isGameOver = false;
        isGamePaused = false;
    }

    public void CallGameOverEvent()
    {
        isGameOver = true;
        if (OnGameOver != null) OnGameOver();
    }

    public void CallTogglePauseEvent()
    {
        isGamePaused = !isGamePaused;
        if (OnTogglePause != null) OnTogglePause();
    }

    public void CallWaveOverEvent()
    {
        if (OnWaveOver != null) OnWaveOver();
    }
}
