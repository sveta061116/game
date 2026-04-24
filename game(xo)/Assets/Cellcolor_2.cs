using UnityEngine;
using UnityEngine.UI;

public class Cellcolor_2 : MonoBehaviour
{
    public Image xo;     
    public Sprite cross; 
    public Sprite zero;  

    public bool Clicked_Now = false; 
    public int number;               

    public void OnClick()
    {
        var game = Game_2.instance_2;
        Debug.Log("STATE: " + game.current_State); 

        if (game == null) return;
        if (game.game_Over) return;

        
        if (game.current_State == Game_2.TurnState.Delete_Mode)
        {
           
            if (!Clicked_Now) return;

            xo.sprite = null;        
            Clicked_Now = false;    

            game.xo_moves[number] = 0;

           
            game.current_State = Game_2.TurnState.Normal;

            return;
        }

        
        if (game.current_State == Game_2.TurnState.Choose_Action)
            return;

        
        if (Clicked_Now) return;

       
        if (game.current_State == Game_2.TurnState.Forced_Move)
        {
            Mark(game.forcedX);
        }
        else
        {
            
            Mark(game.playerX);
        }

        
        game.current_State = Game_2.TurnState.Normal;

      
        game.End_Move();
    }

    
    void Mark(bool x_now)
    {
        xo.sprite = x_now ? cross : zero;
        Clicked_Now = true;

        Game_2.instance_2.xo_moves[number] = x_now ? 1 : 2;
        Game_2.instance_2.CheckWin();
    }
}