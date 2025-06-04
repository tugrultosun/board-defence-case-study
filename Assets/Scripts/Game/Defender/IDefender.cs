namespace Game.Defender
{
    public interface IDefender
    {
        public bool CanAttack { get; set; }
        public void Initialize(DefenderDataModel defenderDataModel);
        
        public void Activate();
        
        public void Deactivate();
    }
}