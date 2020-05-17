using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
public Canvas Button_Holder;

    public void StartGame()
    {
        SceneManager.LoadScene(1);

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
     Button_Holder.enabled = false;
   }
   void Update(){
       if (Input.GetKeyDown(KeyCode.Escape)){
     Button_Holder.enabled = true;
   }
}
}
