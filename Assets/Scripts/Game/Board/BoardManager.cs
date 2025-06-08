using Events;
using Game.Controllers;
using Game.Tiles;
using Game.Enemies;
using Managers;
using Settings;
using UnityEngine;
using Utils;

namespace Game.Board
{
    public class BoardManager : MonoSingleton<BoardManager>
    {
        [SerializeField] private Camera boardCamera;
        
        public Tile tilePrefab;

        private CameraController cameraController;

        private DefenceController defenceController;
        public EnemyController EnemyController { get; private set; }
        private TileController TileController { get; set; }

        private Tile[,] tiles;

        public override void Awake()
        {
            TileController = new TileController(tilePrefab, transform);
            TileController.GenerateTiles(GameSettingsManager.Instance.boardSettings.width, GameSettingsManager.Instance.boardSettings.height);
            cameraController = new CameraController();
            cameraController.Initialize(boardCamera);
            EventManager.Instance.AddListener<LevelDataLoadedEvent>(OnLevelDataLoaded);
        }


        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<LevelDataLoadedEvent>(OnLevelDataLoaded);
        }
        
        private async void OnLevelDataLoaded(object e)
        {
            EnemyController = new EnemyController(GameSettingsManager.Instance.enemySettings);
            await EnemyController.InitializeEnemies(GameManager.Instance.LevelManager.CurrentLevel.EnemyLevelData);
            defenceController = new DefenceController(GameSettingsManager.Instance.defenderSettings);
            await defenceController.InitializeDefenders(GameManager.Instance.LevelManager.CurrentLevel.DefenderLevelData);
        }

        public Tile GetClosestTileForDroppingDefender(Vector2 mousePos)
        {
            return TileController.GetClosestTileForDroppingDefender(mousePos);
        }

        public Vector3 GetRandomUpmostTileSpawnPosition()
        {
            return TileController.GetRandomUpmostTileSpawnPosition();
        }

        public void RemoveDeadEnemy(Enemy enemy)
        {
            bool enemyDoesntLeft = EnemyController.Remove(enemy);
            if(enemyDoesntLeft)
            {
                EventManager.Instance.TriggerEvent<GameFinishedEvent>(new GameFinishedEvent{IsWin = true});
            }
        }
    }
}