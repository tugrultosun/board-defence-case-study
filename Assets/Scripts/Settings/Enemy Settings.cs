using Game.Enemy;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "EnemySettings", menuName = "Scriptable Objects/EnemySettings", order = 1)]
    public class EnemySettings : ScriptableObject
    {
        public EnemyDataModel enemyData1;
        public EnemyDataModel enemyData2;
        public EnemyDataModel enemyData3;
    }
}
