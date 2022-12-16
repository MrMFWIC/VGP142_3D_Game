using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public GameObject PSGreen;
    public GameObject PSYellow;

    void Start()
    {
        
    }

    void Update()
    {
        if (GameManager.Instance.checkpoint)
        {
            PSGreen.SetActive(true);
            PSYellow.SetActive(false);
        }
    }
}
