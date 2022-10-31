using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    Animator anim;

    public Camera playerCamera;
    public float moveSpeed = 10;
    public float gravity = 9.81f;
    public float jumpSpeed = 10.0f;
    public float projectileSpeed = 40f;

    public Rigidbody projectile;
    public Transform projectileSpawnPoint;
    
    CharacterController controller;

    Vector3 curMoveInput;
    Vector3 destination;
    Vector2 move;

    void Start()
    {
        try
        {
            anim = GetComponentInChildren<Animator>();
            controller = GetComponent<CharacterController>();
            controller.minMoveDistance = 0.0f;

            if (!anim)
            {
                Debug.Log("No animation component in child for the gameobject");
            }

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
        curMoveInput.y -= gravity * Time.deltaTime;
        controller.Move(curMoveInput * Time.deltaTime);

        anim.SetFloat("Forward", move.x);
        anim.SetFloat("Right", move.x);
    }

    public void MovePlayer(InputAction.CallbackContext context)
    {
        move = context.action.ReadValue<Vector2>();
        move.Normalize();

        Vector3 moveVel = new Vector3(move.x, 0, move.y);
        curMoveInput = moveVel * moveSpeed;

        if (controller.isGrounded)
        {
            curMoveInput = transform.TransformDirection(curMoveInput);
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        try
        {
            if (context.action.WasPressedThisFrame())
            {
                if (projectile && projectileSpawnPoint)
                {
                    Rigidbody temp = Instantiate(projectile, projectileSpawnPoint.position, projectileSpawnPoint.rotation);
                    temp.AddForce(projectileSpawnPoint.forward * projectileSpeed, ForceMode.Impulse);

                    Destroy(temp.gameObject, 2.0f);
                }

                throw new UnassignedReferenceException("Fire Pressed");
            }

            if (context.action.WasReleasedThisFrame())
            {
                throw new UnassignedReferenceException("Fire Released");
            }
        }

        catch (UnassignedReferenceException e)
        {
            Debug.Log(e.Message);
        }
    }
}