using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameManager : Singelton<GameManager>
{
    [HideInInspector] public Player playerInput;
    [HideInInspector] public UnityEvent<int> OnLifeValueChanged;

    MouseLook playerLook;
    MouseLook cameraLook;

    public PlayerController controller;
    public Transform spawnPoint;
    public CanvasManager cv;

    public bool drowned = false;
    public int continueCounter = 3;

    private int _health = 4;
    public int maxHealth = 4;

    public int health
    {
        get { return _health; }
        set
        {
            if (controller.godModeActive)
                return;

            _health = value;

            if (_health > maxHealth)
            {
                _health = maxHealth;
            }

            if (_health <= 0)
            {
                Playerdeath();
            }

            OnLifeValueChanged.Invoke(_health);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        playerInput = new Player();
        cameraLook = Camera.main.GetComponent<MouseLook>();
    }

    void Start()
    {
        if (cv == null)
            Debug.Log("cv is null");
        else
            Debug.Log("cv is not null");
        
        health = continueCounter + 1;

        playerInput.Actions.Escape.performed += ctx => OnButtonPress(ctx);
        AddPlayerInput();
    }

    private void LateUpdate()
    {
        if (controller == null && SceneManager.GetActiveScene().name == "Level")
        {
            AddPlayerInput();
        }
    }

    private void OnEnable()
    {
        playerInput.Enable();
    }

    private void OnDisable()
    {
        playerInput.Disable();
    }

    public void OnButtonPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (SceneManager.GetActiveScene().name == "Level")
                SceneManager.LoadScene("MainMenu");
            else
            {
                SceneManager.LoadScene("Level");
                AddPlayerInput();
            }       
        }
    }

    void AddPlayerInput()
    {
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        cameraLook = Camera.main.GetComponent<MouseLook>();
        playerLook = GameObject.FindGameObjectWithTag("Player").GetComponent<MouseLook>();

        if (controller == null || cameraLook == null || playerLook == null)
        {
            AddPlayerInput();
        }

        playerInput.Actions.Move.performed += ctx => controller.MovePlayer(ctx);
        playerInput.Actions.Move.canceled += ctx => controller.MovePlayer(ctx);
        playerInput.Actions.Fire.performed += ctx => controller.Fire(ctx);

        playerInput.Actions.Look.performed += ctx => cameraLook.Look(ctx);
        playerInput.Actions.Look.performed += ctx => playerLook.Look(ctx);
    }

    public void GameOver()
    {
        SceneManager.LoadScene("GameOver");
    }

    public void Victory()
    {
        SceneManager.LoadScene("Victory");
    }

    public void Respawn()
    {
        controller.transform.position = spawnPoint.position;
    }

    public void Playerdeath()
    {
        if (continueCounter > 0)
        {
            SceneManager.LoadScene("Continue");
            Debug.Log(drowned.ToString());

            cv.drownedText.gameObject.SetActive(false);
            cv.killedText.gameObject.SetActive(false);
            cv.finalLifeText.gameObject.SetActive(false);

            if (continueCounter == 1)
                cv.finalLifeText.gameObject.SetActive(true);
            else 
            { 
                if (drowned)
                    cv.drownedText.gameObject.SetActive(true);
                else
                    cv.killedText.gameObject.SetActive(true);
            }
            
            cv.continueCounterText.text = "Remaining: " + continueCounter.ToString();
        }
        else
            SceneManager.LoadScene("GameOver");
    }
}
