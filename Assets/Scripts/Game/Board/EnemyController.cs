using System.Collections.Generic;
using Game.Enemy;
using Lean.Pool;

namespace Game.Board
{
    public class EnemyController
    {
        private Enemy.Enemy enemy;
        public void InitializeEnemies(List<EnemyLevelData> currentLevelEnemyLevelData)
        {
            foreach (var enemyLevelData in currentLevelEnemyLevelData)
            {
                var enemy = LeanPool.Spawn<Enemy.Enemy>(this.enemy);
            }
        }
    }
}
