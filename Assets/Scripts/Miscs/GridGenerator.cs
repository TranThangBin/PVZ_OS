using UnityEngine;

namespace Game
{
    public class GridGenerator : MonoBehaviour
    {
        [SerializeField] private Transform _lawnStart;
        [SerializeField] private Transform _lawnEnd;
        [SerializeField] private int _lawnRowCount;
        [SerializeField] private int _lawnColumnCount;
        [SerializeField] private GameObject _object;

        [ContextMenu("Generate Objects")]
        private void GenerateObjects()
        {
            if (_object == null) { return; }

            CleanupGrid();

            float width = _lawnEnd.position.x - _lawnStart.position.x;
            float height = _lawnStart.position.y - _lawnEnd.position.y;

            Vector2 cellSize = new(width / _lawnColumnCount, height / _lawnRowCount);

            for (int row = 0; row < _lawnRowCount; row++)
            {
                for (int col = 0; col < _lawnColumnCount; col++)
                {
                    Vector3 instancePos = _lawnStart.position + new Vector3(cellSize.x * col + cellSize.x / 2, -(cellSize.y * row + cellSize.y / 2));
                    Instantiate(_object, instancePos, Quaternion.identity, transform);
                }
            }
        }

        [ContextMenu("Generate Colliders")]
        private void GenerateColliders()
        {
            CleanupGrid();

            float width = _lawnEnd.position.x - _lawnStart.position.x;
            float height = _lawnStart.position.y - _lawnEnd.position.y;

            Vector2 cellSize = new(width / _lawnColumnCount, height / _lawnRowCount);

            for (int row = 0; row < _lawnRowCount; row++)
            {
                for (int col = 0; col < _lawnColumnCount; col++)
                {
                    GameObject cell = new($"GridCell{row}:{col}", typeof(BoxCollider2D));
                    Vector3 instancePos = _lawnStart.position + new Vector3(cellSize.x * col + cellSize.x / 2, -(cellSize.y * row + cellSize.y / 2));

                    cell.transform.position = instancePos;
                    cell.transform.parent = transform;
                    cell.layer = gameObject.layer;

                    BoxCollider2D collider = cell.GetComponent<BoxCollider2D>();
                    collider.size = cellSize;
                }
            }
        }

        private void CleanupGrid()
        {
            GameObject[] children = new GameObject[transform.childCount];
            for (int i = 0; i < children.Length; i++)
            {
                children[i] = transform.GetChild(i).gameObject;
            }
            foreach (GameObject child in children)
            {
                DestroyImmediate(child);
            }
        }
    }
}
