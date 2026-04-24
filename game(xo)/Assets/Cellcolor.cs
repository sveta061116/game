using UnityEngine;
using UnityEngine.UI;

public class Cellcolor : MonoBehaviour
{
    public Image xo;
    public Sprite cross;
    public Sprite zero;

    public bool Clicked_Now = false;
    public int number;

    public void OnClick()
    {
        if (Game.instance == null) return;
        if (Game.instance.game_Over) return;
        if (Clicked_Now) return;

        Mark(Game.instance.playerX);
        Game.instance.End_Move();
    }

    public void Mark(bool x_now)
    {
        if (Clicked_Now) return;

        if (xo == null)
        {
 
            return;
        }

        xo.sprite = x_now ? cross : zero;
        Clicked_Now = true;

        if (Game.instance != null)
        {
            Game.instance.xo_moves[number] = x_now ? 1 : 2;
            Game.instance.Win();
        }
    }
}