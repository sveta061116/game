using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;//корутины

public enum Gametype  //Режимы игры
{
    With_friend,
    With_PK
}

public class manager : MonoBehaviour
{
    public static manager variable; //Глобальный доступ к переменной класса
    public Gametype current; //Текущий выбранный режим

    private bool isLoading = false; //Защита от множественной загрузки

    void Awake() //Удаление дубликатов менеджера
    {
        if (variable != null && variable != this)
        {
            Destroy(gameObject);
            return;
        }

        variable = this;
    }

    public void With_friend_game() //Смена сцены для игры с другом
    {
        current = Gametype.With_friend;
        SceneManager.LoadScene("With_friend_scene");
    }

    public void With_PK_game() //Смена сцены для игры с ПК
    {
        current = Gametype.With_PK;
        SceneManager.LoadScene("With_PK");
    }

    public void RestartSceneAfterDelay(float time) //Перезапуск игры
    {
        if (isLoading) return;

        StartCoroutine(RestartCoroutine(time));
    }

    private IEnumerator RestartCoroutine(float time)//Корутина для перезапуска
    {
        isLoading = true;

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("start");

        isLoading = false;
    }

}