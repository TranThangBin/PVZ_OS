using UnityEngine;

namespace Game
{
    public readonly struct GameGrid
    {
        public readonly Vector2[,] Matrix;
        public readonly Vector2 CellSize;

        public GameGrid(Vector2 gridStart, Vector2 gridEnd, int row, int column)
        {
            Matrix = new Vector2[row, column];

            float width = Mathf.Abs(gridEnd.x - gridStart.x);
            float height = Mathf.Abs(gridEnd.y - gridStart.y);

            float cellWidth = width / column;
            float cellHeight = height / row;

            CellSize = new(cellWidth, cellHeight);

            for (int r = 0; r < row; r++)
            {
                for (int c = 0; c < column; c++)
                {
                    Vector2 cellPos = gridStart +
                        new Vector2(cellWidth * c + cellWidth / 2,
                            -(cellHeight * r + cellHeight / 2));

                    Matrix[r, c] = cellPos;
                }
            }
        }
    }
}
