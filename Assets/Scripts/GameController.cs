using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    // Start is called before the first frame update
    private int[,] board = new int[3,3];
    [SerializeField]
    private Button btn00;
    [SerializeField]
    private Button btn01;
    [SerializeField]
    private Button btn02;
    [SerializeField]
    private Button btn10;
    [SerializeField]
    private Button btn11;
    [SerializeField]
    private Button btn12;
    [SerializeField]
    private Button btn20;
    [SerializeField]
    private Button btn21;
    [SerializeField]
    private Button btn22;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OriginBoard(){
        int i,j = 0;
        for (i = 0; i<3;i++){
            for (j=0; j<3; j++){
                board[i,j] = 0;
            }
        }

        btn00.GetComponent<CellSingleton>().ResetAllChanged();
        btn01.GetComponent<CellSingleton>().ResetAllChanged();
        btn02.GetComponent<CellSingleton>().ResetAllChanged();
        btn10.GetComponent<CellSingleton>().ResetAllChanged();
        btn11.GetComponent<CellSingleton>().ResetAllChanged();
        btn12.GetComponent<CellSingleton>().ResetAllChanged();
        btn20.GetComponent<CellSingleton>().ResetAllChanged();
        btn21.GetComponent<CellSingleton>().ResetAllChanged();
        btn22.GetComponent<CellSingleton>().ResetAllChanged();
    }

    public void ShowBoard(){
        int i,j = 0;
        for (i = 0; i<3;i++){
            for (j=0; j<3; j++){
                print(i+" "+ j+ ":"+board[i,j]);
            }
        }
    }

    public void CheckBoard(int r, int c, int player){
        if (player == 1){
            this.board[r,c] = 1;
            return;
        }
        this.board[r,c] = -1;
    }

    public int BoardLength(){
        int count = 0;
        for (int i = 0; i<3;i++){
            for (int j=0; j<3; j++){
                if (board[i,j] != 0){
                    count++;
                }
            }
        }
        return count;
    }

    public bool CheckWin(int r, int c){
        // 
        if (board[0,c] == board[1,c] && board[1,c] == board[2,c])
        return true;
        if (board[r,0] == board[r,1] && board[r,1] == board[r,2])
        return true;
        if (board[0,0] == board[1,1] && board[1,1] == board[2,2])
        return true;
        if (board[0,2] == board[1,1] && board[1,1] == board[2,0])
        return true;
        return false;
    }
}
