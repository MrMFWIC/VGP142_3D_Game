using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public PlayerController curPlayer;

    [Header("HUDObject")]
    public GameObject HUD;

    [Header("Bars")]
    public GameObject healthBar;

    void Start()
    {
        GameManager.instance.OnLifeValueChanged.AddListener((value) => UpdateHealthBarSprite(value));
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().name == "Level" && Time.timeScale == 1)
        {
            HUD.SetActive(true);
        }
        else
        {
            HUD.SetActive(false);
        }
    }

    public void UpdateHealthBarSprite(int value)
    {
        if (value == 4)
            healthBar.GetComponent<SpriteRenderer>().sprite.name = "HealthBar4";

        if (value == 3)
            healthBar.GetComponent<SpriteRenderer>().sprite.name = "HealthBar3";
        
        if (value == 2)
            healthBar.GetComponent<SpriteRenderer>().sprite.name = "HealthBar2";
        
        if (value == 1)
            healthBar.GetComponent<SpriteRenderer>().sprite.name = "HealthBar1";
        
        if (value == 0)
            healthBar.GetComponent<SpriteRenderer>().sprite.name = "HealthBar0";
    }
}
