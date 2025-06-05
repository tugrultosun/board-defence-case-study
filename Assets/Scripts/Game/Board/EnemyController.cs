using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Enemies;
using Lean.Pool;
using Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Board
{
    public class EnemyController
    {
        public List<Enemy> Enemies => enemies;
        
        private List<Enemy> enemies = new List<Enemy>();
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
                        var enemy = LeanPool.Spawn<Enemy>(enemyPrefab.GetComponent<Enemy>());
                        enemy.Initialize(GameSettingsManager.Instance.GetEnemy(enemyLevelData.enemyType));
                        var randomSpawnPosition = BoardManager.Instance.GetRandomUpmostTileSpawnPosition();
                        enemy.transform.position = randomSpawnPosition;
                        enemies.Add(enemy);
                    }
                }
                Addressables.Release(handle);
            }
            else
            {
                Debug.LogError($"Failed to load enemy");
            }
        }

        public bool Remove(Enemy enemy)
        {
            enemies.Remove(enemy);
            if (enemies.Count <= 0)
            {
                return true;
            }

            return false;
        }
    }
}
