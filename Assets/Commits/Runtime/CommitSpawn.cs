using System;
using System.Linq;
using CommitionalConvents;
using UnityEngine;

namespace Commits.Runtime
{
    public class CommitSpawn : MonoBehaviour
    {
        [SerializeField] CommitBubble commitPrefab;

        public int CommitCount => FindObjectsOfType<CommitBubble>().Length;

        public event Action<CommitBubble> Committed;

        void Awake()
            => FindObjectOfType<SharedModel>()
                .StagingCompleted += FreeSpawningCommit;

        void FreeSpawningCommit(Commit commit)
        {
            var bubble = Instantiate
            (
                commitPrefab,
                transform.position,
                Quaternion.identity,
                FindObjectsOfType<Transform>().Single(x => x.name == "Origin")
            );
            bubble.Free(number: CommitCount, commit);
            
            Committed?.Invoke(bubble);
        }
    }
}