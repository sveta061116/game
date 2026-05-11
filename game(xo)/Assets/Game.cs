using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class Game : MonoBehaviour
{
    public static Game instance; //Глобальный доступ к переменной класса

    public bool playerX = true; //Выбор фигуры игрока
    public bool playerNow = true; //Ход игрока

    public int[] xo_moves = new int[9]; //Массив ходов

    public TextMeshProUGUI winning_text; //Текст о выигрыше

    public bool game_Over = false; //Окончание игры

    void Awake()
    {
        instance = this;
    }

    void Start() 
    {
        ResetGame(); 
    }

    public void ResetGame() //Обнуление прогресса
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

    public void PlayerX() //Флаг того, что игрок выбрал играть за X
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

    public void PlayerO()//Флаг того, что игрок выбрал играть за 0
    {
        playerX = false;
        playerNow = false;
        game_Over = false;

        if (winning_text != null)
            winning_text.text = "";

        Computer_Move();
    }

    public void End_Move() //Передача информации о конце хода
    {
        playerNow = !playerNow;

        if (!playerNow)
            Computer_Move();
    }

    void Computer_Move() //Ход ПК
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

    public void Win() //Проверка выигрышной комбинации
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

    public void EndGame(int winner) //Вывод оповещения о победе
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