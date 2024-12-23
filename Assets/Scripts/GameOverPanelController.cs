using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameOverPanelController : MonoBehaviour
{
    // Start is called before the first frame update

    public Button exitBtn;
    public Button continueBtn;
    public Text player1Point;
    public Text player2Point;
    public GameObject gameOverPanel;
    [SerializeField]
    private GameController gameControllerScript;
    
    [SerializeField]
    private PlayerController playerControllerScript;
    
    private int PlayerWin; 

    private bool ThisPanelActive = false;
    void Start()
    {
        exitBtn.onClick.AddListener(OnClickExitBtn);
        continueBtn.onClick.AddListener(OnClickContinueBtn);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnClickContinueBtn()
    {
        this.ThisPanelActive = false;
        DeActive();   
        gameControllerScript.OriginBoard();
        if (PlayerWin == 1){
            playerControllerScript.IncPlayerPoint(PlayerWin);
            player1Point.text = playerControllerScript.GetPlayerPoint(this.PlayerWin).ToString();
            
            playerControllerScript.StopAllCoroutine();
            if (playerControllerScript.GetIsPvp()) playerControllerScript.ChangePlayer();
            playerControllerScript.StartTimer(60);
        }
        if (PlayerWin == -1){
            playerControllerScript.IncPlayerPoint(PlayerWin);
            player2Point.text = playerControllerScript.GetPlayerPoint(this.PlayerWin).ToString();
            playerControllerScript.StopAllCoroutine();
            if (playerControllerScript.GetIsPvp()) playerControllerScript.ChangePlayer();
            playerControllerScript.StartTimer(60);
        }
        if (PlayerWin == 0){
            playerControllerScript.StopAllCoroutine();
            if (playerControllerScript.GetIsPvp()) playerControllerScript.ChangePlayer();
            playerControllerScript.StartTimer(60);
        }
    }
    void OnClickExitBtn()
    {
        print("ext");
    }

    public void SetActivePanel(int player)
    {
        this.gameObject.SetActive(true);
        this.ThisPanelActive = true;
        playerControllerScript.StopAllCoroutine();
        //gameOverPanel.GetComponent<Text>().text = content;
        if (player == 1){
            this.gameOverPanel.GetComponentInChildren<Text>().text = "Player1 Win!";
            this.PlayerWin = 1;
        }
        if (player == -1){
            this.gameOverPanel.GetComponentInChildren<Text>().text = "Player2 Win!";
            this.PlayerWin = -1;
        }
        if (player == 0){
            this.gameOverPanel.GetComponentInChildren<Text>().text = "Draw!";
            this.PlayerWin = 0;
        }
        
        
    }

    void DeActive()
    {
        this.gameObject.SetActive(false);
    }
    public bool GetThisPanelActive(){
        return this.ThisPanelActive;
    }
}
