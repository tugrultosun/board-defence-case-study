using System.Collections.Generic;
using System.Threading.Tasks;
using Game.Defender;
using Lean.Pool;
using Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace Game.Board
{
    public class DefenceController
    {
        public async Task InitializeDefenders(List<DefenderLevelData> currentLevelEnemyDefenderLevelData)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>("defender");
            await handle.Task;
            if (handle.Status == UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationStatus.Succeeded)
            {
                var defenderPrefab  = handle.Result;
                foreach (var defenderLevelData in currentLevelEnemyDefenderLevelData)
                {
                    for (int i = 0; i < defenderLevelData.count; i++)
                    {
                        var enemy = LeanPool.Spawn<Defender.Defender>(defenderPrefab.GetComponent<Defender.Defender>());
                        enemy.Initialize(GameSettingsManager.Instance.GetDefender(defenderLevelData.defenderType));
                    }
                }
            }
            else
            {
                Debug.LogError($"Failed to load defender");
            }
        }
    }
}
