using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game
{
    public class Trophy : MonoBehaviour
    {
        private void OnDestroy() => DOTween.Kill(this);

        private void Update()
        {
            if (!DOTween.IsTweening(this) && Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D rc = Physics2D.Raycast(mousePosition, Vector2.zero, 1,
                    LayerMask.GetMask("Trophy"));
                float animateDuration = 5;
                if (rc.collider != null && rc.collider.gameObject == gameObject)
                {
                    DOTween.
                        Sequence(this).
                        Append(transform.DOMove(Camera.main.transform.position, animateDuration)).
                        Join(transform.DOScale(30, animateDuration)).
                        OnComplete(() =>
                        {
                            if (PlayerLevels.IncrementLevel())
                            {
                                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                            }
                            else
                            {
                                SceneManager.LoadScene("Home");
                            }
                        });
                }
            }
        }
    }
}
