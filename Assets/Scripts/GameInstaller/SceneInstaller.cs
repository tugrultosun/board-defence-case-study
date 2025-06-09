using Game.Board;
using Game.Tiles;
using UnityEngine;
using Zenject;

namespace GameInstaller
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private BoardManager boardManager;
        [SerializeField] private Transform boardRoot;
        [SerializeField] private Tile tilePrefab;
        
        public override void InstallBindings()
        {
            Debug.Log("SceneInstaller InstallBindings");
            Container.Bind<Tile>().FromInstance(tilePrefab).AsSingle();
            Container.Bind<TileController>().AsSingle().WithArguments(tilePrefab, boardRoot);
            Container.Bind<DefenceController>().AsSingle();
            Container.Bind<EnemyController>().AsSingle();
            Container.Inject(boardManager);
        }
    }
}
