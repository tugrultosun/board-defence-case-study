using AssetLoader;
using Game.Board;
using Settings;
using UnityEngine;
using Zenject;

namespace GameInstaller
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAssetLoader>().To<AddressableAssetLoader>().AsSingle();
            Container.Bind<DefenceController>().AsSingle();
            Container.Bind<EnemyController>().AsSingle();
            Container.Bind<EnemySettings>().FromInstance(GameSettingsManager.Instance.enemySettings);
            Container.Bind<DefenderSettings>().FromInstance(GameSettingsManager.Instance.defenderSettings);
        }
    }
}
