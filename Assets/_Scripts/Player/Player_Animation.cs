using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Animation : MonoBehaviour
{
    public string runParameter = "IsRunning";
    public string swingParameter = "IsSwinging";
    public string dieParameter = "Die";
    
    private Animator anim;
    private int anim_IsRunning;
    private int anim_IsSwinging;
    private int anim_Die;

    private void Start()
    {
        Initialize();
    }

    private void Initialize()
    {
        anim = GetComponent<Animator>();
        anim_IsRunning = Animator.StringToHash(runParameter);
        anim_IsSwinging = Animator.StringToHash(swingParameter);
        anim_Die = Animator.StringToHash(dieParameter);
    }

    public void SetRunning(bool flag)
    {
        anim.SetBool(anim_IsRunning, flag);
    }

    public void SetDie()
    {
        anim.SetTrigger(anim_Die);
    }

    public void SetSwinging(bool flag)
    {
        anim.SetBool(anim_IsSwinging, flag);
    }
}
