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
[HideInInspector]
public GameObject Audio_manager;
[HideInInspector]
public Audio_manager audio_manager;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        fox_agent = transform.GetComponent<NavMeshAgent>();
        anim = transform.GetComponent<Animator>();
        anim.SetBool("Moving",false);
        Audio_manager = GameObject.FindWithTag("Audio");
        audio_manager = Audio_manager.transform.GetComponent<Audio_manager>();
    }
    void Update(){
      if(Is_Following==true){
           fox_agent.SetDestination(player.transform.position);
             anim.SetBool("Moving",true);
      }
      /*else
      {
          Vector3 randomDestination = transform.position + Random.insideUnitSphere * 10f;
          randomDestination.y = transform.position.y;
          fox_agent.SetDestination(randomDestination);
          anim.SetBool("Moving",true);    
      }*/
    }
    // Update is called once per frame
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player"){
          Is_Following = true;
          audio_manager.Source.clip = audio_manager.Fox_laugh;
          audio_manager.Source.Play();

    }

  }

}
