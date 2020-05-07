using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager_Cursor : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;

    public Texture2D mainCursor;

    private void OnEnable()
    {
        Initialize();
        gameManagerMaster.OnTogglePause += UpdateCursor;
        gameManagerMaster.OnGameOver += SetDefaultCursor;

        UpdateCursor();
    }

    private void Initialize()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
    }

    public void UpdateCursor()
    {
        SetDefaultCursor();
    }

    private void SetDefaultCursor()
    {
        Cursor.SetCursor(mainCursor, Vector2.zero, CursorMode.Auto);
    }

    private void OnDisable()
    {
        gameManagerMaster.OnTogglePause -= UpdateCursor;
        gameManagerMaster.OnGameOver -= SetDefaultCursor;
    }
}
