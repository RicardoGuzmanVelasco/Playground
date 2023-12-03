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
                yield return new WaitUntil(AtTheTarget);
            }
            
            bool AtTheTarget() => Vector2.Distance(transform.position, target) < 0.1f;
        }
        
        public void Stop() => StopAllCoroutines();
        
        void OnDrawGizmos()
        {
            Gizmos.color = GetComponentInChildren<SpriteRenderer>().color;
            Gizmos.DrawLine(transform.position, target);
        }
    }
}