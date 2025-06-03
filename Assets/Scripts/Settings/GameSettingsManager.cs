using System.Collections;
using System.Collections.Generic;
using Settings;
using UnityEditor;
using UnityEngine;

public class GameSettingsManager : MonoSingleton<GameSettingsManager>
{
    public DefenderSettings defenderSettings;
    public EnemySettings enemySettings;
}
