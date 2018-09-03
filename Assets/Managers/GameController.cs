using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameController : MonoBehaviour {

    bool playerDead = false;

    //Timer
    public Text timerLabel;
    public float timer = 120;
    float timeLeft;
    bool gameStarted = false;

    //Countdown
    public Image[] countImg;
    bool countDown = true;
    float countAmount = 4;

    //Reset
    public GameObject[] players = new GameObject[2];
    Death playerOneDeath;
    Death playerTwoDeath;
    Vector2 playerOneTransform;
    Vector2 playerTwoTransform;

    //Scores
    int playerOneScore = 0;
    int playerTwoScore = 0;
    public Text playerOneScoreText;
    public Text playerTwoScoreText;

    //Win Screen
    public GameObject winCan;
    public Image[] winImg;

    //Pause Screen
    public Image[] pauseImg;
    bool isPaused = false;
    Vector2 playerOneStore;
    Vector2 playerTwoStore;

    //Start Screen


    // Use this for initialization
    void Start ()
    {

        //Timer
        timeLeft = timer;

        //Reset
        playerOneTransform = players[0].transform.position;
        playerTwoTransform = players[1].transform.position;
        playerOneDeath = players[0].GetComponent<Death>();
        playerTwoDeath = players[1].GetComponent<Death>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        Timer();        
        CountDown();
        CheckDeath();
        ScoreCheck();

        //Pause Menu
        playerOneStore = players[0].transform.position;
        playerTwoStore = players[1].transform.position;

        if (Input.GetKey(KeyCode.Joystick1Button7) && !isPaused || Input.GetKey(KeyCode.Joystick2Button7) && !isPaused)
        {
            gameStarted = false;
            isPaused = true;
        }

        if (isPaused)
        {
            pauseImg[0].enabled = true;
            pauseImg[1].enabled = true;

            players[0].transform.position = playerOneStore;
            players[1].transform.position = playerTwoStore;

            if (Input.GetButton("P1_Defence") || Input.GetButton("P2_Defence"))
            {
                SceneManager.LoadScene(0);
            }

            if (Input.GetButton("P1_Accelerate") || Input.GetButton("P2_Accelerate"))
            {
                Application.Quit();
            }

            if (Input.GetKey(KeyCode.Joystick2Button7) || Input.GetKey(KeyCode.Joystick1Button7))
            {
                isPaused = false;
            }

        }



        if (!isPaused)
        {
            pauseImg[0].enabled = false;
            pauseImg[1].enabled = false;
            gameStarted = true;
            players[0].transform.position = players[0].transform.position;
            players[1].transform.position = players[1].transform.position;
        }

        if (!countDown)
        {
            countAmount = 4;
        }
    }

    public void ScoreCheck()
    {
        string pOneScore = playerOneScore.ToString();
        string pTwoScore = playerTwoScore.ToString();
        playerOneScoreText.text = pOneScore;
        playerTwoScoreText.text = pTwoScore;
    }

    public void Timer()
    {
        if (gameStarted)
        {
            timeLeft -= Time.deltaTime;
            timerLabel.text = string.Format("{0:00}", timeLeft);

            if (timeLeft <= 0)
            {
                timeLeft = 0;
                WinScreen();
            }
        }
    }

    public void WinScreen()
    {
        players[0].GetComponent<PlayerController>().enabled = false;
        players[1].GetComponent<PlayerController>().enabled = false;

        

        if(playerOneScore < playerTwoScore)
        {
            winCan.SetActive(true);
            winImg[1].enabled = true;
        }

        if(playerTwoScore < playerOneScore)
        {
            winCan.SetActive(true);
            winImg[0].enabled = true;
        }

        if(playerOneScore == playerTwoScore)
        {
            winCan.SetActive(true);
            winImg[2].enabled = true;
        }

        Restart();
        Quit();
    }

    public void Restart()
    {
        if (Input.GetButton("P1_Defence") && timeLeft == 0 || Input.GetButton("P2_Defence") && timeLeft == 0)
        {
            SceneManager.LoadScene(0);
        }
        
    }

    public void Quit()
    {
        if(Input.GetButton("P1_Accelerate") && timeLeft == 0 || Input.GetButton("P2_Accelerate") && timeLeft == 0)
        {
            Application.Quit();
        }
    }

    public void WhoDied(float who)
    {
        if (who == 1)
        {
            playerTwoScore += 1;
            playerDead = true;
        }

        if (who == 2)
        {
            playerOneScore += 1;
            playerDead = true;
        }
    }

    public void CheckDeath()
    {
        if (playerDead)
        {
            playerOneDeath.isDead = false;
            playerTwoDeath.isDead = false;
            countDown = true;
        }
        else
        {
            players[0].transform.position = players[0].transform.position;
            players[1].transform.position = players[1].transform.position;
        }
    }

    public void CountDown()
    {
        if (countDown)
        {
            gameStarted = false;
            countAmount -= Time.deltaTime;
            players[0].transform.position = playerOneTransform;
            players[1].transform.position = playerTwoTransform;
                        
            if(countAmount<4 && countAmount > 3)
            {
                countImg[0].enabled = true;
            }

            if(countAmount<3 && countAmount > 2)
            {
                countImg[0].enabled = false;
                countImg[1].enabled = true;
            }

            if(countAmount<2 && countAmount > 1)
            {
                countImg[1].enabled = false;
                countImg[2].enabled = true;
            }

            if(countAmount<1 && countAmount > 0)
            {
                countImg[2].enabled = false;
                countImg[3].enabled = true;
            }

            if (countAmount < 0)
            {
                countImg[3].enabled = false;
            }

            //End Countdown
            if(countAmount <= 0)
            {
                playerDead = false;
                gameStarted = true;
                countDown = false;
            }
        }
    }
}
