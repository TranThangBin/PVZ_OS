using UnityEngine;
using UnityEngine.Assertions;
using UnityEngine.Events;

namespace Game
{
    public class LawnManager : MonoBehaviour
    {
        [SerializeField] private Transform _lawnStart;
        [SerializeField] private Transform _lawnEnd;
        [SerializeField] private int _lawnRowCount;
        [SerializeField] private int _lawnColumnCount;

        public UnityEvent<Transform> OnLawnCellClick;

        private void Awake()
        {
            float width = _lawnEnd.position.x - _lawnStart.position.x;
            float height = _lawnStart.position.y - _lawnEnd.position.y;

            Assert.IsTrue(width > 0 && height > 0, "Check your lawn start and lawn end");

            Vector2 cellSize = new(width / _lawnColumnCount, height / _lawnRowCount);

            for (int row = 0; row < _lawnRowCount; row++)
            {
                for (int col = 0; col < _lawnColumnCount; col++)
                {
                    GameObject cell = new($"lawnCell{row}:{col}", typeof(BoxCollider2D));
                    Vector3 instancePos = _lawnStart.position + new Vector3(cellSize.x * col + cellSize.x / 2, -(cellSize.y * row + cellSize.y / 2));

                    cell.transform.position = instancePos;
                    cell.transform.parent = transform;
                    cell.layer = LayerMask.NameToLayer("LawnCell");

                    BoxCollider2D collider = cell.GetComponent<BoxCollider2D>();
                    collider.size = cellSize;
                }
            }
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                RaycastHit2D hit = Physics2D.Raycast(mousePosition, Vector2.zero, 1, LayerMask.GetMask("LawnCell", "Sun"));

                if (hit.collider != null && hit.collider.gameObject.layer != LayerMask.NameToLayer("Sun"))
                {
                    OnLawnCellClick.Invoke(hit.collider.transform);
                }
            }
        }
    }
}
