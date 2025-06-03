using Defender;
using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "Defender Settings", menuName = "Scriptable Objects/Defender Settings", order = 1)]
    public class DefenderSettings : ScriptableObject
    {
        public DefenderDataModel defenderData1;
        public DefenderDataModel defenderData2;
        public DefenderDataModel defenderData3;
    }
}
