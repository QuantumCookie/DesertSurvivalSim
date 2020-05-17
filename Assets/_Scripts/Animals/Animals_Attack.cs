using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals_Attack : MonoBehaviour
{
 [HideInInspector]
  public Player_Health health;
   [HideInInspector]
  public GameObject Audio_manager;
   [HideInInspector]
  public Audio_manager audio_manager;

    private float lastAttack;
    
     void Start(){
          Audio_manager = GameObject.FindWithTag("Audio");
          audio_manager = Audio_manager.transform.GetComponent<Audio_manager>();
     }

     void OnCollisionStay(Collision col){
       if(col.gameObject.tag=="Player"){
         health = col.gameObject.transform.GetComponent<Player_Health>();
           if(Time.time > lastAttack + 1f)
           {
               health.Attack(5f);
               audio_manager.Source.clip = audio_manager.hit;
               audio_manager.Source.Play();
               lastAttack = Time.time;
           }
       }
     }
}
