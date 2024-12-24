
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


    [SerializeField]
    // Start is called before the first frame update
    private GameObject gameController;
    [SerializeField]
    // Start is called before the first frame update
    private AiSystem ai;
    GameController gameControllerScript;
    // Start is called before the first frame update
    public GameObject gameOverPanel;
    private int player = 1;

    private int player1Point = 0;
    private int player2Point = 0;

    private bool isPvP;


    public GameObject gameInfoContainer;

    private Text Player1Timer;
    private Text Player2Timer;

    void Start()
    {
        this.isPvP = PlayerPrefs.GetInt("PvP") == 1 ? true : false;
        gameControllerScript = gameController.GetComponent<GameController>();
        Player1Timer = GameObject.Find("Player1Timer").GetComponent<Text>();
        Player2Timer = GameObject.Find("Player2Timer").GetComponent<Text>();
        StartCoroutine(PlayerTimer(60));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public int PlayerNow()
    {
        return this.player;
    }

    public void ChangePlayer()
    {
        // neu nguoi voi nguoi

        if (isPvP)
        {
            this.player = -this.player;
            StopAllCoroutines();
            StartCoroutine(PlayerTimer(60));
        }
        else
        {
            if (this.player == 1)
            {
                StopAllCoroutines();
                StartCoroutine(PlayerTimer(60));
                this.player = -1;
                // Call AI turn
                ai.AITurn();
                return;
            }
            this.player = 1;
            return;
        }
        //AI turn lam gi di chu
    }

    public void CellClick(int r, int c)
    {

        gameControllerScript.CheckBoard(r, c, this.player);
        //checkwin
        
        //return (r,c);
    }
    public void IncPlayerPoint(int player)
    {
        if (player == 1)
        {
            this.player1Point++;
            return;
        }
        this.player2Point++;
    }

    public int GetPlayerPoint(int player)
    {
        if (player == 1)
        {
            return this.player1Point;
        }
        return this.player2Point;
    }


    public IEnumerator PlayerTimer(int time)
    {
        int count = time;
        if (player == 1)
        {
            Player2Timer.text = "";
            while (count >= 0)
            {

                if (count == 60)
                    Player1Timer.text = "01:00";
                if (count < 60 && count > 9)
                    Player1Timer.text = "00:" + count;
                if (count < 10)
                    Player1Timer.text = "00:0" + count;
                count--;
                yield return new WaitForSeconds(1);
            }
            if (count == -1)
            {
                gameOverPanel.GetComponent<GameOverPanelController>().SetActivePanel(-this.player);
                StopAllCoroutine();
            }
        }

        else
        {
            Player1Timer.text = "";
            while (count >= 0)
            {

                if (count == 60)
                    Player2Timer.text = "01:00";
                if (count < 60 && count > 9)
                    Player2Timer.text = "00:" + count;
                if (count < 10)
                    Player2Timer.text = "00:0" + count;
                count--;
                yield return new WaitForSeconds(1);
            }

            if (count == -1)
            {
                gameOverPanel.GetComponent<GameOverPanelController>().SetActivePanel(-this.player);
                StopAllCoroutine();

            }
        }

    }

    public void StartTimer(int time)
    {
        StartCoroutine(PlayerTimer(time));
    }

    public void StopAllCoroutine()
    {
        StopAllCoroutines();
    }

    public bool GetIsPvp()
    {
        return this.isPvP;
    }

    public void AiToPlayer()
    {
        this.player = 1;
    }

    public int PlayerWin(int r, int c)
    {
        if (gameControllerScript.BoardLength() > 4)
        {
            if (gameControllerScript.CheckWin(r, c, this.player))
            {

                gameOverPanel.GetComponent<GameOverPanelController>().SetActivePanel(this.player);
                StopAllCoroutines();
                return 1;
            }
            if (!gameControllerScript.CheckWin(r, c, this.player) && gameControllerScript.BoardLength() == 9)
            {
                StopAllCoroutines();
                gameOverPanel.GetComponent<GameOverPanelController>().SetActivePanel(0);
                return -1;
            }
        }
        return 0;
    }

    public void ShowOverPanel(){
        gameOverPanel.GetComponent<GameOverPanelController>().SetActivePanel(this.player);
        return;
    }
}
