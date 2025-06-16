using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    [CreateAssetMenu(menuName = "Scriptable Objects/UI Event Handlers")]
    public class UIEventHandler : ScriptableObject
    {
        [SerializeField] private string _homeScene;
        [SerializeField] private string _levelScene;

        public void HomeNewGame()
        {
            SceneManager.LoadScene(_levelScene);
        }

        public void HomeQuit()
        {
            Environment.Exit(0);
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
