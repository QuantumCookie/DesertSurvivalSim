using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio_manager : MonoBehaviour
{


  public AudioClip hit;
  public AudioClip Wood_chopping;
  public AudioClip Fox_laugh;
  public AudioClip Rock_breaking;
  public AudioClip Heat_Stroke;
  public AudioClip Crafting;

[HideInInspector]
  public AudioSource Source;


  void Start(){
    Source = transform.GetComponent<AudioSource>();
  }
}
