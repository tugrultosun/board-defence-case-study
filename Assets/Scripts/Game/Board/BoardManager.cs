using Events;
using Game.Controllers;
using Game.Tiles;
using Managers;
using Settings;
using UnityEngine;

namespace Game.Board
{
    public class BoardManager : MonoSingleton<BoardManager>
    {
        public Tile tilePrefab;

        private CameraController cameraController;

        private DefenceController defenceController;

        private EnemyController enemyController;

        private Tile[,] tiles;

        public override void Awake()
        {
            base.Awake();
            InitializeTiles();
            cameraController = new CameraController();
            cameraController.Initialize();
            EventManager.Instance.AddListener<LevelDataLoadedEvent>(OnLevelDataLoaded);
        }


        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<LevelDataLoadedEvent>(OnLevelDataLoaded);
        }

        private void InitializeTiles()
        {
            tiles = new Tile[GameSettingsManager.Instance.boardSettings.width,
                GameSettingsManager.Instance.boardSettings.height];
            for (var i = 0; i < GameSettingsManager.Instance.boardSettings.width; i++)
            for (var j = 0; j < GameSettingsManager.Instance.boardSettings.height; j++)
            {
                var tile = Instantiate(tilePrefab, transform);
                tile.transform.position = new Vector3(i, j);
                tiles[i, j] = tile;
                tile.Init(i, j);
            }
        }

        private async void OnLevelDataLoaded(object e)
        {
            enemyController = new EnemyController();
            await enemyController.InitializeEnemies(GameManager.Instance.currentLevel.EnemyLevelData);
            defenceController = new DefenceController();
            await defenceController.InitializeDefenders(GameManager.Instance.currentLevel.DefenderLevelData);
        }


        public Tile GetClosestTileForDroppingDefender(Vector2 mousePos)
        {
            for (var i = 0; i < GameSettingsManager.Instance.boardSettings.width; i++)
            for (var j = 0; j < GameSettingsManager.Instance.boardSettings.height; j++)
                if (Vector2.Distance(mousePos, tiles[i, j].transform.position) < 0.5f &&
                    j <= GameSettingsManager.Instance.boardSettings.defenceItemMaxPlacebleYIndex && tiles[i, j].GetTileState() == typeof(EmptyTileState))
                    return tiles[i, j];

            return null;
        }

        public void RemoveEnemy(Enemy.Enemy enemy)
        {
            bool enemyDoesntLeft = enemyController.Remove(enemy);
            if(enemyDoesntLeft)
            {
                EventManager.Instance.TriggerEvent<GameFinishedEvent>(new GameFinishedEvent{IsWin = true});
            }
        }
    }
}