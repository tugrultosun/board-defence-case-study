using System;
using Events;
using Game.Controllers;
using Managers;
using Settings;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Board
{
    public class BoardManager : MonoBehaviour
    {
        public Tile tilePrefab;
        
        private Tile[,] tiles;
        
        private CameraController cameraController;

        private EnemyController enemyController;
        
        private DefenceController defenceController;
        
        private void Awake()
        {
            InitializeTiles();
            cameraController = new CameraController();
            cameraController.Initialize();
            EventManager.Instance.AddListener<LevelDataLoadedEvent>(OnLevelDataLoaded);
        }

        private void InitializeTiles()
        {
            tiles = new Tile[GameSettingsManager.Instance.boardSettings.width, GameSettingsManager.Instance.boardSettings.height];
            for (int i = 0; i < GameSettingsManager.Instance.boardSettings.width; i++)
            {
                for (int j = 0; j < GameSettingsManager.Instance.boardSettings.height; j++)
                {
                    Tile tile = GameObject.Instantiate(tilePrefab,transform);
                    tile.transform.position = new Vector3(i, j);
                    tiles[i, j] = tile;
                    tile.Init(i,j);
                }
            }
        }

        private void OnLevelDataLoaded(object e)
        {
            enemyController = new EnemyController();
            enemyController.InitializeEnemies(GameManager.Instance.currentLevel.EnemyLevelData);
            defenceController = new DefenceController();
        }


        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<LevelDataLoadedEvent>(OnLevelDataLoaded);
        }
    }
}
