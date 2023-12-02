using System.Collections;
using UnityEngine;

namespace Commits.Runtime
{
    public class Wander : MonoBehaviour
    {
        public void Endlessly(float speed)
        {
            StartCoroutine(WanderInsideMainCamera(speed));
        }

        IEnumerator WanderInsideMainCamera(float speed)
        {
            while(true)
            {
                var goalPosition = Camera.main.RandomPointInside();

                transform.position = goalPosition;
                yield return new WaitForSeconds(1 / speed);
            }
        }
    }
}