using UnityEngine;

namespace Game
{
    public class GameOverTrigger : MonoBehaviour
    {
        public RectTransform GameOverMenu;

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                GameOverMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
            }
        }

        private void OnDestroy() => Time.timeScale = 1;
    }
}
