using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class GameOverController : MonoBehaviour
    {
        public void Restart()
        {
            SceneManager.LoadScene("Level");
        }

        public void MainMenu()
        {
            SceneManager.LoadScene("Home");
        }
    }
}
