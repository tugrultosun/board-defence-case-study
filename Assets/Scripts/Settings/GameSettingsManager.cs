using Utils;

namespace Settings
{
    public class GameSettingsManager : MonoSingleton<GameSettingsManager>
    {
        public DefenderSettings defenderSettings;
        public EnemySettings enemySettings;
        public BoardSettings boardSettings;
        public ProjectileSettings projectileSettings;
    }
}
