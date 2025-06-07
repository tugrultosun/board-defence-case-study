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
        
        private SaveManager saveManager;

        public LevelManager LevelManager { get; private set; }

        private bool isGameFinished;

        public override void Awake()
        {
            saveManager = new SaveManager();
            LevelManager = new LevelManager(saveManager);
            LevelManager.LoadLevelData();
            EventManager.Instance.AddListener<GameFinishedEvent>(OnGameFinished);
            isGameFinished = false;
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