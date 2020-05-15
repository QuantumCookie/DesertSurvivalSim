using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class Animals_Fox_NavmeshAgent : MonoBehaviour
{
private NavMeshAgent fox_agent;
private GameObject player;
private bool Is_Following;
private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        fox_agent = transform.GetComponent<NavMeshAgent>();
        anim = transform.GetComponent<Animator>();
        anim.SetBool("Moving",false);
    }
    void Update(){
      if(Is_Following==true){
           fox_agent.SetDestination(player.transform.position);
             anim.SetBool("Moving",false);
      }
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player"){
          Is_Following = true;
        
    }

  }

}
