using Managers;
using UnityEngine;

namespace Game.Enemy
{
    public class Enemy : MonoBehaviour , IEnemy
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        public void Initialize(EnemyDataModel enemyDataModel)
        {
            spriteRenderer.sprite = SpriteManager.Instance.GetEnemySprite(enemyDataModel.enemyType);
        }
    }
}
