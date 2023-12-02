using System;
using CommitionalConvents;
using UnityEngine;

namespace Commits.Runtime
{
    public class CommitSpawn : MonoBehaviour
    {
        void Awake()
        {
            FindObjectOfType<SharedModel>().StagingCompleted += FreeSpawningCommit;
        }

        void FreeSpawningCommit(Commit commit)
        {
            Debug.Log($"Spawning commit: {commit.CommitType.id} of {commit.TotalSize} size");
        }
    }
}