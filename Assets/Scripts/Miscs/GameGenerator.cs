using System.Collections.Generic;
using UnityEngine;

namespace Game
{
    public class GameGenerator : MonoBehaviour
    {
        [SerializeField] private GameGeneratorProps _props;
        [SerializeField] private Transform _pool;
        [SerializeField] private Transform _lawnStart;
        [SerializeField] private Transform _lawnEnd;
        [SerializeField] private RectTransform _gameOverMenu;

        [ContextMenu("Generate Game")]
        private void GenerateGame()
        {
            List<Transform> children = new();
            foreach (Transform child in _pool.transform) { children.Add(child); }
            children.ForEach((t) => DestroyImmediate(t.gameObject));

            GameGrid grid = new(_lawnStart.position, _lawnEnd.position,
                _props.Cleaners.Length, _props.LawnColumns);

            GameObject lawn = new("Lawn");
            lawn.transform.parent = _pool;

            for (int r = 0; r < grid.Matrix.GetLength(0); r++)
            {
                for (int c = 0; c < grid.Matrix.GetLength(1); c++)
                {
                    GameObject cell = new($"LawnCell{r}:{c}", typeof(BoxCollider2D));
                    cell.transform.parent = lawn.transform;
                    cell.transform.position = grid.Matrix[r, c];
                    cell.layer = LayerMask.NameToLayer("LawnCell");
                    cell.tag = $"Row{r}";
                    cell.GetComponent<BoxCollider2D>().size = grid.CellSize;
                }
            }

            GameObject cleaners = new("Cleaners");
            cleaners.transform.parent = _pool;

            for (int i = 0; i < _props.Cleaners.Length; i++)
            {
                LawnMower cleaner = Instantiate(_props.Cleaners[i],
                    grid.Matrix[i, 0] + Vector2.left * grid.CellSize * 0.8f, Quaternion.identity);
                cleaner.transform.parent = cleaners.transform;
                cleaner.tag = $"Row{i}";
                cleaner.name = $"Cleaner{i}";
            }

            GameObject gameOverTrigger = new("GameOverTrigger");
            gameOverTrigger.layer = LayerMask.NameToLayer("GameOver");
            gameOverTrigger.transform.parent = _pool;
            gameOverTrigger.transform.position = grid.Matrix[0, 0] + Vector2.left * grid.CellSize * 1.8f;
            gameOverTrigger.AddComponent<GameOverTrigger>().GameOverMenu = _gameOverMenu;
            gameOverTrigger.AddComponent<BoxCollider2D>().size = new(5, 200);

            GameObject zombies = new("Zombies");
            zombies.transform.parent = _pool;

            ZombieSpawner zSpawner = zombies.AddComponent<ZombieSpawner>();
            zSpawner.ZombieSpawnInterval = _props.ZombieSpawnInterval;
            zSpawner.BasicZombie = _props.BasicZombie;

            for (int i = 0; i < grid.Matrix.GetLength(0); i++)
            {
                GameObject zombieSpawn = new($"ZombieSpawn{i}");
                zombieSpawn.transform.parent = zombies.transform;
                zombieSpawn.transform.position = grid.Matrix[i, _props.LawnColumns - 1] + Vector2.right * grid.CellSize;
                zombieSpawn.tag = $"Row{i}";
            }

            GameObject suns = new("Suns");
            suns.transform.parent = _pool;

            SunSpawner sSpawner = suns.AddComponent<SunSpawner>();
            sSpawner.Sun = _props.Sun;
            sSpawner.SunSpawnInterval = _props.SunSpawnInterval;
            sSpawner.SunSpawnStart = _lawnStart;
            sSpawner.SunSpawnEnd = _lawnEnd;
        }
    }
}
