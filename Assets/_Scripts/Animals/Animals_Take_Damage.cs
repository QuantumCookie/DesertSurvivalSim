using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animals_Take_Damage : MonoBehaviour
{
    // Hit will be triggered when player hits the fox
     [HideInInspector]
    public bool Hit;
    private float Fox_health = 50f;
    private Animals_Spawner spawner;
      [HideInInspector]
    public GameObject spawner_object;
    private Rigidbody rigidbody;
    public float backlash;

    // Start is called before the first frame update
    void Start()
    {
    //  rigidbody = transform.GetComponent<Rigidbody>();
      spawner_object = GameObject.FindWithTag("Spawner");
      spawner = spawner_object.GetComponent<Animals_Spawner>();
        Hit = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Hit==true){
          Fox_health -=20f;
          rigidbody.AddForce(-transform.forward * backlash);
            
          Hit = false;
        }
        if(Fox_health<0f){
          spawner.no_of_foxes++;
          Destroy(gameObject);
        }
    }
}
