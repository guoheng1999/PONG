using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameManager : MonoBehaviour {

    private static GameManager _instance;
    public static  GameManager Instance{
        get
        {
            return _instance;
        }
    }

    private readonly int _FULL_SCORE = 12;


    //找到游戏的物体
    private GameObject pauseButton;
    private GameObject continueButton;
    private GameObject ball;
    private GameObject calculationPanel;
    private GameObject winerTextobj;

    //游戏世界的四面墙
    private BoxCollider2D rightWall;
	private BoxCollider2D leftWall;
	private BoxCollider2D topWall;
	private BoxCollider2D downWall;


    //玩家1，2
	public Transform player1;
	public Transform player2;
    
    
    //分值
    private int score1;
    private int score2;

    //计分板
    public Text score1Text;
    public Text score2Text;

    //结算面板
    public Text winnerText;

    //游戏暂停开始标志
    private bool gameFlag = true;

    //游戏控制按钮
    public Button controButton;

    //游戏结束标志
    private bool gameIsRun = true;

    AudioSource audio;
    void Awake()
    {
        _instance = this;
    }

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        //找到游戏中的物体
        pauseButton = GameObject.Find("PauseButton");
        continueButton =GameObject.Find("ContinueButton");
        ball = GameObject.Find("ball");
        calculationPanel = GameObject.Find("CalculationPanel");
        winerTextobj = GameObject.Find("WinnerText");

        calculationPanel.SetActive(false);
        continueButton.SetActive(false);
        ResetWall();
		ResetPlayer();
	}
	
	// Update is called once per frame
	void Update () {
        CheckGame();
	}

	void ResetWall(){
		rightWall = transform.Find("rightWall").GetComponent<BoxCollider2D>();
		leftWall = transform.Find("leftWall").GetComponent<BoxCollider2D>();
		topWall = transform.Find("topWall").GetComponent<BoxCollider2D>();// x = screen.width/2    y = screen.height
		downWall = transform.Find("downWall").GetComponent<BoxCollider2D>();

		Vector3 tempPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width,Screen.height));
		
		topWall.transform.position = new Vector3(0,tempPosition.y+0.5f,0);
		topWall.size = new Vector2(tempPosition.x*2,1);
		
		downWall.transform.position = new Vector3(0,-tempPosition.y-0.5f,0);
		downWall.size = new Vector2(tempPosition.x*2,1);

		rightWall.transform.position = new Vector3(tempPosition.x+0.5f,0,0);
		rightWall.size = new Vector2(1,tempPosition.y*2);
		
		leftWall.transform.position = new Vector3(-tempPosition.x-0.5f,0,0);
		leftWall.size = new Vector2(1,tempPosition.y*2);
		
	}

	/*
	设置预制玩家的位置
	*/
	void ResetPlayer()
	{

		Vector3 player1Position = Camera.main.ScreenToWorldPoint(new Vector3(50,Screen.height/2,0));
		player1Position.z = 0;
		player1.position = player1Position;
		
		Vector3 player2Position = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width-50,Screen.height/2,0));
		player2Position.z = 0;
		player2.position = player2Position; 

	}

    public void ChangeScore(string  wallName)
    {
        if(wallName == "rightWall")
        {
            score1++;
        }
        else if(wallName == "leftWall")
        {
            score2++;
        }

        score1Text.text = score1.ToString();
        score2Text.text = score2.ToString();
    }

    public  void GameReset()
    {
        score1 = score2 = 0;
        score1Text.text = score1.ToString();
        score2Text.text = score2.ToString();
        ResetPlayer();
        ball.SendMessage("Reset");
        gameFlag = true;
        audio.UnPause();
        gameIsRun = true;
        calculationPanel.SetActive(false);
    }
    public void GameContinue()
    {
        if(gameFlag == false && gameIsRun)
        {
            ball.SendMessage("BallStart");
            player1.SendMessage("PlayerStart");
            player2.SendMessage("PlayerStart");
            pauseButton.SetActive(true);
            continueButton.SetActive(false);
            gameFlag = true;
            audio.UnPause();
        }

    }
    public void GamePause()
    {
        if (gameFlag == true && gameIsRun)
        {
            ball.SendMessage("BallStop");
            player1.SendMessage("PlayerStop");
            player2.SendMessage("PlayerStop");
            pauseButton.SetActive(false);
            continueButton.SetActive(true);
            gameFlag = false;
            audio.Pause();
        }
    }

    void CheckGame()
    {

        if (gameIsRun && (score1 >= _FULL_SCORE || score2 >= _FULL_SCORE)) 
        {
            if (score1 == _FULL_SCORE)
            {
                winnerText = winerTextobj.GetComponent<Text>();
                winnerText.text = "红方胜利！";
            }
            else
            {
                winnerText = winerTextobj.GetComponent<Text>();
                winnerText.text = "蓝方胜利！";
            }
            GamePause();
            audio.UnPause();
            gameIsRun = !gameIsRun;
            calculationPanel.SetActive(true);
        }
    }
}
