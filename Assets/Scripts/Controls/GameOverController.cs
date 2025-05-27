using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameOverController : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene("Level");
            Time.timeScale = 1.0f;
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("Home");
            Time.timeScale = 1.0f;
        }
    }
}
