namespace Game.Defender
{
    public interface IDefender
    {
        public DefenderType DefenderType { get; set; }
        public int Damage { get; set; }
        public int Range { get; set; }
        public int AttackRate { get; set; }
        public bool CanAttack { get; set; }
        public DefenderAttackDirection AttackDirection { get; set; }
        public void Initialize(DefenderDataModel defenderDataModel);
        public void Activate();
        public void Deactivate();
    }
}