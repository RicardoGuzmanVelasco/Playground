using System;
using System.Collections;
using System.Linq;
using CommitionalConvents;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Commits.Runtime
{
    public class IssueSpawn : MonoBehaviour
    {
        [SerializeField] IssueTicket issuePrefab;
        
        public int IssueCount => FindObjectsOfType<IssueTicket>().Length;

        IEnumerator Start()
        {
            while (true)
            {
                yield return new WaitForSeconds(RythmOfIssues());
                Instantiate
                (
                    issuePrefab,
                    transform.position,
                    Quaternion.identity,
                    Origin()
                ).Emerge(number: IssueCount, issue: RandomIssue());
            }
        }

        static Issue RandomIssue() => Issue.Emerge(RandomSize(), RandomType());

        static float RandomSize() => Random.Range(0, 5f) / Random.Range(0, 5f);

        static Issue.Type RandomType()
            => Random.Range(0, 1f) switch
            {
                < 0.2f => Issue.Type.ToDo,
                < 0.4f => Issue.Type.Bug,
                < 0.5f => Issue.Type.Debt,
                < 0.6f => Issue.Type.Qa,
                < .65f => Issue.Type.DevOps,
                < 0.7f => Issue.Type.Plugin,
                < 0.85f => Issue.Type.Doubt,
                < .95f => Issue.Type.Stuff,
                < .985f => Issue.Type.Guide,
                < 1f => Issue.Type.Lag,
                _ => throw new InvalidOperationException()
            };

        static float RythmOfIssues() => Random.Range(5, 10f) * Random.Range(1, 3f);

        static Transform Origin()
            => FindObjectsOfType<Transform>().Single(x => x.name == "Origin");
    }
}