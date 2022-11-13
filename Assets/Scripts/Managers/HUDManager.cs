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

    [Header("Text")]
    public Text lifeText;
    public Text scoreText;

    [Header("Bars")]
    public GameObject healthBar;

    void Start()
    {
        if (lifeText)
        {
            //GameManager.instance.OnLifeValueChanged.AddListener((value) => UpdateLifeText(value));
        }

        if (scoreText)
        {
            //GameManager.instance.OnScoreValueChanged.AddListener((value) => UpdateScoreText(value));
        }
    }

    void UpdateLifeText(int value)
    {
        if (lifeText)
        {
            lifeText.text = "LIVES: " + value.ToString();
        }
    }

    void UpdateScoreText(int value)
    {
        if (scoreText)
        {
            scoreText.text = "Score: " + value.ToString();
        }
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
}
