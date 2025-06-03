using System.Collections.Generic;
using Game.Defender;
using Game.Enemy;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/LevelData", order = 1)]
    public class LevelData : ScriptableObject
    {
        public List<EnemyLevelData> EnemyLevelData;
        public List<DefenderLevelData> DefenderLevelData;
    }
}
