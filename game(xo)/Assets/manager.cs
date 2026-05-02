using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public enum Gametype
{
    With_friend,
    With_PK
}

public class manager : MonoBehaviour
{
    public static manager variable;
    public Gametype current;

    private bool isLoading = false;

    void Awake()
    {
        if (variable != null && variable != this)
        {
            Destroy(gameObject);
            return;
        }

        variable = this;
    }

    public void With_friend_game()
    {
        current = Gametype.With_friend;
        SceneManager.LoadScene("With_friend_scene");
    }

    public void With_PK_game()
    {
        current = Gametype.With_PK;
        SceneManager.LoadScene("With_PK");
    }

    public void RestartSceneAfterDelay(float time)
    {
        if (isLoading) return;

        StartCoroutine(RestartCoroutine(time));
    }

    private IEnumerator RestartCoroutine(float time)
    {
        isLoading = true;

        yield return new WaitForSeconds(time);

        SceneManager.LoadScene("start");

        isLoading = false;
    }

}