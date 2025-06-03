using System;
using Game.Defender;
using Game.Enemy;

namespace Settings
{
    public class GameSettingsManager : MonoSingleton<GameSettingsManager>
    {
        public DefenderSettings defenderSettings;
        public EnemySettings enemySettings;
        public BoardSettings boardSettings;


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
    }
}
