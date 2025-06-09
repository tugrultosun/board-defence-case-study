using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace AssetLoader
{
    public class AddressableAssetLoader : IAssetLoader
    {
        public async Task<T> LoadAssetAsync<T>(string key) where T : Object
        {
            var handle = Addressables.LoadAssetAsync<T>(key);
            await handle.Task;
            return handle.Status == AsyncOperationStatus.Succeeded ? handle.Result : null;
        }

        public void ReleaseAsset<T>(T asset) where T : Object
        {
            Addressables.Release(asset);
        }
    }
}