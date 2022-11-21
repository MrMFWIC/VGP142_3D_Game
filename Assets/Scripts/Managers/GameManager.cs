using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.Events;

public class GameManager : Singelton<GameManager>
{
    static GameManager _instance = null;

    public static GameManager instance
    {
        get { return _instance; }
        set { _instance = value; }
    }

    [HideInInspector] public Player playerInput;
    [HideInInspector] public UnityEvent<int> OnLifeValueChanged;

    MouseLook playerLook;
    MouseLook cameraLook;

    public PlayerController controller;
    public Transform spawnPoint;

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
                GameOver();
            }

            OnLifeValueChanged.Invoke(_health);
        }
    }

    protected override void Awake()
    {
        base.Awake();
        playerInput = new Player();
        cameraLook = Camera.main.GetComponent<MouseLook>();
            
        if (instance)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
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
}
