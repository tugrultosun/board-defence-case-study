using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "BoardSettings", menuName = "Scriptable Objects/Board Settings", order = 1)]
    public class BoardSettings : ScriptableObject
    {
        public int width;
        public int height;
        public int defenceItemMaxPlacebleYIndex;
        public float defenderPlacementThreshold;
    }
}
