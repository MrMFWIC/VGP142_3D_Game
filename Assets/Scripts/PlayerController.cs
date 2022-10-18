using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public Camera playerCamera;
    public float moveSpeed = 10;
    public float gravity = 9.81f;
    public float jumpSpeed = 10.0f;
    
    CharacterController controller;

    Vector3 curMoveInput;


    void Start()
    {
        try
        {
            controller = GetComponent<CharacterController>();
            controller.minMoveDistance = 0.0f;

            if(moveSpeed <= 0.0f)
            {
                moveSpeed = 6.0f;
                throw new ArgumentNullException("Movespeed argument is null so the value has been defaulted to 6.0");
            }
        }

        catch (ArgumentNullException e)
        {
            Debug.Log(e.Message);
        }

        finally
        {
            Debug.Log("Movespeed validation always runs");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (controller.isGrounded)
        {
            curMoveInput = transform.TransformDirection(curMoveInput);
        }

        curMoveInput.y -= gravity * Time.deltaTime;
        controller.Move(curMoveInput * Time.deltaTime);
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        Vector2 move = context.action.ReadValue<Vector2>();
        move.Normalize();
        
        Vector3 moveVel = new Vector3(move.x, 0, move.y);
        moveVel *= moveSpeed;

        curMoveInput = moveVel;
    }

    public void Look(InputAction.CallbackContext context)
    {
        
    }

    public void Fire(InputAction.CallbackContext context)
    {
        try
        {
            if (context.action.WasPerformedThisFrame())
            {
                throw new UnassignedReferenceException("Fire Pressed");
            }

            if (context.action.WasReleasedThisFrame())
            {
                throw new UnassignedReferenceException("Fire Pressed");
            }
        }
        
        catch (UnassignedReferenceException e)
        {
            Debug.Log(e.Message);
        }
    }
}
