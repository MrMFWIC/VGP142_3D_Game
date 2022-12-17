using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    GameManager gameManager;
    Animator anim;
    WeaponPickup wp;
    CharacterController controller;
    MouseLook ml;
    
    public Camera playerCamera;
    public float moveSpeed = 10;
    public float gravity = 9.81f;
    public float jumpSpeed = 10.0f;
    public float projectileSpeed = 40f;

    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPoint;
    public float projectileForce = 30.0f;

    public bool godModeActive = false;
    public float godModeTimer = 8.0f;

    public bool speedUpActive = false;
    public float speedUpTimer = 8.0f;

    public Transform playerTransform;
    public Transform checkpointSpawn;

    Vector3 curMoveInput;
    Vector3 destination;
    Vector2 move;

    void Start()
    {
        try
        {
            anim = GetComponentInChildren<Animator>();
            controller = GetComponent<CharacterController>();
            wp = GetComponent<WeaponPickup>();
            ml = GetComponent<MouseLook>();

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

        GameManager.Instance.health = GameManager.Instance.maxHealth;

        if (GameManager.Instance.checkpoint)
        {
            playerTransform.position = checkpointSpawn.position;
            Debug.Log("Checkpoint spawn called");
        }
    }

    // Update is called once per frame
    void Update()
    {
        curMoveInput.y -= gravity * Time.deltaTime;
        controller.Move(curMoveInput * Time.deltaTime);

        anim.SetFloat("Forward", move.x);
        anim.SetFloat("Right", move.x);

        if (controller.transform.position.y <= 9.0f)
        {
            GameManager.Instance.drowned = true;
            Debug.Log(GameManager.Instance.drowned.ToString());
            GameManager.Instance.Playerdeath();
        }

        if (speedUpActive)
        {
            moveSpeed = 15;
        }
        else
        {
            moveSpeed = 10;
        }
    }

    private void OnDisable()
    {
        GameManager.Instance.playerInput.Actions.Move.performed -= MovePlayer;
        GameManager.Instance.playerInput.Actions.Move.canceled -= MovePlayer;
        GameManager.Instance.playerInput.Actions.Fire.performed -= Fire;
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
        if (context.action.WasPressedThisFrame())
        {
            if (projectilePrefab && wp.weaponFirePoint && GetComponent<WeaponPickup>().weapon != null)
            {
                Rigidbody temp = Instantiate(projectilePrefab, wp.weaponFirePoint.transform.position, wp.weaponFirePoint.transform.rotation);
                temp.AddForce(wp.weaponFirePoint.transform.forward * projectileForce, ForceMode.Impulse);

                Destroy(temp.gameObject, 2.0f);
            }
        }
    }

    IEnumerator StopGodMode()
    {
        yield return new WaitForSeconds(godModeTimer);

        godModeActive = false;
    }

    IEnumerator StopSpeedUp()
    {
        yield return new WaitForSeconds(speedUpTimer);

        speedUpActive = false;
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("GodMode"))
        {
            if (!godModeActive)
            {
                godModeActive = true;
                Debug.Log("God Mode Activated");
                StartCoroutine(StopGodMode());
            }

            Destroy(hit.gameObject);
        }

        if (hit.gameObject.CompareTag("SpeedUp"))
        {
            if (!speedUpActive)
            {
                speedUpActive = true;
                Debug.Log("Speed Up Activated");
                StartCoroutine(StopSpeedUp());
            }

            Destroy(hit.gameObject);
        }

        if (hit.gameObject.CompareTag("GateKey"))
        {
            GameManager.Instance.gateActive = true;
            Debug.Log("Gate Activated");
            Destroy(hit.gameObject);
        }

        if (hit.gameObject.CompareTag("Melee") && godModeActive == false)
        {
            GameManager.Instance.health--;
            GameManager.Instance.Playerdeath();
        }

        if (hit.gameObject.CompareTag("Projectile"))
        {
            GameManager.Instance.health--;
            GameManager.Instance.Playerdeath();
        }

        if (hit.gameObject.CompareTag("Enemy"))
            GameManager.Instance.health--;

        if (hit.gameObject.CompareTag("Checkpoint"))
        {
            GameManager.Instance.checkpoint = true;
            Debug.Log("Checkpoint reached");
        }
    }
}