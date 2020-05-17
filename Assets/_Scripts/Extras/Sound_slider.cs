using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Sound_slider : MonoBehaviour
{
  public Slider Vol_slider;
  private AudioSource[] Main_Audio;
  private GameObject source;
    // Start is called before the first frame update
    void Start()
    {
        Vol_slider = this.gameObject.GetComponent<Slider>();
        source = GameObject.Find("Audio manager");
        Main_Audio = source.transform.GetComponents<AudioSource>();
        Vol_slider.value = Main_Audio[0].volume;
    }

    // Update is called once per frame
    void Update()
    {

      Main_Audio[0].volume = Vol_slider.value;
      if(Main_Audio[1] != null){
        Main_Audio[1].volume = Vol_slider.value;
      }
    }
}
