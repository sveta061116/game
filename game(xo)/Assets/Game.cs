using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game instance;

    public bool playerX = true;
    public bool playerNow = true;

    public int[] xo_moves = new int[9];

    public TextMeshProUGUI winning_text;

    public bool game_Over = false;

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        ResetGame();
    }

    public void ResetGame()
    {
        game_Over = false;
        playerNow = true;

        if (winning_text != null)
            winning_text.text = "";

        for (int i = 0; i < xo_moves.Length; i++)
            xo_moves[i] = 0;

        Cellcolor[] cells = FindObjectsByType<Cellcolor>(FindObjectsSortMode.None);

        foreach (Cellcolor cell in cells)
        {
            if (cell == null) continue;

            cell.Clicked_Now = false;

            if (cell.xo != null)
                cell.xo.sprite = null;
        }
    }

    public void PlayerX()
    {
        if (instance == null)
        {
           
            return;
        }

        playerX = true;
        playerNow = true;
        game_Over = false;

        winning_text.text = "";

     
    }

    public void PlayerO()
    {
        playerX = false;
        playerNow = false;
        game_Over = false;

        if (winning_text != null)
            winning_text.text = "";

        Computer_Move();
    }

    public void End_Move()
    {
        playerNow = !playerNow;

        if (!playerNow)
            Computer_Move();
    }

    void Computer_Move()
    {
        if (game_Over) return;

        Cellcolor[] cells = FindObjectsByType<Cellcolor>(FindObjectsSortMode.None);

        foreach (Cellcolor cell in cells)
        {
            if (cell == null) continue;

            if (!cell.Clicked_Now)
            {
                cell.Mark(!playerX);
                break;
            }
        }

        playerNow = true;
    }

    public void Win()
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
        playerNow = false;

        if (winning_text != null)
        {
            if (winner == 1)
                winning_text.text = playerX ? "Ты выиграл!" : "Ты проиграл!";
            else if (winner == 2)
                winning_text.text = !playerX ? "Ты выиграл!" : "Ты проиграл!";
            else
                winning_text.text = "Ничья!";
        }

        manager.variable.RestartSceneAfterDelay(2f);
    }
}