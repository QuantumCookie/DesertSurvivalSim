using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager_PauseMenuUI : MonoBehaviour
{
    private GameManager_Master gameManagerMaster;

    public GameObject pauseMenu;
    public GameObject Button_Holder;

    public void Update(){
      if(Input.GetKeyDown(KeyCode.Escape)){
           Button_Holder.SetActive(true);
        
           }
         }
    private void OnEnable()
    {
        gameManagerMaster = GetComponent<GameManager_Master>();
        pauseMenu.SetActive(false);

        gameManagerMaster.OnTogglePause += UpdatePauseMenuUI;
    }

    private void UpdatePauseMenuUI()
    {
        if (gameManagerMaster.isGamePaused) pauseMenu.SetActive(true);
        else pauseMenu.SetActive(false);
    }

    private void OnDisable()
    {
        gameManagerMaster.OnTogglePause -= UpdatePauseMenuUI;
    }
    public void QuitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif
    }
    public void Instructions(){
          var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
         SceneManager.LoadScene("Instructions",parameters);
            Disable_Buttons();
        }
    public void Settings(){
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
         SceneManager.LoadScene("Settings",parameters);
          Disable_Buttons();
            }
    public void Credits(){
        var parameters = new LoadSceneParameters(LoadSceneMode.Additive);
         SceneManager.LoadScene("Credits",parameters);
         Disable_Buttons();


                }
   private void Disable_Buttons(){
     Button_Holder.SetActive(false);
   }

   }
