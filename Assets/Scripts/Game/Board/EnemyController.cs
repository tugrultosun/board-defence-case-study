using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Enemies;
using Lean.Pool;
using Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Board
{
    public class EnemyController
    {
        private readonly EnemySettings enemySettings;
        public List<Enemy> Enemies { get; private set; }
        
        public EnemyController(EnemySettings settings)
        {
            enemySettings = settings;
            Enemies = new List<Enemy>();
        }
        
        public async Task InitializeEnemies(List<EnemyLevelData> currentLevelEnemyLevelData)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>("enemy");
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var enemyPrefab  = handle.Result;
                foreach (var enemyLevelData in currentLevelEnemyLevelData)
                {
                    for (int i = 0; i < enemyLevelData.count; i++)
                    {
                        var enemy = LeanPool.Spawn(enemyPrefab.GetComponent<Enemy>());
                        enemy.Initialize(enemySettings.GetEnemy(enemyLevelData.enemyType));
                        enemy.transform.position = BoardManager.Instance.GetRandomUpmostTileSpawnPosition();
                        Enemies.Add(enemy);
                        enemy.gameObject.SetActive(false);
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
            Enemies.Remove(enemy);
            if (Enemies.Count <= 0)
            {
                return true;
            }

            return false;
        }
    }
}
