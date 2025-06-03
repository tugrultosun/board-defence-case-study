namespace Game.Defender
{
    [System.Serializable]
    public class DefenderDataModel
    {
        public DefenderType defenderType;
        public int damage;
        public int range;
        public int attackRate;
        public DefenderAttackDirection direction;
    }
}
