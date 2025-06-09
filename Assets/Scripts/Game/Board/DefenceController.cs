using System.Collections.Generic;
using System.Threading.Tasks;
using AssetLoader;
using Draggable;
using Game.Defender;
using Lean.Pool;
using Settings;
using UnityEngine;
using Zenject;

namespace Game.Board
{
    public class DefenceController
    {
        private readonly DefenderSettings defenderSettings;
        private readonly IAssetLoader assetLoader;

        [Inject]
        public DefenceController(DefenderSettings settings,IAssetLoader loader)
        {
            defenderSettings = settings;
            assetLoader = loader;
        }
        
        public async Task InitializeDefenders(List<DefenderLevelData> currentLevelEnemyDefenderLevelData)
        {
            var defenderPrefab = await assetLoader.LoadAssetAsync<GameObject>("defender");
            if (defenderPrefab != null)
            {
                foreach (var defenderLevelData in currentLevelEnemyDefenderLevelData)
                {
                    for (var i = 0; i < defenderLevelData.count; i++)
                    {
                        var defender = LeanPool.Spawn(defenderPrefab.GetComponent<Defender.Defender>());
                        defender.Initialize(defenderSettings.GetDefender(defenderLevelData.defenderType));
                        defender.transform.position = new Vector3((int)defenderLevelData.defenderType, -1, 0);
                        defender.GetComponent<DraggableItem>().initialPos = defender.transform.position;
                    }
                }
                assetLoader.ReleaseAsset(defenderPrefab);
            }
            else
            {
                Debug.LogError("Failed to load defender prefab");
            }
        }
    }
}