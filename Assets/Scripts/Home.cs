using UnityEngine;
using UnityEngine.SceneManagement;

public class Home : MonoBehaviour
{
    public void NewGame()
    {
        SceneManager.LoadScene("Level");
    }

    public void Continue()
    {

    }

    public void Settings()
    {

    }

    public void Quit()
    {
        Application.Quit(0);
    }
}
