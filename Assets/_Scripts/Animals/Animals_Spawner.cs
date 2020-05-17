using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Animals_Spawner : MonoBehaviour
{
    Vector3 RandomPoint;
    public GameObject fox;
    //approx length and breadth of map(flats) from the spawner
    private float radius = 254f;
    public float no_of_foxes;

    void Update()
    {
      if(no_of_foxes>0f){
      Invoke("SetOrigin",0f);
      GameObject go = Instantiate(fox);
          go.GetComponent<NavMeshAgent>().Warp(RandomPoint);
      no_of_foxes--;
      }
    }
    void SetOrigin(){
      RandomPoint = this.gameObject.transform.position;
      RandomPoint.x += Random.Range(-radius, radius);
      RandomPoint.z += Random.Range(-radius, radius);
    }
}
