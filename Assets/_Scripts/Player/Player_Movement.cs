/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //Movement
    public float moveSpeed = 15f;
    private Vector3 speedSmoothVelocity;
    public float movementSmoothingTime = 0.01f;
    private Vector3 currentVelocity;

    //Turning
    public float turnSpeed = 20f;
    public LayerMask floorMask;
    private float maxRaycastDistance = 100;
    private Quaternion newRotation;

    private GameManager_Master gameManagerMaster;

    private void Start()
    {
        gameManagerMaster = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_Master>();
    }

    void Update()
    {
        if(gameManagerMaster.isGamePaused || gameManagerMaster.isGameOver)return;
        
        Move();
        Turn();
    }

    private void Move()
    {
        Vector3 direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical")).normalized;

        transform.position += direction * moveSpeed * Time.deltaTime;
    }

    private void Turn()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, maxRaycastDistance, floorMask))
        {
            Vector3 targetDir = hit.point - transform.position;

            newRotation = Quaternion.Euler(0f, Mathf.Atan2(targetDir.x, targetDir.z) * Mathf.Rad2Deg, 0f);
            transform.rotation = newRotation;
        }
    }
}*/

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
        //transform.rotation = Quaternion.LookRotation(direction);
        //transform.position += direction * moveSpeed * Time.deltaTime;
        playerAnimation.SetRunning(true);
    }
}