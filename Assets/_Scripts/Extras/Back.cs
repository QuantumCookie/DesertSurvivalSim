using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Back : MonoBehaviour
{
  public string scene_name;

    
    void Update()
    {
      if(Input.GetKeyDown(KeyCode.Escape)){

           SceneManager.UnloadScene(scene_name);
           }
    }
}
