using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Misc/UI Event Handlers")]
    public class UIEventHandler : ScriptableObject
    {
        [SerializeField] private PlayerLevels _playerLevels;
        [SerializeField] private string _homeScene;
        [SerializeField] private string _levelScene;

        public void HomeNewGame()
        {
            _playerLevels.CurrentLevel = 1;
            SceneManager.LoadScene(_levelScene);
        }

        public void HomeContinue()
        {
            SceneManager.LoadScene(_levelScene);
        }

        public void HomeQuit()
        {
            Application.Quit();
        }

        public void LevelGameOverRestart()
        {
            SceneManager.LoadScene(_levelScene);
        }

        public void LevelGameOverMainMenu()
        {
            SceneManager.LoadScene(_homeScene);
        }
    }
}
