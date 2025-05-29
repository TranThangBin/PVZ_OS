using UnityEngine;

namespace Game
{
    public class GameOver : MonoBehaviour
    {
        [SerializeField] private GameObject _gameOverMenu;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                _gameOverMenu.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
}
