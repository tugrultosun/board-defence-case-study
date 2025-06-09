using AssetLoader;
using Events;
using Game.Controllers;
using Game.Tiles;
using Game.Enemies;
using Managers;
using Settings;
using UnityEngine;
using Utils;
using Zenject;

namespace Game.Board
{
    public class BoardManager : MonoSingleton<BoardManager>
    {
        [SerializeField] private Camera boardCamera;
        
        [SerializeField] private WaveController waveController;
        
        public Tile tilePrefab;

        private CameraController cameraController;

        [Inject] private DefenceController defenceController;
        [Inject] public EnemyController EnemyController { get; private set; }
        [Inject] private TileController TileController { get; set; }

        private Tile[,] tiles;

        [Inject] private IAssetLoader assetLoader;

        public override void Awake()
        {
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
            await EnemyController.InitializeEnemies(GameManager.Instance.LevelManager.CurrentLevel.EnemyLevelData);
            waveController.Initialize(EnemyController);
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