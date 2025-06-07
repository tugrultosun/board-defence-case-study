using System.Collections.Generic;
using Game.Enemies;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Scriptable Objects/EnemySettings", order = 1)]
    public class EnemySettings : ScriptableObject
    {
        
        public List<EnemyDataModel> enemyDataModels;
        
        public EnemyDataModel GetEnemy(EnemyType type)
        {
            foreach (var enemyDataModel in enemyDataModels)
            {
                if (enemyDataModel.enemyType == type)
                {
                    return enemyDataModel;
                }
            }
            Debug.LogWarning($"EnemyType '{type}' not found in EnemySettings.");
            return null;
        }
    }
}
