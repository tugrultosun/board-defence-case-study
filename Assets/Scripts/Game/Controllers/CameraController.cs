using Settings;
using UnityEngine;

namespace Game.Controllers
{
    public class CameraController
    {
        private Camera mainCamera;

        public void Initialize(Camera boardCamera)
        {
            this.mainCamera = boardCamera;
            var x = GameSettingsManager.Instance.boardSettings.width;
            var y = GameSettingsManager.Instance.boardSettings.height;
            var pos = new Vector3(x / 2.0f - 0.5f, y / 2.0f - 0.5f, -10.0f);
            var aspectRatio = (float)Screen.width / Screen.height;
            var height = (float)y;
            var width = x / aspectRatio / 2;
            var orthographicSize = (height > width ? height : width) - 2.27f;
            if (mainCamera != null)
            {
                mainCamera.transform.position += new Vector3(pos.x, pos.y, 0);
                mainCamera.orthographicSize = orthographicSize;
                Debug.Log("camera initialized");
            }
            else
            {
                Debug.LogWarning("Main camera is null");
            }
            
        }
    }
}