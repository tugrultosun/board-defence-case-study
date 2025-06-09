using Game.Board;
using UnityEngine;
using Zenject;

namespace GameInstaller
{
    public class SceneInstaller : MonoInstaller
    {
        [SerializeField] private BoardManager boardManager;

        public override void InstallBindings()
        {
            Debug.Log("SceneInstaller InstallBindings");
            Container.Inject(boardManager);
        }
    }
}
