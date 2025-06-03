using Events;
using Levels;
using Managers;
using Save;
using UnityEngine;
using UnityEngine.AddressableAssets;

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
        LevelEntity levelEntity = saveManager.LoadLevel();
        var handle= Addressables.LoadAssetAsync<LevelData>("level" + levelEntity.Level);
        await handle.Task;
        if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
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
