using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerManager : MonoBehaviour
{
    public static bool gameOver;
    public GameObject gameOverPanel;

    public static bool isGameStarted;
    public GameObject startText;

    public static int coinNumber;
    public Text coinText;

    void Start()
    {
        gameOver = false;
        Time.timeScale = 1;
        isGameStarted = false;
        coinNumber = 0;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver)
        {
            Time.timeScale = 0;
            gameOverPanel.SetActive(true);
        }

        coinText.text = "Coins : " + coinNumber;

        if (Input.anyKeyDown)
        {
            isGameStarted = true;
            Destroy(startText);
        }
    }
}
