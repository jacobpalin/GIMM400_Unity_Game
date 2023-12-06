using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManger : MonoBehaviour
{
    [Header("UIs")]
    public GameObject UI;
    [SerializeField] private GameObject timerUI;
    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private GameObject pressStartUI;

    [Header("Players")]
    public GameObject player1;
    public GameObject player2;
    //public GameObject playerManager;

    [Header("UI Elements")]
    [SerializeField] private Text timerText;
    [SerializeField] private Text winnerText;

    [Space(20)]
    [SerializeField] private float gameTimer;
    private float timer;
    private bool gameStarted;

    void Start()
    {
        Time.timeScale = 0;

        timerUI.SetActive(false);
        gameOverUI.SetActive(false);
        pressStartUI.SetActive(true);
        gameStarted = false;
        timer = gameTimer;
    }

    void Update()
    {
        if (gameStarted == false && Input.anyKey)
        {
            gameStarted = true;
        }
        if (gameStarted == true)
        {
            Time.timeScale = 1;

            timerUI.SetActive(true);
            pressStartUI.SetActive(false);

            timer -= Time.deltaTime;
            DisplayTime(timer);

            if (timer <= 0 || player1.GetComponent<PlayerHealth>().currentHealth <= 0 || player2.GetComponent<PlayerHealth>().currentHealth <= 0)
            {
                //game over
                gameOverUI.SetActive(true);
                Time.timeScale = 0;
                timerUI.SetActive(false);

                if (timer <= 0)
                {
                    //sudden death maybe? shrink map border/1hp/no time limit
                    winnerText.text = "Tie";
                }
                else if (player1.GetComponent<PlayerHealth>().currentHealth <= 0)
                {
                    winnerText.text = "Player 2 Wins";
                }
                else if (player2.GetComponent<PlayerHealth>().currentHealth <= 0)
                {
                    winnerText.text = "Player 1 Wins";
                }
            }
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        timer = gameTimer;
        gameStarted = false;
        pressStartUI.SetActive(true);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}