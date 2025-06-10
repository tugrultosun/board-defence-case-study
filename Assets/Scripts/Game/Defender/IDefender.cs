namespace Game.Defender
{
    public interface IDefender
    {
        public DefenderType DefenderType { get; }
        public int Damage { get; }
        public int Range { get; }
        public int AttackRate { get; }
        public bool CanAttack { get; }
        public DefenderAttackDirection AttackDirection { get; }
        public IAttackStrategy Strategy { get; }
        public void Initialize(DefenderDataModel defenderDataModel);
        public void Activate();
        public void Deactivate();
    }
}