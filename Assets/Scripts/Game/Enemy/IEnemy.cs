namespace Game.Enemy
{
    public interface IEnemy
    {
        public int Health { get; set; }
        
        public float Speed { get; set; }
        
        public void Initialize(EnemyDataModel enemyDataModel);
        
        public void ApplyDamage(int damage);
    }
}
