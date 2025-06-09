using AssetLoader;
using Game.Board;
using Settings;
using Zenject;

namespace GameInstaller
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            var settingsManager = GameSettingsManager.Instance;
            Container.Bind<IAssetLoader>().To<AddressableAssetLoader>().AsSingle();
            Container.Bind<EnemySettings>().FromInstance(settingsManager.enemySettings);
            Container.Bind<DefenderSettings>().FromInstance(settingsManager.defenderSettings);
            Container.Bind<BoardSettings>().FromInstance(settingsManager.boardSettings).AsSingle();
        }
    }
}
