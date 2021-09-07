using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;
    public float forwardSpeed = 12f;
    public float jumpSpeed = 8f;
    public float gravity = 20f;
    public float rotateSpeed = 3f;
    private Vector3 moveDirection = Vector3.zero;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(controller.isGrounded)
        {
            //float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");
            moveDirection = transform.TransformDirection(new Vector3(0, 0, z)) * forwardSpeed;
            if(Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }

        moveDirection.y -= gravity * Time.deltaTime;        
        controller.Move(moveDirection * Time.deltaTime);
        transform.Rotate(0, Input.GetAxis("Horizontal"), 0);
    }
}
