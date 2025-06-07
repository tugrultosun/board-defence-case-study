using Events;
using Managers;
using Save;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Levels
{
    public class LevelManager
    {
        private SaveManager saveManager;

        public LevelData CurrentLevel { get; private set; }

        public LevelManager(SaveManager saveManager)
        {
            this.saveManager = saveManager;
        }

        public async void LoadLevelData()
        {
            int levelIndex = ((saveManager.LoadLevel().Level - 1) % 3) + 1;
            var handle = Addressables.LoadAssetAsync<LevelData>("Level" + levelIndex);
            await handle.Task;

            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                CurrentLevel = handle.Result;
                EventManager.Instance.TriggerEvent<LevelDataLoadedEvent>();
            }
            else
            {
                Debug.LogError($"Failed to load level: {levelIndex}");
            }
        }
    }
}
