using AYellowpaper.SerializedCollections;
using Game.Defender;
using Game.Enemy;
using UnityEngine;

namespace Managers
{
    public class SpriteManager : MonoSingleton<SpriteManager>
    {
        [SerializeField] private SerializedDictionary<EnemyType, Sprite> enemySprites;
        [SerializeField] private SerializedDictionary<DefenderType, Sprite> defenderSprites;


        public Sprite GetEnemySprite(EnemyType enemyType)
        {
            enemySprites.TryGetValue(enemyType, out var sprite);
            return sprite;
        }

        public Sprite GetDefenderSprite(DefenderType defenderType)
        {
            defenderSprites.TryGetValue(defenderType, out var sprite);
            return sprite;
        }
    }
}
