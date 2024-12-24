using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AiSystem : MonoBehaviour
{
    private int mode;
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

    void Update()
    {

    }

    public void AITurn()
    {
        MyArrayNow();
        switch (this.mode)
        {
            case 0:
                EasyMode();
                break;
            case 1:
                MediumMode();
                break;
            case 2:
                HardMode();
                break;
        }
    }

    void EasyMode()
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
    }

    void MediumMode()
    {
        int[,] board = gameController.getBoard();
        // Kiểm tra nếu có thể thắng hoặc chặn người chơi thắng
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
                        return;
                    }
                    board[i, j] = 0;

                    board[i, j] = 1;
                    if (gameController.CheckWin(i, j, 1))
                    {
                        board[i, j] = -1; // Chặn người chơi
                        ChangeBtnAiCheck(i, j);
                        playerController.AiToPlayer();
                        vectorList.Clear();
                        return;
                    }
                    board[i, j] = 0;
                }
            }
        }
        // Nếu không thể thắng hoặc chặn, đánh ngẫu nhiên
        Vector2 temp = vectorList[Random.Range(0, vectorList.Count)];
        board[(int)temp.x, (int)temp.y] = -1;
        ChangeBtnAiCheck((int)temp.x, (int)temp.y);
        playerController.AiToPlayer();
        vectorList.Clear();
    }

    void HardMode()
    {

        int[,] board = gameController.getBoard();
        int bestScore = int.MinValue;
        Vector2 bestMove = Vector2.zero;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (board[i, j] == 0)
                {
                    board[i, j] = -1; // AI move
                    int score = MinimaxNonRecursive(board);
                    board[i, j] = 0; // Undo move
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestMove = new Vector2(i, j);
                    }
                }
            }
        }
        board[(int)bestMove.x, (int)bestMove.y] = -1;
        ChangeBtnAiCheck((int)bestMove.x, (int)bestMove.y);
        if (gameController.CheckWin2(-1))
        {
            gameOverPanel.GetComponent<GameOverPanelController>().SetActivePanel(-1);
        }
        else if (IsBoardFull(gameController.getBoard()))
        {
            gameOverPanel.GetComponent<GameOverPanelController>().SetActivePanel(0); // Hòa
        }
        else
        {
            playerController.AiToPlayer();

        }

    }



    void ChangeBtnAiCheck(int r, int c)
    {
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

    public int MinimaxNonRecursive(int[,] board)
    {
        Stack<Node> stack = new Stack<Node>();
        stack.Push(new Node(board, 0, true));

        int bestVal = int.MinValue;

        while (stack.Count > 0)
        {
            Node currentNode = stack.Pop();
            int[,] currentBoard = currentNode.Board;
            int depth = currentNode.Depth;
            bool isMaximizingPlayer = currentNode.IsMaximizingPlayer;

            if (gameController.CheckWin2(-1))
                bestVal = Mathf.Max(bestVal, 10 - depth);
            else if (gameController.CheckWin2(1))
                bestVal = Mathf.Max(bestVal, depth - 10);
            else if (IsBoardFull(currentBoard))
                bestVal = Mathf.Max(bestVal, 0);
            else
            {
                if (isMaximizingPlayer)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (currentBoard[i, j] == 0)
                            {
                                int[,] newBoard = (int[,])currentBoard.Clone();
                                newBoard[i, j] = -1; // AI move
                                stack.Push(new Node(newBoard, depth + 1, false));
                            }
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            if (currentBoard[i, j] == 0)
                            {
                                int[,] newBoard = (int[,])currentBoard.Clone();
                                newBoard[i, j] = 1; // Player move
                                stack.Push(new Node(newBoard, depth + 1, true));
                            }
                        }
                    }
                }
            }
        }
        return bestVal;
    }

    public class Node
    {
        public int[,] Board { get; }
        public int Depth { get; }
        public bool IsMaximizingPlayer { get; }

        public Node(int[,] board, int depth, bool isMaximizingPlayer)
        {
            Board = board;
            Depth = depth;
            IsMaximizingPlayer = isMaximizingPlayer;
        }
    }


    int CheckStateGame(int player)
    {
        if (gameController.CheckWin2(player))
            return player;
        return 0;
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
