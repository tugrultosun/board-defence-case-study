using System;
using Game.Defender;
using Game.Enemies;

namespace Settings
{
    public class GameSettingsManager : MonoSingleton<GameSettingsManager>
    {
        public DefenderSettings defenderSettings;
        public EnemySettings enemySettings;
        public BoardSettings boardSettings;
        public ProjectileSettings projectileSettings;

        public EnemyDataModel GetEnemy(EnemyType enemyType)
        {
            switch (enemyType)
            {
                case EnemyType.Enemy1:
                    return enemySettings.enemyData1;
                case EnemyType.Enemy2:
                    return enemySettings.enemyData2;
                case EnemyType.Enemy3:
                    return enemySettings.enemyData3;
                default:
                    return null;
            }
        }

        public DefenderDataModel GetDefender(DefenderType defenderType)
        {
            switch (defenderType)
            {
                case DefenderType.Defender1:
                    return defenderSettings.defenderData1;
                case DefenderType.Defender2:
                    return defenderSettings.defenderData2;
                case DefenderType.Defender3:
                    return defenderSettings.defenderData3;
                default:
                    return null;
            }
        }
    }
}
