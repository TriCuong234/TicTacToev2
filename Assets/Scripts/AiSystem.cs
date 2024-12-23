using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiSystem : MonoBehaviour
{
    private int mode = 1;
    // Start is called before the first frame update
    [SerializeField]

    private GameController gameController;
    [SerializeField]
    private PlayerController playerController;
    [SerializeField]
    private GameOverPanelController gameOverPanel;
    private List<Vector2> vectorList = new List<Vector2>();
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AITurn()
    {
        MyArrayNow();
        switch (this.mode)
        {
            case 1:
                {
                    int[,] board = gameController.getBoard();
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (board[i, j] == 0)
                            {
                                board[i, j] = -1;
                                if (gameController.CheckWin(i, j,-1))
                                {
                                    
                                    gameOverPanel.GetComponent<GameOverPanelController>().SetActivePanel(-1);
                                    playerController.AiToPlayer();
                                    return; // Nếu AI thắng, kết thúc lượt
                                }
                                board[i, j] = 0; // Hoàn tác nước đi nếu không thắng
                            }
                        }
                    }
                    // Nếu không thể thắng, AI đánh ngẫu nhiên
                    Vector2 temp = vectorList[Random.Range(0, vectorList.Count)];
                    board[(int)temp.x, (int)temp.y] = -1;
                    ChangeBtnAiCheck((int)temp.x, (int)temp.y);
                    playerController.AiToPlayer();
                    vectorList.Clear();
                    break;
                }

        }
    }
    void ChangeBtnAiCheck(int r, int c)
    {
        // Add more checks if necessary
        string temp1 = r.ToString();
        string temp2 = c.ToString();

        string name = temp1 + temp2;

        GameObject btn = GameObject.FindWithTag(name);
        if (btn == null)
        {
            Debug.LogError("Button not found with tag: " + name);
            return;
        }

        CellSingleton btnScript = btn.GetComponent<CellSingleton>();
        if (btnScript == null)
        {
            Debug.LogError("CellSingleton component not found on: " + name);
            return;
        }
        btnScript.ChangeBtnAiPick(r, c);
        return;
        // Add more null checks if necessary
    }

    public void MyArrayNow()
    {
        int[,] board = gameController.getBoard();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == 0)
                {
                    vectorList.Add(new Vector2(i, j));
                }
            }
        }
    }
}
