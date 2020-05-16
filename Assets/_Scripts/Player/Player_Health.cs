using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Player_Health : MonoBehaviour
{
    public float life = 100f;
    public Slider health;
    public Image Health_fill;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
       health.value = life;
       Health_fill.color = new Color(Mathf.Clamp((1 - life / 100f), 0, 1), Mathf.Clamp((life / 100f), 0, 1), 0, 0.5f);


    }
}
