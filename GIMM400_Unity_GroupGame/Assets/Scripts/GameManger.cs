using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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

    [Header("UI Elements")]
    [SerializeField] private Text timerText;
    [SerializeField] private Text startTimerText;
    [SerializeField] private Text winnerText;
    [SerializeField] private GameObject player1Ready;
    [SerializeField] private GameObject player1NotReady;
    [SerializeField] private GameObject player2Ready;
    [SerializeField] private GameObject player2NotReady;

    [Space(20)]
    [SerializeField] private float gameTimer;
    private float timer;
    private bool gameStarted;
    [SerializeField] private float startCountdown;
    private float startTimer;

    void Start()
    {
        Time.timeScale = 0;

        //UI = Instantiate(UI, transform.position, transform.rotation);

        //uncomment when player ready up bool is made
        /*player1Ready.SetActive(false);
        player1NotReady.SetActive(true);
        player2Ready.SetActive(false);
        player2NotReady.SetActive(true);*/

        timerUI.SetActive(false);
        gameOverUI.SetActive(false);
        pressStartUI.SetActive(true);
        gameStarted = false;
        timer = gameTimer;
        startTimer = startCountdown;
        startTimerText.text = "";

        //set player1/2 to gameobjects

    }

    void Update()
    {
        //uncomment when player ready up bool is made
        /*if(player1.GetComponent < -----player script----->.ready == true)
        {
            player1Ready.SetActive(true);
            player1NotReady.SetActive(false);
        }
        else if (player1.GetComponent < -----player script----->.ready == false)
        {
            player1Ready.SetActive(false);
            player1NotReady.SetActive(true);
        }
        if (player2.GetComponent < -----player script----->.ready == true)
        {
            player2Ready.SetActive(true);
            player2NotReady.SetActive(false);
        }
        else if (player2.GetComponent < -----player script----->.ready == false)
        {
            player2Ready.SetActive(false);
            player2NotReady.SetActive(true);
        }*/

        //uncomment when player ready up bool is made
        //need to put Input.anyKey into player script and have it change a bool to make sure both players are ready
        if (gameStarted == false && Input.anyKey/*player1.GetComponent<-----player script----->.ready == true && player2.GetComponent<-----player script----->.ready == true*/)
        {
            gameStarted = true;
        }
        if (gameStarted == true)
        {
            Time.timeScale = 1;
            startTimer -= Time.deltaTime;
            DisplayStartTime(startTimer);

            if (startTimer <= 0)
            {
                //spawn players/items at this point

                timerUI.SetActive(true);
                pressStartUI.SetActive(false);

                timer -= Time.deltaTime;
                DisplayTime(timer);
                //uncomment when player health is working
                if (timer <= 0 /*|| player1.GetComponent<-----player script----->.health <= 0 || player2.GetComponent<-----player script----->.health <= 0*/)
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
                    //uncomment when player health is working
                    /*else if(player1.health <= 0)
                    {
                        winnerText.text = "Player 2 Wins";
                    }
                    else if(player2.health <= 0)
                    {
                        winnerText.text = "Player 1 Wins";
                    }
                     */
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

    void DisplayStartTime(float timeToDisplay)
    {
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        startTimerText.text = seconds.ToString();
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        timer = gameTimer;
        gameStarted = false;
        pressStartUI.SetActive(true);
        startTimerText.text = "";
        //uncomment if necessary when player ready up bool is made
        /*player1Ready.SetActive(false);
        player1NotReady.SetActive(true);
        player2Ready.SetActive(false);
        player2NotReady.SetActive(true);*/
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}