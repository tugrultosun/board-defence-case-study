using System.Collections.Generic;
using System.Threading.Tasks;
using AssetLoader;
using Game.Enemies;
using Lean.Pool;
using Settings;
using UnityEngine;
using Zenject;

namespace Game.Board
{
    public class EnemyController
    {
        private readonly EnemySettings enemySettings;
        public List<Enemy> Enemies { get; private set; }
        
        private readonly IAssetLoader assetLoader;
        
        [Inject]
        public EnemyController(EnemySettings settings, IAssetLoader loader)
        {
            enemySettings = settings;
            Enemies = new List<Enemy>();
            assetLoader = loader;
        }
        
        public async Task InitializeEnemies(List<EnemyLevelData> currentLevelEnemyLevelData)
        {
            var enemyPrefab = await assetLoader.LoadAssetAsync<GameObject>("enemy");
            if (enemyPrefab != null)
            {
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
                assetLoader.ReleaseAsset(enemyPrefab);
            }
            else
            {
                Debug.LogError("Failed to load enemy prefab");
            }
        }

        public bool Remove(Enemy enemy)
        {
            Enemies.Remove(enemy);
            return Enemies.Count <= 0;
        }
    }
}
