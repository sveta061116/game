using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Game_2 : MonoBehaviour
{
   
    public static Game_2 instance_2;

    public TextMeshProUGUI winning_text;

    public money money_Script;
   
    public bool playerX = true;

    
    public int[] xo_moves = new int[9];

    
    public bool game_Over = false;

    
    public enum TurnState
    {
        Normal,        
        Choose_Action,  
        Delete_Mode,    
        Forced_Move     
    }

    public TurnState current_State = TurnState.Normal;

   
    public bool forcedX;

    void Start()
    {
        winning_text.text = "";
    }

    void Awake()
    {
        instance_2 = this;
    }

   
    public void End_Move()
    {
        
        playerX = !playerX;

       
        current_State = TurnState.Choose_Action;



        if (money_Script != null)
            money_Script.Hide();
    }

    
    public void Choose_Delete()
    {
        current_State = TurnState.Delete_Mode;
    }
 
    public void Choose_Coin()
    {
        bool v = Random.value > 0.5f;
        forcedX = v;

        current_State = TurnState.Forced_Move;

        if (money_Script != null)
            money_Script.SetCoin(forcedX);

        
    }

    public void CheckWin()
    {
        int[,] winCombos =
        {
        {0,1,2},{3,4,5},{6,7,8},
        {0,3,6},{1,4,7},{2,5,8},
        {0,4,8},{2,4,6}
    };

        for (int i = 0; i < 8; i++)
        {
            int a = winCombos[i, 0];
            int b = winCombos[i, 1];
            int c = winCombos[i, 2];

            if (xo_moves[a] != 0 &&
                xo_moves[a] == xo_moves[b] &&
                xo_moves[a] == xo_moves[c])
            {
                EndGame(xo_moves[a]);
                return;
            }
        }
        bool draw = true;
        foreach (int cell in xo_moves)
        {
            if (cell == 0)
            {
                draw = false;
                break;
            }
        }

        if (draw)
            EndGame(0);

    }
    public void EndGame(int winner)
    {
        
        game_Over = true;

        if (winning_text != null)
        {
            if (winner == 1)
                winning_text.text = "Победили крестики!";
            else if (winner == 2)
                winning_text.text = "Победили нолики!";
            else
                winning_text.text = "Ничья!";
        }

        Invoke("GoToMenu", 1.5f);
    }
    void GoToMenu()
    {
        SceneManager.LoadScene("start");
    }
}
