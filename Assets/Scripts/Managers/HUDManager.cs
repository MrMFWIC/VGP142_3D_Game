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
    public Image healthBar;

    void Start()
    {
        GameManager.Instance.OnLifeValueChanged.AddListener((value) => UpdateHealthBarSprite(value));
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
            healthBar.fillAmount = 1.0f;

        if (value == 3)
            healthBar.fillAmount = 0.715f;

        if (value == 2)
            healthBar.fillAmount = 0.5f;

        if (value == 1)
            healthBar.fillAmount = 0.29f;

        if (value == 0)
            healthBar.fillAmount = 0.0f;
    }
}
