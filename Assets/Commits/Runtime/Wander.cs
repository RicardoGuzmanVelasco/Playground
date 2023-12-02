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

        public void Endlessly(float speed)
        {
            this.speed = speed;
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
    }
}