using AYellowpaper.SerializedCollections;
using Game.Enemy;
using UnityEngine;

namespace Managers
{
    public class SpriteManager : MonoSingleton<SpriteManager>
    {
        [SerializeField] private SerializedDictionary<EnemyType, Sprite> enemySprites;



        public Sprite GetEnemySprite(EnemyType enemyType)
        {
            enemySprites.TryGetValue(enemyType, out var sprite);
            return sprite;
        }
    }
}
