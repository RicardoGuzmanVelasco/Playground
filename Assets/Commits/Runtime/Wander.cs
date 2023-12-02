using System;
using System.Collections;
using UnityEngine;

namespace Commits.Runtime
{
    public class Wander : MonoBehaviour
    {
        float speed;
        Vector3 target;
        
        public void OnDestroy() => Stop();

        void Update()
        {
            transform.position = Vector3.MoveTowards
            (
                current: transform.position,
                target: target,
                maxDistanceDelta: speed * Time.deltaTime
            );
        }

        public void Endlessly(float mass)
        {
            this.speed = 1 / mass;
            StartCoroutine(WanderInsideMainCamera());
        }

        IEnumerator WanderInsideMainCamera()
        {
            while(true)
            {
                target = Camera.main.RandomPointInside();
                yield return new WaitForSeconds(speed);
            }
        }
        
        public void Stop() => StopAllCoroutines();
        
        //draw a gizmo of the camera bounds
        void OnDrawGizmos()
        {
            var camera = Camera.main;
            var cameraWorldBounds = camera.OrthographicBounds();
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(cameraWorldBounds.center, cameraWorldBounds.size);
        }
    }
}