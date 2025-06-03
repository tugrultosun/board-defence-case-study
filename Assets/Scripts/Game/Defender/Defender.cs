using Managers;
using UnityEngine;

namespace Game.Defender
{
    public class Defender : MonoBehaviour, IDefender
    {
        [SerializeField] private SpriteRenderer spriteRenderer;

        public void Initialize(DefenderDataModel defenderDataModel)
        {
            spriteRenderer.sprite = SpriteManager.Instance.GetDefenderSprite(defenderDataModel.defenderType);
        }
    }
}