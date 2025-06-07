using System.Collections.Generic;
using Game.Defender;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "Defender Settings", menuName = "Scriptable Objects/Defender Settings", order = 1)]
    public class DefenderSettings : ScriptableObject
    {
        
        public List<DefenderDataModel> defenderDataModels;
        
        
        public DefenderDataModel GetDefender(DefenderType defenderType)
        {
            foreach (var defenderDataModel in defenderDataModels)
            {
                if (defenderDataModel.defenderType == defenderType)
                {
                    return defenderDataModel;
                }
            }
            Debug.LogWarning($"DefenderType '{defenderType}' not found in DefenderSettings.");
            return null;
        }
    }
}
