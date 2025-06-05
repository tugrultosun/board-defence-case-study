using System;
using Events;
using Game.UI;
using Levels;
using Save;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        [SerializeField] private EndGameScreen endGameScreen;
        
        public LevelData currentLevel;

        private SaveManager saveManager;

        public override void Awake()
        {
            base.Awake();
            saveManager = new SaveManager();
            LoadLevelData();
            EventManager.Instance.AddListener<GameFinishedEvent>(OnGameFinished);
        }

        private async void LoadLevelData()
        {
            var levelEntity = saveManager.LoadLevel();
            var handle = Addressables.LoadAssetAsync<LevelData>("Level" + levelEntity.Level);
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                currentLevel = handle.Result;
                Debug.Log($"Loaded Level: {currentLevel.name}");
                EventManager.Instance.TriggerEvent<LevelDataLoadedEvent>();
            }
            else
            {
                Debug.LogError($"Failed to load level: {levelEntity.Level}");
            }
        }

        private void OnGameFinished(object e)
        {
            var gameFinishedEvent = (GameFinishedEvent)e;
            if (gameFinishedEvent.IsWin)
            {
                endGameScreen.InitializeWinPanel();
            }
            else
            {
                endGameScreen.InitializeLosePanel();
            }
        }


        private void OnDestroy()
        {
            EventManager.Instance.RemoveListener<GameFinishedEvent>(OnGameFinished);
        }
    }
}