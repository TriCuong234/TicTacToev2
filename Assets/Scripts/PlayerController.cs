
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{


    [SerializeField]
    // Start is called before the first frame update
    private GameObject gameController;
    GameController gameControllerScript;
    // Start is called before the first frame update
    public GameObject gameOverPanel;
    private int player = 1;

    private int player1Point = 0;
    private int player2Point = 0;

    public GameObject gameInfoContainer;

    private Text Player1Timer;
    private Text Player2Timer;

    void Start()
    {
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
        this.player = -this.player;
    }

    public void CellClick(int r, int c)
    {
        gameControllerScript.CheckBoard(r, c, this.player);
        //checkwin
        if (gameControllerScript.BoardLength() > 4)
        {
            if (gameControllerScript.CheckWin(r, c))
            {
                gameOverPanel.GetComponent<GameOverPanelController>().SetActivePanel(this.player);
            }
            if (!gameControllerScript.CheckWin(r, c) && gameControllerScript.BoardLength() == 9)
            {
                gameOverPanel.GetComponent<GameOverPanelController>().SetActivePanel(0);
            }
        }
        StopAllCoroutine();
        ChangePlayer();
        StartCoroutine(PlayerTimer(60));
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

    public void PlayerTimer(int player, int time)
    {
        if (player == 1)
        {
            Player1Timer.text = "00:" + time;

        }
    }

    public IEnumerator PlayerTimer(int time)
    {
        int count = time;
        print("hehe");
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

    public void StartTimer(int time){
        StartCoroutine(PlayerTimer(time));
    }

    public void StopAllCoroutine()
    {
        StopAllCoroutines();
    }
}
