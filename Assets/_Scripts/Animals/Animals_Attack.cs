using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals_Attack : MonoBehaviour
{
 [HideInInspector]
  public Player_Health health;

     void OnCollisionEnter(Collision col){
       if(col.gameObject.tag=="Player"){
         health = col.gameObject.transform.GetComponent<Player_Health>();
         health.life-=10f;
       }
     }
}
