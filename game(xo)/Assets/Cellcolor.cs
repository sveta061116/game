using UnityEngine;
using UnityEngine.UI;

public class Cellcolor : MonoBehaviour
{
    public Image xo; //Спрайт клетки
    public Sprite cross;
    public Sprite zero;

    public bool Clicked_Now = false;//Нажата ли клетка
    public int number; //Номер клетки

    public void OnClick() //Нажатие игрока на клетку
    {
        if (Game.instance == null) return;
        if (Game.instance.game_Over) return;
        if (Clicked_Now) return;

        Mark(Game.instance.playerX);
        Game.instance.End_Move();
    }

    public void Mark(bool x_now)//Появление фигуры в клетки, запись в массив ходов
    {
        if (Clicked_Now) return;

        if (xo == null)
        {
            return;
        }

        xo.sprite = x_now ? cross : zero;
        Clicked_Now = true;

        Game.instance.xo_moves[number] = x_now ? 1 : 2;
        Game.instance.Win();
    }
}