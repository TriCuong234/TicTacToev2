using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiSystem : MonoBehaviour
{
    private int mode;
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
        this.mode = PlayerPrefs.GetInt("Mode");
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
            case 0:
                {
                    int[,] board = gameController.getBoard();
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (board[i, j] == 0)
                            {
                                board[i, j] = -1;
                                if (gameController.CheckWin(i, j, -1))
                                {
                                    ChangeBtnAiCheck(i, j);
                                    gameOverPanel.GetComponent<GameOverPanelController>().SetActivePanel(-1);
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
            case 1:
                print("hehe");
                break;
            case 2:
                {
                    int[,] board = gameController.getBoard();
                    int bestScore = int.MinValue;
                    Vector2 bestMove = Vector2.zero; for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (board[i, j] == 0)
                            {
                                board[i, j] = -1; // AI move 
                                int score = Minimax(board, i, j, 0, false);
                                board[i, j] = 0; // Undo move 
                                if (score > bestScore) { bestScore = score; bestMove = new Vector2(i, j); }
                            }
                        }
                    } // Thực hiện nước đi tốt nhất 
                    board[(int)bestMove.x, (int)bestMove.y] = -1;
                    ChangeBtnAiCheck((int)bestMove.x, (int)bestMove.y);
                    if (gameController.CheckWin((int)bestMove.x, (int)bestMove.y, -1)) { gameOverPanel.GetComponent<GameOverPanelController>().SetActivePanel(-1); } else { playerController.AiToPlayer(); }
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
        btnScript.ChangeBtnOnClick(r, c);
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


    int Minimax(int[,] board,int r, int c, int depth, bool isMaximizing)
    {
        if (gameController.CheckWin(r, c, -1)) // AI thắng
            return 1;
        if (gameController.CheckWin(r, c, 1)) // Người chơi thắng
            return -1;
        if (IsBoardFull(board)) // Hòa
            return 0;

        if (isMaximizing)
        {
            int maxEval = int.MinValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                    {
                        board[i, j] = -1; // AI move
                        int eval = Minimax(board,i,j, depth + 1, false);
                        board[i, j] = 0; // Undo move
                        maxEval = Mathf.Max(eval, maxEval);
                    }
                }
            }
            return maxEval;
        }
        else
        {
            int minEval = int.MaxValue;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == 0)
                    {
                        board[i, j] = 1; // Player move
                        int eval = Minimax(board,i,j, depth + 1, true);
                        board[i, j] = 0; // Undo move
                        minEval = Mathf.Min(eval, minEval);
                    }
                }
            }
            return minEval;
        }
    }
    bool IsBoardFull(int[,] board)
    {
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == 0)
                    return false;
            }
        }
        return true;
    }



}
