using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManger : MonoBehaviour
{
    public GameObject UI;

    [SerializeField] private float gameTimer;
    private float timer;
    [SerializeField] private Text timerText;

    [SerializeField] private GameObject gameOverUI;
    [SerializeField] private Text winnerText;
    public GameObject player1;
    public GameObject player2;

    // Start is called before the first frame update
    void Start()
    {
        //UI = Instantiate(UI, transform.position, transform.rotation);
        gameOverUI.gameObject.SetActive(false);
        timer = gameTimer;
        DisplayTime(timer);
        //set player1/2 to gameobjects
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        //timerText.text = "Time: " + timer;
        DisplayTime(timer);

        if (timer <= 0 /*|| player1.health <= 0 || player2.health <= 0*/)
        {
            //game over
            gameOverUI.SetActive(true);
            Time.timeScale = 0;
            timerText.enabled = false;

            if(timer <= 0)
            {
                //sudden death maybe? shrink map border/1hp/no time limit
                winnerText.text = "Tie";
            }
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

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Main Menu");
    }

    public void RestartButton()
    {
        Time.timeScale = 1;
        timer = gameTimer;
        timerText.enabled = true;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}