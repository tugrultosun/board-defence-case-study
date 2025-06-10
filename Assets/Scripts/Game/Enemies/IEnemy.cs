using Game.Movement;

namespace Game.Enemies
{
    public interface IEnemy
    {
        public EnemyType EnemyType { get; }
        
        public int Health { get; }
        
        public float Speed { get; }
        
        public bool CanMove { get; }
        public IMovementAbility MovementAbility { get; }
        
        public void Initialize(EnemyDataModel enemyDataModel);
        
        public void ApplyDamage(int damage);

        public void Move();
    }
}
