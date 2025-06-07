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
            return enemySettings.GetEnemy(enemyType);
        }

        public DefenderDataModel GetDefender(DefenderType defenderType)
        {
            return defenderSettings.GetDefender(defenderType);
        }
    }
}
