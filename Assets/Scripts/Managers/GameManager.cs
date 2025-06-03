using Events;
using Levels;
using Save;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Managers
{
    public class GameManager : MonoSingleton<GameManager>
    {
        public LevelData currentLevel;

        private SaveManager saveManager;

        public override void Awake()
        {
            base.Awake();
            saveManager = new SaveManager();
            LoadLevelData();
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
    }
}