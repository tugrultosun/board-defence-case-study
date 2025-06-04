using System.Collections.Generic;
using Game.Defender;
using Game.Enemies;
using UnityEngine;

namespace Levels
{
    [CreateAssetMenu(fileName = "LevelData", menuName = "Scriptable Objects/Level", order = 1)]
    public class LevelData : ScriptableObject
    {
        public List<EnemyLevelData> EnemyLevelData;
        public List<DefenderLevelData> DefenderLevelData;
    }
}