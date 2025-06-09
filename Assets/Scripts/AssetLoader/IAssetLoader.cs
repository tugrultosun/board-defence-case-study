using System.Threading.Tasks;

namespace AssetLoader
{
    public interface IAssetLoader
    {
        Task<T> LoadAssetAsync<T>(string path) where T : UnityEngine.Object;
        void ReleaseAsset<T>(T asset) where T : UnityEngine.Object;
    }
}
