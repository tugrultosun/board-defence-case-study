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
        
        private bool isGameFinished;

        public override void Awake()
        {
            saveManager = new SaveManager();
            LoadLevelData();
            EventManager.Instance.AddListener<GameFinishedEvent>(OnGameFinished);
            isGameFinished = false;
        }

        private async void LoadLevelData()
        {
            var levelEntity = saveManager.LoadLevel();
            int levelIndex = ((levelEntity.Level - 1) % 3) + 1; // just for wrapping level number between 1 and 3
            var handle = Addressables.LoadAssetAsync<LevelData>("Level" + levelIndex);
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
            if (isGameFinished)
            {
                Debug.LogWarning("Game is finished triggered already...Returning...");
                return;
            }
            isGameFinished = true;
            Debug.LogWarning("Game finished event called");
            var gameFinishedEvent = (GameFinishedEvent)e;
            if (gameFinishedEvent.IsWin)
            {
                endGameScreen.InitializeWinPanel();
                saveManager.IncreaseLevel();
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