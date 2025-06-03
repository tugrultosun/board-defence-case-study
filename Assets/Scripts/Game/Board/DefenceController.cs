using System.Collections.Generic;
using System.Threading.Tasks;
using Draggable;
using Game.Defender;
using Lean.Pool;
using Settings;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Game.Board
{
    public class DefenceController
    {
        public async Task InitializeDefenders(List<DefenderLevelData> currentLevelEnemyDefenderLevelData)
        {
            var handle = Addressables.LoadAssetAsync<GameObject>("defender");
            await handle.Task;
            if (handle.Status == AsyncOperationStatus.Succeeded)
            {
                var defenderPrefab = handle.Result;
                foreach (var defenderLevelData in currentLevelEnemyDefenderLevelData)
                    for (var i = 0; i < defenderLevelData.count; i++)
                    {
                        var defender = LeanPool.Spawn(defenderPrefab.GetComponent<Defender.Defender>());
                        defender.Initialize(GameSettingsManager.Instance.GetDefender(defenderLevelData.defenderType));
                        defender.transform.position = new Vector3((int)defenderLevelData.defenderType, -1, 0);
                        defender.GetComponent<DraggableItem>().initialPos = defender.transform.position;
                    }
            }
            else
            {
                Debug.LogError("Failed to load defender");
            }
        }
    }
}