using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //Movement
    public float moveSpeed = 15f;
    public float turnSmoothing = 5f;
    private GameManager_Master gameManagerMaster;
    private Player_Animation playerAnimation;

    private Vector3 direction;
    private Rigidbody rb;
    
    private void Start()
    {
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
        rb = GetComponent<Rigidbody>();
        playerAnimation = GetComponent<Player_Animation>();
        direction = transform.forward;
    }

    void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;
        
        Move();
    }

    private void Move()
    {
        Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;
        if (input.magnitude < 0.01f)
        {
            playerAnimation.SetRunning(false);
            return;
        }

        direction = Vector3.Lerp(direction, input, turnSmoothing * Time.deltaTime);
        rb.MoveRotation(Quaternion.LookRotation(direction));
        rb.velocity = direction * moveSpeed;
        playerAnimation.SetRunning(true);
    }
}