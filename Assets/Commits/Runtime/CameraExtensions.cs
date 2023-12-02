using UnityEngine;

namespace Commits.Runtime
{
    public static class CameraExtensions
    {
        public static Bounds OrthographicBounds(this Camera camera)
        {
            var screenAspect = Screen.width / (float)Screen.height;
            var cameraHeight = camera.orthographicSize * 2;

            return new
            (
                center: camera.transform.position,
                size: new(cameraHeight * screenAspect, cameraHeight, 0)
            );
        }
        
        public static Vector3 RandomPointInside(this Camera camera)
        {
            var cameraWorldBounds = camera.OrthographicBounds();
            return new Vector3
            (
                x: UnityEngine.Random.Range(cameraWorldBounds.min.x, cameraWorldBounds.max.x),
                y: UnityEngine.Random.Range(cameraWorldBounds.min.y, cameraWorldBounds.max.y),
                z: 0
            );
        }
    }
}