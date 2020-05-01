using System.Collections;
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
    
    void Update()
    {
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
}