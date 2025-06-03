using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Enemy;
using Lean.Pool;
using Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Board
{
    public class EnemyController
    {
        public async Task InitializeEnemies(List<EnemyLevelData> currentLevelEnemyLevelData)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>("enemy");
            await handle.Task;
            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                var enemyPrefab  = handle.Result;
                foreach (var enemyLevelData in currentLevelEnemyLevelData)
                {
                    for (int i = 0; i < enemyLevelData.count; i++)
                    {
                        var enemy = LeanPool.Spawn<Enemy.Enemy>(enemyPrefab.GetComponent<Enemy.Enemy>());
                        enemy.Initialize(GameSettingsManager.Instance.GetEnemy(enemyLevelData.enemyType));
                    }
                }
            }
            else
            {
                Debug.LogError($"Failed to load enemy");
            }
        }
    }
}
