using Managers;
using UnityEngine;

namespace Game.Defender
{
    public class Defender : MonoBehaviour, IDefender
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public bool CanAttack { get; set; }

        public void Initialize(DefenderDataModel defenderDataModel)
        {
            spriteRenderer.sprite = SpriteManager.Instance.GetDefenderSprite(defenderDataModel.defenderType);
        }

        public void Activate()
        {
            CanAttack = true;
        }

        public void Deactivate()
        {
            CanAttack = false;
        }
    }
}