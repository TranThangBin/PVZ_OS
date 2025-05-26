using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class HomeController : MonoBehaviour
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
}
