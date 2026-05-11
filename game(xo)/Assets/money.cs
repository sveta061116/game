using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class money : MonoBehaviour
{
    public Image money_xo; //спрайты монетки
    public Sprite money_x;
    public Sprite money_0;
    public Sprite money_null;

    public TextMeshProUGUI text;

    void Start()
    {
        money_xo.sprite = money_null;
        text.text = "";
    }
    public void SetCoin(bool isX) //Смена состояния монетки
    {
        if (isX)
        {
            money_xo.sprite = money_x;
            text.text = "ставь крестик";
        }
        else
        {
            money_xo.sprite = money_0;
            text.text = "ставь нолик";
        }
    }
    public void Hide() //Обнуление состояния монетки
    {
        money_xo.sprite = money_null;
        text.text = "";
    }

}