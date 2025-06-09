using System.Collections;
using System.Collections.Generic;
using Game.Board;
using Game.Enemies;
using Settings;
using UnityEngine;

namespace Game.Controllers
{
    public class WaveController : MonoBehaviour
    {
        private EnemyController enemyController;
        
        private int boardWidth;

        private float delay;
        
        private WaitForSeconds delayWait;

        public void Initialize(EnemyController controller)
        {
            enemyController = controller;
            boardWidth = GameSettingsManager.Instance.boardSettings.width;
            delayWait = new WaitForSeconds(GameSettingsManager.Instance.boardSettings.boardEnemySpawnDelay);
            StartCoroutine(SpawnEnemies());
        }
        
        private IEnumerator SpawnEnemies()
        {
            HashSet<int> indexes = new HashSet<int>();
            int totalSpawnedEnemies = 0;
            int goingToSpawn = enemyController.Enemies.Count;
            while (totalSpawnedEnemies < goingToSpawn)
            {
                var spawnedEnemyCountOnWave = 0;
                indexes.Clear();
                foreach (var enemy in enemyController.Enemies)
                {
                    if (!enemy.isActiveAndEnabled && !enemy.IsDead)
                    {
                        var randomColumnIndex = GetUniqueColumn(indexes);
                        SpawnEnemyOnColumn(enemy, randomColumnIndex);
                        spawnedEnemyCountOnWave++;
                        totalSpawnedEnemies++;
                        if (spawnedEnemyCountOnWave == boardWidth)
                        {
                            break;
                        }
                    }
                }
                yield return delayWait;
            }
            Debug.Log("All waves spawned ");
        }

        private int GetUniqueColumn(HashSet<int> indexes)
        {
            int randomIndex;
            do
            { 
                randomIndex = Random.Range(0, boardWidth);
            } while (indexes.Contains(randomIndex));
            indexes.Add(randomIndex);
            return randomIndex;
        }

        private void SpawnEnemyOnColumn(Enemy enemy, int randomColumnIndex)
        {
            enemy.transform.position = new Vector3(randomColumnIndex, enemy.transform.position.y, 0);
            enemy.gameObject.SetActive(true);
        }
    }
}
